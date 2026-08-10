// EXAMPLE_SUMMARY.md - Complete guide to the CUE OpenAPI patterns example

## Project Overview

This is a comprehensive example demonstrating how to use **CUE** to define OpenAPI specifications with strong type safety, reduced duplication, and elegant pattern reuse.

Instead of writing thousands of lines of repetitive YAML/JSON, you define patterns once and specialize them for different resources using CUE's powerful composition system.

## Files Included

### Documentation Files

1. **README_CUE_PATTERNS.md** - Main guide covering all file organization and features
   - Overview of the CUE approach vs traditional OpenAPI
   - Explanation of each file's purpose
   - Feature matrix showing what each file demonstrates
   - Real-world scaling examples

2. **CRUD_TEMPLATE_GUIDE.md** - Deep dive into the CRUD template pattern
   - Understanding reference scopes and aliases
   - Structure of each CRUD operation
   - Complete specialization examples
   - How to extend the template
   - Benefits over traditional approaches

3. **EXAMPLE_SUMMARY.md** - This file

### CUE Specification Files

#### Foundation Layer
- **primitives.cue** - Base types (Timestamp, UUID, HTTP status codes, audit metadata)
  - ~50 lines
  - Imported by all other modules

#### Reusable Patterns Layer
- **responses.cue** - Standard response envelopes
  - ErrorResponse, ErrorDetail
  - PaginatedListEnvelope with pagination metadata
  - BatchOperationResult, AcknowledgmentResponse
  - ~60 lines

- **models.cue** - Filters and request patterns
  - TextSearchFilter (with fuzzy search)
  - DateRangeFilter (with conjunction constraints)
  - StatusFilter, ComprehensiveDocumentFilter
  - Notification channel alternatives (Email | Webhook | SMS) - OneOf pattern
  - CreateDocumentRequest, BulkUpdateDocumentsRequest
  - DocumentAccessRequest (with conditional fields)
  - ~140 lines

#### Template Layer
- **crud_template.cue** - Generic CRUD template
  - Generic Create, Read, List, Update, Delete operations
  - BatchCreate, BatchUpdate, BatchDelete operations
  - Placeholders for resource-specific types
  - Three complete specializations: Document, User, Team
  - ~320 lines
  - Shows reference scopes and field-level composition

#### Endpoint and Usage Layer
- **endpoints.cue** - Custom endpoints beyond CRUD
  - Specialized endpoint definitions
  - Demonstrates parameter composition
  - Shows advanced filter + response patterns
  - ~130 lines

- **crud_usage_example.cue** - Concrete instantiated endpoints
  - Document, User, Team endpoint instantiation
  - Full API registry combining all endpoints
  - Shows how to create the final API spec
  - ~80 lines

#### Legacy/Reference
- **filters.cue** - Original separate filters file (redundant with models.cue)
- **requests.cue** - Original separate requests file (redundant with models.cue)
- **simple.cue** - Original auto-generated specification (for reference)

## Key Concepts Demonstrated

### 1. Type Constraints and Validation
```cue
#DocumentMetadata: {
  title: string & !=""                    // Non-empty string
  tags?: [...string]                      // Optional list
  isPublic?: bool                         // Optional boolean
}

#DocumentFilter: {
  pageNumber?: int & >=1                  // Minimum 1
  pageSize?: int & >=1 & <=100            // Range constraint
}
```

### 2. Union Types (OneOf)
```cue
#NotificationChannelEmail: { type: "email", emailAddress: string }
#NotificationChannelWebhook: { type: "webhook", webhookUrl: string }
#NotificationChannelSms: { type: "sms", phoneNumber: string }

#NotificationChannelRequest: 
  #NotificationChannelEmail | #NotificationChannelWebhook | #NotificationChannelSms
```

### 3. Type Composition (Unification)
```cue
#CreateDocumentRequest: {
  title: string & !=""
  description?: string
  contentType: "pdf" | "docx"
  // Combine with base metadata
  tags?: [...string]
  isPublic?: bool
}
```

### 4. Reusable Response Patterns
```cue
#ErrorResponse: {
  error: {
    code: string
    message: string
    field?: string
  }
  timestamp?: string
}

// Reused in 50+ endpoints
```

### 5. Reference Scopes and Aliases
```cue
#CRUDTemplate: {
  Create: {
    request: _                            // Placeholder
    response: {
      "201": { resourceId: string }
      "400": #ErrorResponse               // Consistently reused
    }
  }
}

#DocumentCRUDEndpoints: {
  Create: #CRUDTemplate.Create & {
    request: #DocumentResource            // Specialized here
  }
}
```

The magic: The reference to `#DocumentResource` resolves **in the context where it's used**, not where the template is defined. This enables clean template specialization.

### 6. Optional Fields with Constraints
```cue
#DocumentAccessRequest: {
  documentId: string
  accessLevel: #AuthorizationLevelFilter
  requireApproval?: bool
  approverEmails?: [...string]            // Optional
  reason?: string
}

// If requireApproval is true, approverEmails should not be empty
```

## File Dependency Graph

```
primitives.cue
    ↓
responses.cue
    ↓
models.cue
    ↓
crud_template.cue
    ↓
crud_usage_example.cue
    ↓
endpoints.cue (can depend on all above)
```

