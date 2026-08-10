// CRUD_TEMPLATE_GUIDE.md - Guide to understanding and using the CRUD template pattern

## What is the CRUD Template?

The CRUD template is a generic, reusable pattern that defines the standard Create, Read, Update, Delete operations for any resource type. Instead of defining these operations separately for Documents, Users, Teams, etc., we define them once and specialize them for each resource.

## The Problem It Solves

Without templates, defining a full CRUD API requires repetition:

```cue
// Without template - lots of repetition
#CreateDocumentEndpoint: {
  request: #DocumentResource
  response: {
    "201": { resourceId: string }
    "400": #ErrorResponse
    "409": #ErrorResponse
    "422": #ErrorResponse
  }
}

#CreateUserEndpoint: {
  request: #UserResource
  response: {
    "201": { resourceId: string }    // Copied
    "400": #ErrorResponse            // Copied
    "409": #ErrorResponse            // Copied
    "422": #ErrorResponse            // Copied
  }
}

#CreateTeamEndpoint: {
  request: #TeamResource
  response: {
    "201": { resourceId: string }    // Copied AGAIN
    "400": #ErrorResponse            // Copied AGAIN
    "409": #ErrorResponse            // Copied AGAIN
    "422": #ErrorResponse            // Copied AGAIN
  }
}
```

## The Template Solution

With the CRUD template, you define operations once with placeholder types:

```cue
#CRUDTemplate: {
  Create: {
    request: _                       // Placeholder
    response: {
      "201": { resourceId: string }
      "400": #ErrorResponse
      "409": #ErrorResponse
      "422": #ErrorResponse
    }
  }
  Read: { ... }
  Update: { ... }
  Delete: { ... }
  // ... and batch operations
}
```

Then specialize for each resource by unifying with concrete types:

```cue
#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource     // Fills the placeholder
  }
  Read: #CRUDTemplate.Read & {
    response: "200": #DocumentResource
  }
  // ... 5 more operations
}

#UserCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #UserResource         // Different resource type
  }
  // ... rest of operations
}
```

## How Reference Scopes Make This Work

The key insight is **reference scope binding**. When CUE evaluates:

```cue
#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource
  }
}
```

CUE does NOT just copy-paste `#CRUDTemplate.Create`. Instead, it **unifies** the template with the new constraint. The reference to `#DocumentResource` is resolved **in this context**, not in the original template definition.

This is precisely what the CUE alias pattern enables - making relative references that work correctly wherever the template is instantiated.

## Structure of Each Operation in the Template

### Create
```cue
Create: {
  request: _                          // Resource-specific (filled by caller)
  response: {
    "201": { resourceId: string }     // Success: returns ID
    "400": #ErrorResponse             // Bad input
    "409": #ErrorResponse             // Conflict (duplicate)
    "422": #ErrorResponse             // Validation failed
  }
}
```

### Read
```cue
Read: {
  request: { id: string }             // Get by ID
  response: {
    "200": _                          // Returns full resource
    "400": #ErrorResponse
    "401": #ErrorResponse             // Unauthorized
    "404": #ErrorResponse             // Not found
  }
}
```

### List
```cue
List: {
  request: {
    filter?: _                        // Resource-specific filter (optional)
    pageNumber?: int & >=1
    pageSize?: int & >=1 & <=100
    sortBy?: string
    sortDirection?: "asc" | "desc"
  }
  response: {
    "200": {
      items: [...]                    // List of items
      pagination: {                   // Always included
        pageNumber: int & >=1
        pageSize: int & >=1 & <=100
        totalCount: int & >=0
        hasMore: bool
      }
    }
    "400": #ErrorResponse
    "401": #ErrorResponse
  }
}
```

### Update
```cue
Update: {
  request: { id: string }             // ID in path
  response: {
    "200": _                          // Returns updated resource
    "400": #ErrorResponse
    "401": #ErrorResponse
    "404": #ErrorResponse
    "409": #ErrorResponse             // Conflict/state mismatch
  }
}
```

### Delete
```cue
Delete: {
  request: { id: string }
  response: {
    "204": { acknowledged: bool }     // No content returned
    "400": #ErrorResponse
    "401": #ErrorResponse
    "404": #ErrorResponse
  }
}
```

