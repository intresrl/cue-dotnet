// QUICKSTART.md - Get started with the CUE API patterns example in 5 minutes

## Installation

You need CUE installed. Check:

```bash
cue version
```

If not installed, visit https://cuelang.org/docs/install/ or use your package manager.

## The Files You Need to Know About

### Start Here (Read These First)
1. **QUICKSTART.md** - This file
2. **EXAMPLE_SUMMARY.md** - High-level overview
3. **README_CUE_PATTERNS.md** - Detailed file organization

### Deep Dives
- **CRUD_TEMPLATE_GUIDE.md** - Understanding the template pattern

### The Code
- **crud_template.cue** - Generic CRUD operations
- **crud_usage_example.cue** - Using the template for Documents, Users, Teams
- **models.cue** - Resource types and filters
- **responses.cue** - Standard response patterns
- **primitives.cue** - Base types

## Quick Examples

### 1. See the CRUD Template
```bash
cd C:\i3\git\cue\cue-api-dotnet\cue
cue eval crud_template.cue | head -50
```

This shows the generic Create, Read, List, Update, Delete operations available for any resource.

### 2. See It Specialized
```bash
cue eval crud_template.cue crud_usage_example.cue | grep -A 10 "DocumentCreateEndpoint:"
```

This shows how the template is specialized for Documents.

### 3. View Complete API Spec
```bash
cue eval primitives.cue responses.cue models.cue crud_template.cue crud_usage_example.cue
```

Shows the complete API with all endpoints for Documents, Users, and Teams.

### 4. Export to JSON
```bash
cue export crud_usage_example.cue --out json | jq '.APISpec.endpoints | keys | head' 2>/dev/null
```

Export the API specification as JSON and see all endpoint paths.

## Key Concepts in 30 Seconds

### 1. Primitives
```cue
// primitives.cue
#Timestamp: string              // ISO 8601
#ErrorDetail: {
  code: string
  message: string
}
```

### 2. Reusable Patterns
```cue
// responses.cue - used by 50+ endpoints
#ErrorResponse: {
  error: #ErrorDetail
  timestamp?: string
}
```

### 3. Request Models
```cue
// models.cue - resource-specific types
#DocumentResource: { title: string, status: "draft" | "published" }
#DocumentFilter: { search?: {query: string} }
```

### 4. Generic Template
```cue
// crud_template.cue - create once, use everywhere
#CRUDTemplate: {
  Create: { request: _, response: { "201": {...} } }
  Read: { request: {id: string}, response: { "200": _ } }
  // ... more operations
}
```

### 5. Specialize for Your Resource
```cue
// Unify template with your resource type
#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource    // Your specific type!
  }
}
```

## Structure Overview

```
primitives.cue
  ↓ (defines base types)
  
responses.cue
  ↓ (defines error/list patterns)
  
models.cue
  ↓ (defines resource types and filters)
  
crud_template.cue
  ↓ (defines generic CRUD operations)
  
crud_usage_example.cue
  ↓ (specializes template for Documents, Users, Teams)
  
endpoints.cue (custom endpoints beyond CRUD)
```

## Try It Yourself

### Step 1: Create a New Resource
Add to `models.cue`:
```cue
#ProjectResource: {
  id?: string
  name: string & !=""
  description?: string
  ownerIds: [...string]
  status: "active" | "archived"
}

#ProjectListItem: {
  id: string
  name: string
  status: string
}

#ProjectFilter: {
  search?: {query: string}
  statusIn?: [...string]
}
```

### Step 2: Specialize the Template
Add to a new file or `crud_usage_example.cue`:
```cue
#ProjectCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #ProjectResource
  }
  Read: #CRUDTemplate.Read & {
    response: "200": #ProjectResource
  }
  List: #CRUDTemplate.List & {
    request: filter?: #ProjectFilter
    response: "200": items: [...#ProjectListItem]
  }
  Update: #CRUDTemplate.Update & {
    response: "200": #ProjectResource
  }
  Delete: #CRUDTemplate.Delete
  // ... batch operations
}
```

### Step 3: Use in API
```cue
ProjectCreateEndpoint: #ProjectCRUDEndpoints.Create
ProjectListEndpoint: #ProjectCRUDEndpoints.List
// ... etc
```

### Step 4: Validate
```bash
cue eval your_file.cue
```

You now have 8 fully-typed endpoints with error handling, pagination, and batch operations—without copying any boilerplate!

## Common Commands

```bash
# Validate syntax and print evaluated CUE
cue eval crud_template.cue

# Show type definitions (not concrete values)
cue def crud_template.cue

# Export as JSON
cue export crud_usage_example.cue --out json

# Filter evaluation (show only DocumentCreate endpoint)
cue eval crud_usage_example.cue | grep -A 5 "DocumentCreate"

# Count lines in specification
wc -l *.cue

# View a specific field
cue eval crud_usage_example.cue | jq '.APISpec.endpoints | keys'
```

## What Each Operation Does

### Create
- **Request**: Full resource with all fields
- **Response 201**: Just the resourceId
- **Response 400/409/422**: Error details

### Read
- **Request**: ID only
- **Response 200**: Full resource
- **Response 404**: Not found error

### List
- **Request**: Optional filter, pagination params, sorting
- **Response 200**: Items array + pagination metadata
- **Response 401**: Unauthorized error

### Update
- **Request**: ID + resource fields to update
- **Response 200**: Updated resource
- **Response 404/409**: Not found or conflict error

### Delete
- **Request**: ID only
- **Response 204**: No content (success)
- **Response 404**: Not found error

### BatchCreate
- **Request**: Array of resources
- **Response 207**: Multi-Status with per-item results

### BatchUpdate
- **Request**: Filter (which resources) + updates (what to change)
- **Response 200**: Count of updated/skipped
- **Response 422**: Filter matched nothing

### BatchDelete
- **Request**: Filter (which resources to delete)
- **Response 200**: Count of deleted/skipped

## Comparison to YAML

**YAML approach** (repetitive):
```yaml
endpoints:
  POST /documents:
    requestBody: {...}
    responses:
      201: { resourceId: string }
      400: { error: {...} }
      409: { error: {...} }
  
  POST /users:
    requestBody: {...}
    responses:
      201: { resourceId: string }  # Same!
      400: { error: {...} }        # Copied!
      409: { error: {...} }        # Copied!
```

**CUE approach** (DRY):
```cue
#CRUDTemplate: {
  Create: {
    request: _
    response: { 201: {...}, 400: {...}, 409: {...} }
  }
}

#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & { request: #DocumentResource }
}

#UserCRUDEndpoints: {
  Create: #CRUDTemplate.Create & { request: #UserResource }
}
```

One template. Three specializations. Done.

## Learning Path

1. **Beginner**: Read EXAMPLE_SUMMARY.md + view models.cue
2. **Intermediate**: Study crud_template.cue + try modifying a resource type
3. **Advanced**: Read CRUD_TEMPLATE_GUIDE.md + implement a new resource type
4. **Expert**: Extend template with new operations (Export, Import, etc.)

## Next Steps

- ✅ Run `cue eval` to validate the examples
- ✅ Add your own resource type
- ✅ Export to JSON for use with other tools
- ✅ Read CRUD_TEMPLATE_GUIDE.md for deep understanding
- ✅ Experiment with modifying constraint values

Happy CUE-ing! 🎉
