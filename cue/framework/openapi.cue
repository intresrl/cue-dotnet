// framework/openapi.cue - Shared OpenAPI building-blocks for all resource path files.
//
// CRUDPaths generates all 8 standard path items for a resource given a ResourceSpec.
// Resource packages supply only their spec; the framework provides all operation structure.

package framework

import "list"

// ---------------------------------------------------------------------------
// ResourceSpec — the only thing each resource package needs to define
// ---------------------------------------------------------------------------

// ResourceSpec parameterises CRUDPaths. Fill every field in the resource's paths.cue.
#ResourceSpec: {
	// tag is the OpenAPI tag (e.g. "documents") and used in summaries.
	tag: string
	// single is the PascalCase schema name (e.g. "Document").
	single: string
	// listItem is the schema name for list responses (e.g. "DocumentListItem").
	listItem: string
	// basePath is the collection URL (e.g. "/documents").
	basePath: string
	// tagDescription is shown in the tags section of the OpenAPI doc.
	tagDescription: string
	// extraFilterParams are resource-specific query parameters merged into the List operation.
	extraFilterParams: [...{}]
}

// ---------------------------------------------------------------------------
// CRUDPaths generates PathItems for all 8 standard CRUD + batch operations.
// Usage in a resource paths.cue:
//   _crud: fw.CRUDPaths & { spec: { tag: "documents", single: "Document", ... } }
//   PathItems: _crud.PathItems
//   Tag:       _crud.Tag
// ---------------------------------------------------------------------------

CRUDPaths: {
	// spec is a public field so it can be set from any resource package via &.
	// (Hidden fields _spec would be scoped to package framework and unreachable.)
	spec: #ResourceSpec

	let S = spec
	let tag      = S.tag
	let single   = S.single
	let listItem = S.listItem
	let base     = S.basePath

	// OpenAPI tag entry — surfaced as Tag in the resource package.
	Tag: {name: tag, description: S.tagDescription}

	// Assembled path items.
	PathItems: {
		// ── Collection: POST (create) + GET (list) ───────────────────────────
		(base): {
			post: {
				summary:     "Create \(single)"
				operationId: "create\(single)"
				tags: [tag]
				requestBody: {
					required: true
					content: "application/json": schema: "$ref": "#/components/schemas/\(single)"
				}
				responses: {
					"201": {description: "Created", content: "application/json": schema: {
						type: "object"
						required: ["resourceId"]
						properties: resourceId: {type: "string", description: "ID of the created \(single)"}
					}}
					"400": _errResp & {_d: "Validation error"}
					"409": _errResp & {_d: "Conflict – resource already exists"}
					"422": _errResp & {_d: "Unprocessable entity"}
				}
			}
			get: {
				summary:     "List \(tag)"
				operationId: "list\(single)s"
				tags: [tag]
				parameters: list.Concat([CommonListParams, S.extraFilterParams])
				responses: {
					"200": {description: "OK", content: "application/json": schema: {
						type: "object"
						required: ["items", "pagination"]
						properties: {
							items:      {type: "array", items: "$ref": "#/components/schemas/\(listItem)"}
							pagination: "$ref": "#/components/schemas/PaginationMeta"
						}
					}}
					"400": _errResp & {_d: "Invalid query parameters"}
					"401": _errResp & {_d: "Unauthorized"}
				}
			}
		}

		// ── Single item: GET (read) + PUT (update) + DELETE ──────────────────
		"\(base)/{id}": {
			get: {
				summary:     "Get \(single) by ID"
				operationId: "get\(single)"
				tags: [tag]
				parameters: [IdParam]
				responses: {
					"200": {description: "OK", content: "application/json": schema: "$ref": "#/components/schemas/\(single)"}
					"400": _errResp & {_d: "Invalid ID"}
					"401": _errResp & {_d: "Unauthorized"}
					"404": _errResp & {_d: "Not found"}
				}
			}
			put: {
				summary:     "Replace \(single)"
				operationId: "update\(single)"
				tags: [tag]
				parameters: [IdParam]
				requestBody: {
					required: true
					content: "application/json": schema: "$ref": "#/components/schemas/\(single)"
				}
				responses: {
					"200": {description: "OK", content: "application/json": schema: "$ref": "#/components/schemas/\(single)"}
					"400": _errResp & {_d: "Validation error"}
					"401": _errResp & {_d: "Unauthorized"}
					"404": _errResp & {_d: "Not found"}
					"409": _errResp & {_d: "Conflict"}
				}
			}
			delete: {
				summary:     "Delete \(single)"
				operationId: "delete\(single)"
				tags: [tag]
				parameters: [IdParam]
				responses: {
					"204": {description: "Deleted successfully"}
					"400": _errResp & {_d: "Invalid ID"}
					"401": _errResp & {_d: "Unauthorized"}
					"404": _errResp & {_d: "Not found"}
				}
			}
		}

		// ── Batch create ─────────────────────────────────────────────────────
		"\(base):batch-create": {
			post: {
				summary:     "Batch create \(tag)"
				operationId: "batchCreate\(single)s"
				tags: [tag]
				requestBody: {
					required: true
					content: "application/json": schema: "$ref": "#/components/schemas/\(single)BatchCreateRequest"
				}
				responses: {
					"207": {description: "Multi-status batch result", content: "application/json": schema: "$ref": "#/components/schemas/BatchCreateResponse"}
					"400": _errResp & {_d: "Validation error"}
				}
			}
		}

		// ── Batch update ─────────────────────────────────────────────────────
		"\(base):batch-update": {
			patch: {
				summary:     "Batch update \(tag)"
				operationId: "batchUpdate\(single)s"
				tags: [tag]
				requestBody: {
					required: true
					content: "application/json": schema: "$ref": "#/components/schemas/\(single)BatchUpdateRequest"
				}
				responses: {
					"200": {description: "Batch update result", content: "application/json": schema: "$ref": "#/components/schemas/BatchUpdateResponse"}
					"400": _errResp & {_d: "Validation error"}
					"422": _errResp & {_d: "Unprocessable entity"}
				}
			}
		}

		// ── Batch delete ─────────────────────────────────────────────────────
		"\(base):batch": {
			delete: {
				summary:     "Batch delete \(tag)"
				operationId: "batchDelete\(single)s"
				tags: [tag]
				requestBody: {
					required: true
					content: "application/json": schema: "$ref": "#/components/schemas/\(single)BatchDeleteRequest"
				}
				responses: {
					"200": {description: "Batch delete result", content: "application/json": schema: "$ref": "#/components/schemas/BatchDeleteResponse"}
					"400": _errResp & {_d: "Validation error"}
				}
			}
		}
	}
}