### Batch Operations
```cue
BatchCreate: {
  request: {
    items: [...]                      // Multiple resources
    continueOnError?: bool            // Keep going on error?
  }
  response: {
    "207": {                          // Multi-Status (partial success)
      succeeded: int
      failed: int
      results: [...]                  // Per-item results
    }
    "400": #ErrorResponse
  }
}

BatchUpdate: {
  request: {
    filter: _                         // Match resources to update
    updates: _                        // What to change
    dryRun?: bool                     // Preview impact?
  }
  response: {
    "200": {
      updated: int
      skipped: int
      dryRun: bool
    }
    "400": #ErrorResponse
    "422": #ErrorResponse             // Filter matched nothing
  }
}

BatchDelete: {
  request: {
    filter: _                         // Match resources to delete
    confirmDeletion?: bool
  }
  response: {
    "200": {
      deleted: int
      skipped: int
    }
    "400": #ErrorResponse
  }
}
```

## Complete Specialization Example

Here's how to fully specialize the template for a resource:

```cue
// 1. Define resource types
#DocumentResource: {
  id?: string
  title: string & !=""
  description?: string
  status: "draft" | "published" | "archived"
  createdAt?: string
}

#DocumentListItem: {
  id: string
  title: string
  status: string
  createdAt: string
}

#DocumentFilter: {
  search?: { query: string }
  statusIn?: [...string]
  isPublic?: bool
}

// 2. Create CRUD endpoints by specializing template
#DocumentCRUDEndpoints: {
  // Create specialization
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource
  }
  
  // Read specialization
  Read: #CRUDTemplate.Read & {
    response: "200": #DocumentResource
  }
  
  // List specialization
  List: #CRUDTemplate.List & {
    request: filter?: #DocumentFilter
    response: "200": items: [...#DocumentListItem]
  }
  
  // Update specialization
  Update: #CRUDTemplate.Update & {
    response: "200": #DocumentResource
  }
  
  // Delete (no specialization needed - same for all)
  Delete: #CRUDTemplate.Delete
  
  // Batch operations
  BatchCreate: #CRUDTemplate.BatchCreate & {
    request: items: [...#DocumentResource]
  }
  
  BatchUpdate: #CRUDTemplate.BatchUpdate & {
    request: {
      filter: #DocumentFilter
      updates: #DocumentResource
    }
  }
  
  BatchDelete: #CRUDTemplate.BatchDelete & {
    request: filter: #DocumentFilter
  }
}

// 3. Use the specialized endpoints
DocumentCreateAPI: #DocumentCRUDEndpoints.Create
DocumentListAPI: #DocumentCRUDEndpoints.List
DocumentUpdateAPI: #DocumentCRUDEndpoints.Update
// ... etc
```

## Benefits

1. **DRY**: Error handling, status codes, pagination, and batch patterns defined once
2. **Consistency**: All resources follow same API structure
3. **Extensibility**: Add new resource type? Just specialize the template 3 times
4. **Type Safety**: Resource-specific types prevent mixing Document filters with User endpoints
5. **Maintainability**: Fix response format? Update template, all resources inherit the fix
6. **Scalability**: Add 50 new resources? Still just 50 specializations of the same template

## Extending the Template

To add new operations to the template:

```cue
#CRUDTemplate: {
  Create: { ... }
  Read: { ... }
  List: { ... }
  Update: { ... }
  Delete: { ... }
  
  // Add new operation
  Export: {
    request: {
      filter: _
      format: "json" | "csv" | "xml"
    }
    response: {
      "200": { data: string }    // Binary data
      "400": #ErrorResponse
    }
  }
  
  // Add new operation
  Import: {
    request: {
      data: string               // Uploaded file/content
      mode: "create" | "update" | "upsert"
    }
    response: {
      "207": #BatchOperationResult
      "400": #ErrorResponse
    }
  }
}
```

Then specialize in each resource:

```cue
#DocumentCRUDEndpoints: {
  // ... Create, Read, List, Update, Delete ...
  
  Export: #CRUDTemplate.Export & {
    request: filter: #DocumentFilter
  }
  
  Import: #CRUDTemplate.Import
}
```

## See It In Action

Check these files:
- `crud_template.cue` - Full template definition with explanations
- `crud_usage_example.cue` - Complete working examples
- `models.cue` - Resource type definitions
- `responses.cue` - Error and response patterns
