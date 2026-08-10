// resources/teams/paths.cue - OpenAPI path items for teams

package teams

// Tag definition for OpenAPI
Tag: {
	name: "teams"
	description: "Team management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	id: {
		name: "id"
		in: "path"
		required: true
		schema: {type: "string", description: "Team ID"}
	}
}

// PathItems defines all team REST endpoints
PathItems: {
	"/teams": {
		post: {
			operationId: "createTeam"
			summary: "Create a team"
			tags: ["teams"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/Team"}}}
			}
			responses: {
				"201": {description: "Team created"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
		get: {
			operationId: "listTeams"
			summary: "List teams"
			tags: ["teams"]
			parameters: [
				{name: "filter", in: "query", schema: {"$ref": "#/components/schemas/TeamFilter"}, description: "Filter teams"}
				{name: "pageNumber", in: "query", schema: {type: "integer"}, description: "Page number (1-based)"}
				{name: "pageSize", in: "query", schema: {type: "integer"}, description: "Items per page"}
				{name: "sortBy", in: "query", schema: {type: "string"}, description: "Sort by field"}
				{name: "sortDirection", in: "query", schema: {type: "string", enum: ["asc", "desc"]}, description: "Sort direction"}
			]
			responses: {
				"200": {description: "Team list"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/teams/{id}": {
		get: {
			operationId: "getTeam"
			summary: "Get a team"
			tags: ["teams"]
			parameters: [#PathParams.id]
			responses: {
				"200": {description: "Team details"}
				"404": {description: "Not found"}
			}
		}
		put: {
			operationId: "updateTeam"
			summary: "Update a team"
			tags: ["teams"]
			parameters: [#PathParams.id]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/Team"}}}
			}
			responses: {
				"200": {description: "Team updated"}
				"404": {description: "Not found"}
				"409": {description: "Conflict"}
			}
		}
		delete: {
			operationId: "deleteTeam"
			summary: "Delete a team"
			tags: ["teams"]
			parameters: [#PathParams.id]
			responses: {
				"204": {description: "Team deleted"}
				"404": {description: "Not found"}
			}
		}
	}
	"/teams:batch-create": {
		post: {
			operationId: "batchCreateTeams"
			summary: "Batch create teams"
			tags: ["teams"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/TeamBatchCreateRequest"}}}
			}
			responses: {
				"207": {description: "Batch creation result"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/teams:batch-update": {
		patch: {
			operationId: "batchUpdateTeams"
			summary: "Batch update teams"
			tags: ["teams"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/TeamBatchUpdateRequest"}}}
			}
			responses: {
				"200": {description: "Batch update result"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
	}
	"/teams:batch": {
		delete: {
			operationId: "batchDeleteTeams"
			summary: "Batch delete teams"
			tags: ["teams"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/TeamBatchDeleteRequest"}}}
			}
			responses: {
				"200": {description: "Batch delete result"}
				"400": {description: "Bad request"}
			}
		}
	}
}