Each layer builds on the previous one:
- Primitives: Basic types
- Responses: Standard envelopes
- Models: Filters and request bodies
- CRUD Template: Generic operations
- Usage Example: Concrete endpoints
- Endpoints: Custom specialized endpoints

## How to Use This Example

### 1. View and Understand Structure
```bash
cd C:\i3\git\cue\cue-api-dotnet\cue
cat README_CUE_PATTERNS.md          # Overview
cat CRUD_TEMPLATE_GUIDE.md          # Deep dive
```

### 2. Validate CUE Syntax
```bash
cue eval primitives.cue
cue eval models.cue
cue eval crud_template.cue
cue eval crud_usage_example.cue
```

### 3. Evaluate All Together
```bash
cue eval primitives.cue responses.cue models.cue crud_template.cue crud_usage_example.cue endpoints.cue
```

### 4. Export to JSON
```bash
# Requires fully concrete values (no incomplete types)
cue export crud_usage_example.cue --out json
```

### 5. Get Type Definitions
```bash
cue def crud_template.cue | head -50
```

## Pattern Highlights

### Pattern 1: Error Handling
Every endpoint has consistent error responses:
- 400 Bad Request
- 401 Unauthorized  
- 403 Forbidden
- 404 Not Found
- 409 Conflict
- 422 Validation Failed

All use the same `#ErrorResponse` structure.

### Pattern 2: Pagination
List endpoints follow the same pagination pattern:
```cue
{
  items: [...]
  pagination: {
    pageNumber: int
    pageSize: int
    totalCount: int
    hasMore: bool
  }
}
```

### Pattern 3: Batch Operations
Bulk operations (create/update/delete) support:
- Partial success (207 Multi-Status response)
- Continue-on-error mode
- Per-item error reporting
- Dry-run preview

### Pattern 4: Filtering
Complex filters are composable:
- Optional text search
- Optional date ranges
- Optional status filters
- Optional pagination
- Optional sort parameters

### Pattern 5: Notifications
Multiple notification channels are supported via OneOf:
- Email delivery
- Webhook POST
- SMS messages

Client chooses one; schema enforces exactly one.

## Real-World Scaling

To add a new resource (e.g., Projects):

1. Define resource schema:
```cue
#ProjectResource: { ... }
#ProjectListItem: { ... }
#ProjectFilter: { ... }
```

2. Specialize the template:
```cue
#ProjectCRUDEndpoints: {
  Create: #CRUDTemplate.Create & { request: #ProjectResource }
  Read: #CRUDTemplate.Read & { response: "200": #ProjectResource }
  List: #CRUDTemplate.List & { ... }
  // ... 5 more operations
}
```

3. Use in API spec:
```cue
ProjectCreateAPI: #ProjectCRUDEndpoints.Create
ProjectListAPI: #ProjectCRUDEndpoints.List
// ... etc
```

**That's it!** You get 8 fully-typed endpoints (Create, Read, List, Update, Delete, BatchCreate, BatchUpdate, BatchDelete) with consistent error handling, pagination, and all patterns.

Compare to YAML: You'd need to copy ~500 lines and manually maintain consistency.

## Benefits Summary

| Aspect | CUE | YAML |
|--------|-----|------|
| **DRY Principle** | ✅ Define once, reuse everywhere | ❌ Repetition and copy-paste |
| **Type Safety** | ✅ Constraints enforced at definition | ❌ Constraints only in docs |
| **Composability** | ✅ Elegant `&` operator for unification | ❌ Complex $ref chains |
| **Readability** | ✅ Clean syntax for complex schemas | ❌ Deeply nested YAML |
| **Maintainability** | ✅ Change pattern once, all endpoints updated | ❌ Manual sync across many files |
| **Validation** | ✅ Can validate and transform | ❌ Mostly read-only |
| **Scalability** | ✅ Add 50 resources, ~50 specializations | ❌ Add 50 resources, ~5000 lines |

## Further Reading

- [CUE Official Tour](https://cuelang.org/docs/tour)
- [Alias and Reference Scopes](https://cuelang.org/docs/concept/alias-and-reference-scopes/)
- [CUE Language Documentation](https://cuelang.org/docs/)

## File Statistics

| File | Lines | Purpose |
|------|-------|---------|
| primitives.cue | ~50 | Base types |
| responses.cue | ~60 | Standard envelopes |
| models.cue | ~140 | Filters & requests |
| crud_template.cue | ~320 | Generic CRUD template |
| crud_usage_example.cue | ~80 | Concrete endpoints |
| endpoints.cue | ~130 | Custom endpoints |
| README_CUE_PATTERNS.md | ~250 | Main documentation |
| CRUD_TEMPLATE_GUIDE.md | ~350 | Template deep dive |
| **TOTAL** | **~1,380** | **Full API spec** |

Equivalent YAML would be 8,000+ lines with significant duplication.

## Next Steps

1. **Explore the files** - Start with README_CUE_PATTERNS.md
2. **Run the examples** - Use `cue eval` to validate
3. **Modify patterns** - Try changing constraint values or adding fields
4. **Add a new resource** - Define your own resource type and specialize the CRUD template
5. **Export to JSON** - Try `cue export crud_usage_example.cue --out json`
