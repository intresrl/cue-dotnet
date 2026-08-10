package paths

import (
    F  "example.com/apispec/framework"
)

// Tag definition for OpenAPI
_teams: Tag: {
	name: "teams"
	description: "Team management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	teamId: {
		name: "teamId"
		in: "path"
		required: true
		schema: {type: "string", description: "Team ID"}
	}
}

_schemaRefs: F.#SchemaRefs & {
    Team: _
    TeamListResponse: _
    ErrorResponse: _
    TeamListItem: _
    TeamBatchCreateRequest: _
    BatchCreateResponse: _
    TeamBatchUpdateRequest: _
    BatchUpdateResponse: _
    TeamBatchDeleteRequest: _
    BatchDeleteResponse: _
}

// PathItems defines all team REST endpoints
paths: {
    ...,
	"/teams": {
		post: {
			operationId: "createTeam"
			summary: "Create a team"
			tags: ["teams"]
			requestBody: {
				required: true
				content: _schemaRefs.Team
			}
			responses: F.#R400 & F.#R422 & {
				"201": {
					description: "Team created"
					content: _schemaRefs.Team
				}
			}
		}
		get: {
			operationId: "listTeams"
			summary: "List teams"
			tags: ["teams"]
			parameters: F.#Pagination
				//{"$ref": "#/components/parameters/team_filter"}
			responses: F.#R400 & {
				"200": {
					description: "Team list"
					content: _schemaRefs.TeamListResponse
				}
			}
		}
	}
	"/teams/{teamId}": {
		get: {
			operationId: "getTeam"
			summary: "Get a team"
			tags: ["teams"]
			parameters: [#PathParams.teamId]
			responses: {
				"200": {
					description: "Team details"
					content: _schemaRefs.Team
				}
				"404": {
					description: "Not found"
					content: _schemaRefs.ErrorResponse
				}
			}
		}
		put: {
			operationId: "updateTeam"
			summary: "Update a team"
			tags: ["teams"]
			parameters: [#PathParams.teamId]
			requestBody: {
				required: true
				content: _schemaRefs.Team
			}
			responses: {
				"200": {
					description: "Team updated"
					content: _schemaRefs.Team
				}
				"404": {
					description: "Not found"
					content: _schemaRefs.ErrorResponse
				}
				"409": {
					description: "Conflict"
					content: _schemaRefs.ErrorResponse
				}
			}
		}
		delete: {
			operationId: "deleteTeam"
			summary: "Delete a team"
			tags: ["teams"]
			parameters: [#PathParams.teamId]
			responses: {
				"204": {
					description: "Team deleted"
				}
				"404": {
					description: "Not found"
					content: _schemaRefs.ErrorResponse
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
				content: _schemaRefs.TeamBatchCreateRequest
			}
			responses: F.#R400 & {
				"207": {
					description: "Batch creation result"
					content: _schemaRefs.BatchCreateResponse
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
				content: _schemaRefs.TeamBatchUpdateRequest
			}
			responses: F.#R400 & F.#R422 & {
				"200": {
					description: "Batch update result"
					content: _schemaRefs.BatchUpdateResponse
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
				content: _schemaRefs.TeamBatchDeleteRequest
			}
			responses: F.#R400 & {
				"200": {
					description: "Batch delete result"
					content: _schemaRefs.BatchDeleteResponse
				}
			}
		}
	}
}
