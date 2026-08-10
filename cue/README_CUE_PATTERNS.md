// README_CUE_PATTERNS.md - Guide to the CUE API specification structure

## Overview

This directory demonstrates how to use CUE to define OpenAPI specifications with reusable patterns, filters, and request/response schemas. The structure eliminates duplication and provides strong type constraints through **reference scopes**, **aliases**, and **template composition**.

## File Organization

### `primitives.cue`
Base types used across the entire specification:
- `#Timestamp` - ISO 8601 timestamps
- `#UUID` - UUID identifiers  
- `#ResourceId` - Generic resource identifier
- `#AuditMeta` - Common metadata (createdAt, updatedAt, createdBy)
- HTTP status codes

**Key Pattern**: Primitives are foundational and imported by all other modules.

### `responses.cue`
Reusable response envelopes and patterns:
- `#CreatedResourceEnvelope` - Standard response when creating a resource (just the ID)
- `#MinimalResource` - Simplest resource representation
- `#ErrorDetail` & `#ErrorResponse` - Standardized error responses
- `#PaginatedListEnvelope` - List responses with pagination metadata
- `#BatchOperationResult` - Results of bulk operations
- `#AcknowledgmentResponse` - Success confirmation without data

**Key Pattern**: Many different endpoints reuse the same response envelopes, preventing duplication.

Example: Both `/documents` and `/organizations` creation can return `#CreatedResourceEnvelope`.

### `models.cue`
Query parameter and filter patterns combined with request models:
- `#TextSearchFilter` - Flexible text search with optional fuzzy matching
- `#DateRangeFilter` - Date range with conjunction constraints (startDate < endDate)
- `#StatusFilter` - Enum of possible status values
- `#ComprehensiveDocumentFilter` - Complex filter combining multiple criteria
- `#NumericRangeFilter` - Min/max constraints with validation
- `#TagFilter` - Tag-based filtering with operators (any/all/none)

Request patterns demonstrating advanced CUE features:
- **OneOf Pattern**: Multiple alternatives like `#NotificationChannelRequest` (email | webhook | sms)
- **Conditional Constraints**: Either/or logic for content vs sourceUrl
- **Nested Optionals**: Fields only required when parent field is true

### `crud_template.cue` - **NEW: CRUD Template Pattern**

Generic CRUD template demonstrating **reference scopes** and field-level composition:

**Key Concepts**:
1. **Parameterized Templates**: Generic CRUD operations (Create, Read, List, Update, Delete) with placeholder fields
2. **Reference Scope Binding**: Each resource type specializes the template by unifying concrete types
3. **Batch Operations**: Built-in batch create, update, delete following same pattern as individual operations

**How It Works**:
```cue
// Generic template with placeholder fields (_)
#CRUDTemplate: {
  Create: {
    request: _           // Filled by specialization
    response: {...}      // Consistent error handling
  }
  List: {
    request: {
      filter?: _         // Resource-specific filter
    }
    response: {...}
  }
  ...
}

// Specialize for Documents
#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource
  }
  List: #CRUDTemplate.List & {
    request: filter?: #DocumentFilter
    response: "200": items: [...#DocumentListItem]
  }
  ...
}
```

**Three Resource Examples**:
1. **#DocumentCRUDEndpoints** - Document creation, retrieval, listing, updates, deletion
2. **#UserCRUDEndpoints** - User management with role-based access
3. **#TeamCRUDEndpoints** - Team creation and member management

Each derives from `#CRUDTemplate`, automatically gaining:
- Consistent error responses
- Standard pagination
- Batch operations
- HTTP status codes (201, 400, 404, etc.)

### `endpoints.cue`
Concrete endpoint definitions using shared patterns - shows custom endpoints beyond CRUD.

### `crud_usage_example.cue`
Practical examples showing how to use the CRUD template pattern:
- Instantiated endpoints for Documents, Users, Teams
- API specification registry combining all endpoints
- Shows the full API surface in a structured way

## Key CUE Features Demonstrated

