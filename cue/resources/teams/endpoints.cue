// resources/teams/endpoints.cue - Team CRUD endpoint operations (OpenAPI)

package teams

import fw "example.com/apispec/framework"

// Generate complete OpenAPI operation objects for all 8 CRUD + batch operations.
// These are ready to be merged into PathItems in paths.cue.
Endpoints: fw.#OpenAPIEndpoints & {
	#ResourceSchema: "Team"
	#ListItemSchema: "TeamListItem"
	#FilterParams: [
		{name: "isPublic",          in: "query", schema: {type: "boolean"}, description: "Filter by visibility"},
		{name: "ownerIds",          in: "query", schema: {type: "array", items: {type: "string"}}, description: "Filter by owner user ID"},
		{name: "hasMembers.userId", in: "query", schema: {type: "string"}, description: "Filter teams containing this member"},
		{name: "hasMembers.role",   in: "query", schema: {type: "string", enum: ["owner", "member"]}, description: "Filter by member role within team"},
	]
	#BatchCreateRequest: "TeamBatchCreateRequest"
	#BatchUpdateRequest: "TeamBatchUpdateRequest"
	#BatchDeleteRequest: "TeamBatchDeleteRequest"
	operationIdPrefix: "team"
	tag: "teams"
}
