// resources/users/endpoints.cue - User CRUD endpoint operations (OpenAPI)

package users

import fw "example.com/apispec/framework"

// Generate complete OpenAPI operation objects for all 8 CRUD + batch operations.
// These are ready to be merged into PathItems in paths.cue.
Endpoints: fw.#OpenAPIEndpoints & {
	#ResourceSchema: "User"
	#ListItemSchema: "UserListItem"
	#FilterParams: [
		{name: "search.fields", in: "query", schema: {type: "array", items: {type: "string", enum: ["email", "firstName", "lastName"]}}, description: "Fields to search within"},
		{name: "roleIn",        in: "query", schema: {type: "array", items: {type: "string", enum: ["admin", "editor", "viewer"]}}, description: "Filter by one or more roles"},
		{name: "isActive",      in: "query", schema: {type: "boolean"}, description: "Filter by active status"},
	]
	#BatchCreateRequest: "UserBatchCreateRequest"
	#BatchUpdateRequest: "UserBatchUpdateRequest"
	#BatchDeleteRequest: "UserBatchDeleteRequest"
	operationIdPrefix: "user"
	tag: "users"
}
