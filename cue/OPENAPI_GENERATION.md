# OpenAPI Generation & Documentation

## Overview

This project includes automated generation of OpenAPI 3.0 specification and interactive Swagger UI documentation from CUE types. The generation is **completely domain-agnostic** — add new resources without changing Python code.

## Quick Start

Generate and view API documentation:

```bash
# Generate OpenAPI JSON and HTML
just gen-docs

# Serve locally (requires Python 3)
just serve-docs

# Then open http://localhost:8000 in your browser
```

## Files

- **scripts/gen_openapi.py** - Domain-agnostic Python script that generates OpenAPI 3.0 JSON
- **swagger.html** - Static HTML page using Swagger UI from unpkg CDN
- **dist/openapi.json** - Generated OpenAPI 3.0 specification (created by gen-docs)
- **dist/index.html** - Generated Swagger UI documentation (created by gen-docs)

## How It Works

### Generation Process

1. **`just gen-openapi`** - Generates `dist/openapi.json` from CUE spec
2. **`just gen-docs`** - Runs gen-openapi, then copies `swagger.html` → `dist/index.html`
3. **`just serve-docs`** - Serves the dist/ directory on localhost:8000

### Architecture (Simplified & Domain-Agnostic)

```
operations/api.cue (APISpec)
    ↓
operations/schemas.cue (Schemas)
    ↓                      ↓
resources/*/paths.cue (auto-discovered)
    ↓
gen_openapi.py (domain-agnostic, auto-discovers resources)
    ↓
dist/openapi.json (OpenAPI 3.0)
    ↓
swagger.html (Swagger UI)
    ↓
dist/index.html
    ↓
Browser (http://localhost:8000)
```

### Key Design Points

- **Domain-Agnostic**: No hardcoded resource names in Python
- **Auto-Discovers Resources**: Scans `resources/` directory
- **Auto-Discovers Tags & Paths**: Exports from each resource package
- **Type-Driven Schemas**: All schemas come from CUE types via operations/schemas.cue

## OpenAPI 3.0 Structure

The generated `openapi.json` includes:

```json
{
  "openapi": "3.0.0",
  "info": {
    "title": "Document Management & Collaboration API",
    "version": "1.0.0",
    "description": "Complete API with modular namespaces and CRUD patterns",
    "x-generated-from": "CUE types (operations/schemas.cue)"
  },
  "servers": [
    { "url": "http://localhost:8080", "description": "Development" },
    { "url": "https://api.example.com", "description": "Production" }
  ],
  "tags": [
    { "name": "documents", "description": "Document management endpoints" },
    { "name": "users", "description": "User account management endpoints" },
    { "name": "teams", "description": "Team management endpoints" }
  ],
  "paths": {
    "/documents": { ... },
    "/documents/{id}": { ... },
    "/users": { ... },
    "/users/{id}": { ... },
    "/teams": { ... },
    "/teams/{id}": { ... }
  },
  "components": {
    "schemas": {
      "Document": { ... },
      "User": { ... },
      "Team": { ... },
      "ErrorResponse": { ... },
      "PaginationMeta": { ... }
    }
  }
}
```

## Swagger UI

The `swagger.html` file:

- Uses **Swagger UI** from unpkg CDN (no build required)
- Loads `openapi.json` dynamically at runtime
- Provides interactive API documentation
- Allows testing API endpoints (with mock data)
- Works offline if files are local

### Browser Example

```html
<script src="https://unpkg.com/swagger-ui-dist@3/swagger-ui-bundle.js"></script>
<script>
  SwaggerUIBundle({
    spec: { /* openapi.json */ },
    dom_id: '#swagger-ui',
    presets: [SwaggerUIBundle.presets.apis, SwaggerUIStandalonePreset],
    layout: "BaseLayout"
  });
</script>
```

## Usage Examples

### Generate documentation once

```bash
just gen-docs
# Creates:
#   dist/openapi.json  (OpenAPI 3.0 spec)
#   dist/index.html    (Swagger UI)

# Open file://$(pwd)/dist/index.html in browser
```

### Serve and develop

```bash
just serve-docs

# Server runs at http://localhost:8000
# Press Ctrl+C to stop
```

### Regenerate after changes

```bash
# After modifying CUE files:
just gen-docs      # Regenerates openapi.json and copies HTML

# Refresh browser - new spec is loaded automatically
```

### Add a new resource

The Python script is domain-agnostic, so adding a new resource is simple:

