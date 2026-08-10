// resources/documents/endpoints.cue - Document CRUD endpoint operations (OpenAPI)

package documents

import fw "example.com/apispec/framework"

// Generate complete OpenAPI operation objects for all 8 CRUD + batch operations.
// These are ready to be merged into PathItems in paths.cue.
Endpoints: fw.#OpenAPIEndpoints & {
	#ResourceSchema: "Document"
	#ListItemSchema: "DocumentListItem"
	#FilterParams: [
		{name: "statusIn", in: "query", schema: {type: "array", items: {type: "string", enum: ["draft", "published", "archived"]}}, description: "Filter by one or more statuses"},
		{name: "isPublic", in: "query", schema: {type: "boolean"}, description: "Filter by visibility"},
		{name: "tagIds",   in: "query", schema: {type: "array", items: {type: "string"}}, description: "Filter by tag IDs"},
	]
	#BatchCreateRequest: "DocumentBatchCreateRequest"
	#BatchUpdateRequest: "DocumentBatchUpdateRequest"
	#BatchDeleteRequest: "DocumentBatchDeleteRequest"
	operationIdPrefix: "document"
	tag: "documents"
}
