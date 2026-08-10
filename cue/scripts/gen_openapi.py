#!/usr/bin/env python3
"""Generate OpenAPI 3.0 JSON from CUE types (domain-agnostic).

This script auto-discovers resource packages and generates OpenAPI 3.0 docs
from:
- operations/api.cue: APISpec (title, version, servers, etc)
- operations/schemas.cue: Schemas (all JSON Schema definitions)
- resources/*/paths.cue: PathItems and Tag from each resource

The script is completely generic and works with any CUE project following
this structure. No hardcoded resource names.

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


def main():
    # Read API metadata from operations/api.cue
    api_spec = run_cue_export("./operations/...", "APISpec")
    
    # Read all schemas from operations/schemas.cue
    schemas = run_cue_export("./operations/...", "Schemas")
    
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
            paths.update(path_items)
    
    # Assemble complete OpenAPI 3.0 document
    doc = {
        "openapi": "3.0.0",
        "info": {
            "title": api_spec["title"],
            "version": api_spec["version"],
            "description": api_spec["description"],
            "x-generated-from": "CUE types (resources/*/types.cue via cue vet)",
        },
        "servers": api_spec["servers"],
        "tags": tags,
        "paths": paths,
        "components": {
            "schemas": schemas,
        },
    }
    
    print(json.dumps(doc, indent=2))


if __name__ == "__main__":
    main()