```bash
# 1. Create resource package
mkdir -p resources/new-resource

# 2. Create paths.cue with Tag and empty PathItems
cat > resources/new-resource/paths.cue << 'EOF'
package new_resource

Tag: {
	name: "new-resource"
	description: "Resource description"
}

PathItems: {}
EOF

# 3. Add types to operations/schemas.cue:
#    - NewResource (main type)
#    - NewResourceListItem (for list responses)
#    - NewResourceFilter (for filtering)
#    - NewResourceBatchCreateRequest
#    - NewResourceBatchUpdateRequest
#    - NewResourceBatchDeleteRequest

# 4. Run gen-openapi.py - it auto-discovers the resource!
just gen-docs
```

No Python code changes needed!

## Customization

### Change API metadata

Edit `operations/api.cue`:

```cue
APISpec: {
  version:     "2.0.0"
  title:       "My Custom API"
  description: "..."
  servers: [
    {url: "https://api.example.com", description: "Production"},
  ]
}
```

### Add custom schemas

Edit `operations/schemas.cue`:

```cue
Schemas: {
  // Framework & resource schemas are auto-merged
  MyCustomSchema: {
    type: "object"
    properties: { ... }
  }
}
```

### Change Swagger UI theme

Edit `swagger.html`, add to SwaggerUIBundle config:

```html
layout: "StandalonePreset",
theme: "dark",  // or "light"
```

## How It Generates Schemas from Types

All schemas are defined as plain CUE types in `operations/schemas.cue` and exported to JSON Schema:

1. **CUE Types** (operations/schemas.cue): Plain CUE type definitions (no constraints)
2. **Type Instances** (operations/schemas.cue Schemas export): Concrete instances with example values
3. **JSON Schema Generation**: Python script exports instances and converts them to JSON Schema
4. **Stitching**: Python script combines all exports into OpenAPI document

This approach keeps types and schemas in sync with a single source of truth.

## Technical Details

### Why Domain-Agnostic Python?

✓ **Reusable** - Works with any CUE API project
✓ **Scalable** - Add resources without touching Python
✓ **Maintainable** - Single source of truth (CUE types)
✓ **Robust** - Auto-discovers and gracefully skips missing resources

### Why Swagger UI from unpkg?

✓ **No build process** - Just HTML and CDN URLs
✓ **No dependencies** - Works offline with local files
✓ **Easy to customize** - Pure HTML/JavaScript
✓ **Standard** - Official Swagger UI library
✓ **Lightweight** - ~300KB total (cached by browser)

## Files Generated

| File | Size | Purpose |
|------|------|---------|
| `dist/openapi.json` | ~12 KB | OpenAPI 3.0 specification |
| `dist/index.html` | ~2 KB | Swagger UI HTML page |

Total: ~14 KB for complete interactive API documentation

## Integration

### With CI/CD

```bash
# In your CI pipeline
just gen-docs
# Commit dist/openapi.json and dist/index.html to repo
# or deploy to docs server
```

### With API Gateway

```bash
# Export OpenAPI spec for use in:
# - AWS API Gateway
# - Kong
# - Apigee
# - Any OpenAPI-compatible gateway

cp dist/openapi.json /path/to/api-gateway/specs/
```

### With Documentation Generators

```bash
# Use dist/openapi.json with:
# - ReDoc (alternative UI)
# - Stoplight Elements
# - Postman (import)
# - Insomnia (import)
```

## Troubleshooting

### swagger.html shows "Error: Could not load openapi.json"

**Cause**: The script running index.html can't find openapi.json
**Fix**: 
- Ensure both files are in the same directory
- Make sure to run `just gen-openapi` first
- Use `just serve-docs` to serve via HTTP (file:// protocol has CORS restrictions)

### gen-openapi.py fails

**Cause**: Python 3 not installed or cue command not in PATH
**Fix**:
- Install Python 3: `python --version` should show 3.6+
- Ensure `cue` is in PATH: `which cue`

### Port 8000 already in use

**Fix**: Kill the existing process or use a different port:
```bash
cd dist && python -m http.server 9000
```

## Next Steps

1. **Generate docs**: `just gen-docs`
2. **View locally**: Open `dist/index.html` in browser
3. **Serve**: `just serve-docs` for interactive testing
4. **Deploy**: Copy `dist/` to documentation server or CI/CD pipeline
5. **Add resources**: Just create resource packages, no Python changes needed
6. **Customize**: Modify `operations/api.cue` or `swagger.html` as needed

## Resources

- **OpenAPI 3.0 Spec**: https://spec.openapis.org/oas/v3.0.0
- **Swagger UI Docs**: https://swagger.io/tools/swagger-ui/
- **Swagger UI GitHub**: https://github.com/swagger-api/swagger-ui
- **unpkg CDN**: https://unpkg.com/
- **CUE Language**: https://cuelang.org/
- **CUE JSON Schema**: https://cuelang.org/docs/concept/how-cue-works-with-json-schema/
