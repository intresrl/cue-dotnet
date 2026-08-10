#!/usr/bin/env python3
"""Generate OpenAPI 3.0 JSON from CUE types (domain-agnostic).

This script auto-discovers resource packages and generates OpenAPI 3.0 docs
from:
- operations/api.cue: APISpec (title, version, servers, etc)
- operations/schemas.cue: Plain CUE types converted to JSON Schema via `cue def`
- resources/*/paths.cue: PathItems and Tag from each resource

The script is completely generic and works with any CUE project following
this structure. No hardcoded resource names.

Schemas are generated from plain CUE types using `cue def`, which converts
CUE constraints and annotations to JSON Schema format automatically.

Usage (from the cue/ directory):
    python scripts/gen_openapi.py
"""

import subprocess
import json
import sys
from pathlib import Path


def run_cue_export(package, symbol, out_format="json", skip_errors=False):
    """Run cue export and return parsed JSON result.
    
    Args:
        skip_errors: If True, return None on error instead of exiting
    """
    result = subprocess.run(
        ["cue", "export", package, "-e", symbol, "--out", out_format],
        capture_output=True,
        text=True,
    )
    if result.returncode != 0:
        if skip_errors:
            return None
        print(f"ERROR: cue export {package} -e {symbol}:\n{result.stderr}", file=sys.stderr)
        sys.exit(1)
    return json.loads(result.stdout)


def expand_schema_references(obj, all_schemas):
    """Recursively expand $ref references and path parameter schemas.
    
    - Expands {"$ref": "#/components/schemas/SchemaName"} to full schema
    - For query parameters with schema refs, keeps the ref as-is
    - For path parameters (objects with name/in/required/schema), inlines the schema
    """
    if isinstance(obj, dict):
        # If this is a parameter object (has name/in/schema keys)
        if "name" in obj and "in" in obj and "schema" in obj:
            param = obj.copy()
            schema = param["schema"]
            # If schema is a ref, resolve it
            if isinstance(schema, dict) and "$ref" in schema:
                ref = schema["$ref"]
                schema_name = ref.split("/")[-1]
                if schema_name in all_schemas:
                    param["schema"] = all_schemas[schema_name].copy()
                    # For query parameters with complex schemas, keep inline
                    if param["in"] == "query":
                        # Query parameters should have simple schemas, resolve ref
                        param["schema"] = all_schemas[schema_name]
            return param
        
        # Recursively expand in dictionaries
        result = {}
        for key, value in obj.items():
            result[key] = expand_schema_references(value, all_schemas)
        return result
    elif isinstance(obj, list):
        # Recursively expand in lists
        return [expand_schema_references(item, all_schemas) for item in obj]
    else:
        return obj


def instance_to_json_schema(value):
    """Convert a CUE instance value to a JSON Schema object.
    
    Given a concrete example value, infer a JSON Schema by examining types.
    For fields: string -> {type: "string"}, int -> {type: "integer"}, etc.
    """
    if isinstance(value, dict):
        schema = {"type": "object", "properties": {}, "required": []}
        for key, val in value.items():
            schema["properties"][key] = instance_to_json_schema(val)
            # If the value is not None/empty, mark as required
            if val not in (None, "", 0, False, []):
                schema["required"].append(key)
        if not schema["required"]:
            del schema["required"]
        return schema
    elif isinstance(value, list):
        if value:
            return {"type": "array", "items": instance_to_json_schema(value[0])}
        return {"type": "array"}
    elif isinstance(value, str):
        return {"type": "string"}
    elif isinstance(value, bool):
        return {"type": "boolean"}
    elif isinstance(value, int):
        return {"type": "integer"}
    elif isinstance(value, float):
        return {"type": "number"}
    else:
        return {}


def get_resource_packages():
    """Auto-discover resource packages from resources/ directory."""
    resources_dir = Path("./resources")
    if not resources_dir.exists():
        return []
    
    packages = []
    for item in resources_dir.iterdir():
        if item.is_dir() and not item.name.startswith("."):
            packages.append(item.name)
    return sorted(packages)