| Feature | File | Example |
|---------|------|---------|
| **Imports** | All | `import "primitives"` |
| **Optional Fields** | models.cue | `description?: string` |
| **Constraints** | models.cue | `int & >=1 & <=100` |
| **Conjunctions** | models.cue | `startDate < endDate` |
| **Unions (OneOf)** | models.cue | `Type: A \| B \| C` |
| **Type Composition** | responses.cue | `#BatchOperationResult & { extra: field }` |
| **Regex Validation** | models.cue | `=~"^[\\w.-]+@"` |
| **Conditional Fields** | models.cue | `field?: type & (condition ? !=[] : _)` |
| **Value Constraints** | primitives.cue | `int & >=1 & <=100` |
| **Reference Scopes** | crud_template.cue | Template fields that specialize per resource |
| **Field-Level Unification** | crud_template.cue | `#Template.Op & { field: ConcreteType }` |
| **Placeholder Resolution** | crud_template.cue | Using `_` to create parameterized templates |

## How to Use

### Validating a document creation request:
```bash
cue eval primitives.cue responses.cue models.cue crud_template.cue crud_usage_example.cue
```

### Exporting to JSON (requires complete concrete values):
```bash
cue export crud_usage_example.cue --out json
```

### Evaluating just the CRUD template:
```bash
cue eval crud_template.cue
```

### Defining a template:
```bash
cue def crud_template.cue
```

## Benefits Over Traditional OpenAPI YAML

1. **DRY Principle** - Define `#ErrorResponse` once, reuse in 50 endpoints
2. **Type Safety** - Regex patterns, min/max constraints enforced at definition time
3. **Composability** - Combine patterns with `&` instead of $ref chains
4. **Readability** - CUE syntax is cleaner than YAML for complex schemas
5. **Validation** - Constraints like "must be lowercase" or "start < end" are clear and enforceable
6. **Modular** - Split patterns logically across files by concern (primitives, filters, requests, responses)
7. **Template Reuse** - CRUD template eliminates 80% of endpoint boilerplate across multiple resources
8. **Reference Scopes** - Templates can be parameterized and specialized without creating circular references

## Real-World Application

This pattern structure scales to large APIs:

**Traditional YAML approach** (repetitive):
```yaml
POST /documents:
  requestBody: {...}
  responses:
    201: {... many fields ...}
    400: {error: {...}}
    409: {error: {...}}

POST /users:
  requestBody: {...}
  responses:
    201: {... many fields ...}   # Copy-pasted
    400: {error: {...}}          # Copy-pasted
    409: {error: {...}}          # Copy-pasted

POST /teams:
  requestBody: {...}
  responses:
    201: {... many fields ...}   # Copy-pasted again
    400: {error: {...}}          # Copy-pasted again
    409: {error: {...}}          # Copy-pasted again
```

**CUE template approach** (DRY):
```cue
#CRUDTemplate: { Create: {...}, Read: {...}, List: {...}, ... }

#DocumentCRUDEndpoints: #CRUDTemplate & { 
  Create.request: #DocumentResource 
}

#UserCRUDEndpoints: #CRUDTemplate & { 
  Create.request: #UserResource 
}

#TeamCRUDEndpoints: #CRUDTemplate & { 
  Create.request: #TeamResource 
}
```

**File breakdown**:
- **primitives.cue** - ~50 base types
- **responses.cue** - ~10 response patterns
- **models.cue** - 50 filter + request patterns
- **crud_template.cue** - 1 generic template (7 operations × 3 resources)
- **endpoints.cue** - Custom endpoints beyond CRUD
- **crud_usage_example.cue** - Usage examples + registry

Result: Full OpenAPI spec in ~800 lines of highly maintainable CUE instead of 10,000+ lines of YAML with duplication.

## Understanding Reference Scopes

The key to the CRUD template working correctly is **reference scope binding**. When you write:

```cue
#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource
  }
}
```

CUE resolves the reference to `#DocumentResource` **in the context where it's used**, not where it's defined. This allows:
- Templates to reference placeholder types (`_`)
- Specializations to provide concrete types
- No circular reference issues
- Clean composition without helper functions

See `crud_template.cue` comments for more details on how aliases and unification work together.