// ---------------------------------------------------------------------------
// Shared query parameter lists
// ---------------------------------------------------------------------------

PaginationParams: [
	{name: "pageNumber",    in: "query", schema: {type: "integer", minimum: 1},              description: "Page number (1-based)"},
	{name: "pageSize",      in: "query", schema: {type: "integer", minimum: 1, maximum: 100}, description: "Items per page"},
	{name: "sortBy",        in: "query", schema: {type: "string"},                           description: "Field to sort by"},
	{name: "sortDirection", in: "query", schema: {type: "string", enum: ["asc", "desc"]},    description: "Sort direction"},
]

SearchParams: [
	{name: "search.query",         in: "query", schema: {type: "string"},  description: "Full-text search query"},
	{name: "search.caseSensitive", in: "query", schema: {type: "boolean"}, description: "Case-sensitive search"},
	{name: "search.fuzzy",         in: "query", schema: {type: "boolean"}, description: "Fuzzy matching"},
]

DateRangeParams: [
	{name: "dateRange.from", in: "query", schema: {type: "string", format: "date-time"}, description: "Earliest createdAt to include"},
	{name: "dateRange.to",   in: "query", schema: {type: "string", format: "date-time"}, description: "Latest createdAt to include"},
]

CommonListParams: list.Concat([PaginationParams, SearchParams, DateRangeParams])

// ---------------------------------------------------------------------------
// Shared path parameter
// ---------------------------------------------------------------------------

IdParam: {
	name:        "id"
	in:          "path"
	required:    true
	description: "Resource identifier"
	schema: type: "string"
}

// ---------------------------------------------------------------------------
// Shared response building-blocks
// ---------------------------------------------------------------------------

ErrorBody: {
	"application/json": schema: "$ref": "#/components/schemas/ErrorResponse"
}

// _errResp is package-private: used inside CRUDPaths above.
_errResp: {_d: string, description: _d, content: ErrorBody}
