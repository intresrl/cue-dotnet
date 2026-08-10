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
				"201": {
					description: "Team created"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Team"}}}
				}
				"400": {
					description: "Bad request"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
				"422": {
					description: "Validation error"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "Team list"
					content: {"application/json": {schema: {
						type: "object"
						properties: {
							items: {type: "array", items: {"$ref": "#/components/schemas/TeamListItem"}}
							pagination: {"$ref": "#/components/schemas/PaginationMeta"}
						}
					}}}
				}
				"400": {
					description: "Bad request"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "Team details"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Team"}}}
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "Team updated"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Team"}}}
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
				"409": {
					description: "Conflict"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
			}
		}
		delete: {
			operationId: "deleteTeam"
			summary: "Delete a team"
			tags: ["teams"]
			parameters: [#PathParams.id]
			responses: {
				"204": {
					description: "Team deleted"
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"207": {
					description: "Batch creation result"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/BatchCreateResponse"}}}
				}
				"400": {
					description: "Bad request"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "Batch update result"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/BatchUpdateResponse"}}}
				}
				"400": {
					description: "Bad request"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
				"422": {
					description: "Validation error"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "Batch delete result"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/BatchDeleteResponse"}}}
				}
				"400": {
					description: "Bad request"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
			}
		}
	}
}