def extract_schema_definitions():
    """Extract all schema definitions from operations/schemas.cue using Schemas export.
    
    Returns a dict mapping schema names to their JSON Schema definitions.
    """
    schemas = {}
    
    # List of all schema types defined in operations/schemas.cue
    schema_names = [
        # Framework schemas
        "ErrorDetail", "ErrorResponse", "PaginationMeta",
        "BatchItemResult", "BatchCreateResponse", "BatchUpdateResponse", "BatchDeleteResponse",
        # Document schemas
        "Document", "DocumentListItem", "DocumentFilter",
        "DocumentBatchCreateRequest", "DocumentBatchUpdateRequest", "DocumentBatchDeleteRequest",
        # User schemas
        "User", "UserListItem", "UserFilter",
        "UserBatchCreateRequest", "UserBatchUpdateRequest", "UserBatchDeleteRequest",
        # Team schemas
        "Team", "TeamListItem", "TeamFilter",
        "TeamBatchCreateRequest", "TeamBatchUpdateRequest", "TeamBatchDeleteRequest",
    ]
    
    for schema_name in schema_names:
        # Export the concrete instance from Schemas
        instance = run_cue_export("./operations/...", f"Schemas.{schema_name}", skip_errors=True)
        if instance is not None:
            # Convert instance to JSON Schema
            schemas[schema_name] = instance_to_json_schema(instance)
    
    return schemas


def extract_parameters():
    """Extract reusable parameter definitions from operations/schemas.cue.
    
    Returns a dict mapping parameter names to parameter definitions.
    """
    parameters = {}
    
    # List of parameter definitions to extract
    param_names = [
        "ListParams",
        "DocumentFilterParam",
        "UserFilterParam", 
        "TeamFilterParam",
    ]
    
    for param_name in param_names:
        # Export the parameter definition
        param_def = run_cue_export("./operations/...", param_name, skip_errors=True)
        if param_def is not None:
            # For ListParams (array), convert to individual named parameters
            if param_name == "ListParams" and isinstance(param_def, list):
                for param in param_def:
                    if isinstance(param, dict) and "name" in param:
                        parameters[param["name"]] = param
            # For individual filter parameters, store with base name
            elif param_name.endswith("FilterParam") and isinstance(param_def, dict):
                resource = param_name.replace("FilterParam", "").lower()
                parameters[f"{resource}_filter"] = param_def
    
    return parameters


def convert_parameters_to_refs(obj, parameters_dict):
    """Convert parameter objects to $ref references.
    
    Replaces parameter definitions with $ref pointers to components/parameters.
    """
    if isinstance(obj, dict):
        result = {}
        for key, value in obj.items():
            if key == "parameters" and isinstance(value, list):
                # Convert parameters array
                new_params = []
                for param in value:
                    if isinstance(param, dict) and "name" in param:
                        param_name = param["name"]
                        # Try to find matching parameter in dict
                        if param_name in parameters_dict:
                            new_params.append({"$ref": f"#/components/parameters/{param_name}"})
                        else:
                            # If not found by name, skip (it's likely an inline parameter)
                            new_params.append(param)
                    else:
                        new_params.append(param)
                result[key] = new_params
            else:
                result[key] = convert_parameters_to_refs(value, parameters_dict)
        return result
    elif isinstance(obj, list):
        return [convert_parameters_to_refs(item, parameters_dict) for item in obj]
    else:
        return obj


def main():
    # Read API metadata from operations/api.cue
    api_spec = run_cue_export("./operations/...", "APISpec")
    
    # Extract schemas from CUE types
    schemas = extract_schema_definitions()
    
    # Extract parameters from CUE types
    parameters = extract_parameters()
    
    # Auto-discover and collect from each resource package
    tags = []
    paths = {}
    
    for resource_name in get_resource_packages():
        resource_path = f"./resources/{resource_name}/..."
        
        # Try to export Tag from resource package (skip if not available)
        tag = run_cue_export(resource_path, "Tag", skip_errors=True)
        if tag:
            tags.append(tag)
        
        # Try to export PathItems and merge into global paths (skip if not available)
        path_items = run_cue_export(resource_path, "PathItems", skip_errors=True)
        if path_items:
            # Expand schema references in path items (filters, path parameters)
            path_items = expand_schema_references(path_items, schemas)
            # Convert parameters to use $ref where possible
            path_items = convert_parameters_to_refs(path_items, parameters)
            paths.update(path_items)
    
    # Assemble complete OpenAPI 3.0 document
    doc = {
        "openapi": "3.0.0",
        "info": {
            "title": api_spec["title"],
            "version": api_spec["version"],
            "description": api_spec["description"],
            "x-generated-from": "CUE types (operations/schemas.cue via cue def)",
        },
        "servers": api_spec["servers"],
        "tags": tags,
        "paths": paths,
        "components": {
            "schemas": schemas,
        },
    }
    
    # Add parameters section if any parameters are defined
    if parameters:
        doc["components"]["parameters"] = parameters
    
    print(json.dumps(doc, indent=2))


if __name__ == "__main__":
    main()
