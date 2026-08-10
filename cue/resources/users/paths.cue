// resources/users/paths.cue - OpenAPI path items for users

package users

// Tag definition for OpenAPI
Tag: {
	name: "users"
	description: "User account management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	id: {
		name: "id"
		in: "path"
		required: true
		schema: {type: "string", description: "User ID"}
	}
}

// PathItems defines all user REST endpoints
PathItems: {
	"/users": {
		post: {
			operationId: "createUser"
			summary: "Create a user"
			tags: ["users"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/User"}}}
			}
			responses: {
				"201": {description: "User created"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
		get: {
			operationId: "listUsers"
			summary: "List users"
			tags: ["users"]
			parameters: [
				{name: "filter", in: "query", schema: {"$ref": "#/components/schemas/UserFilter"}, description: "Filter users"}
				{name: "pageNumber", in: "query", schema: {type: "integer"}, description: "Page number (1-based)"}
				{name: "pageSize", in: "query", schema: {type: "integer"}, description: "Items per page"}
				{name: "sortBy", in: "query", schema: {type: "string"}, description: "Sort by field"}
				{name: "sortDirection", in: "query", schema: {type: "string", enum: ["asc", "desc"]}, description: "Sort direction"}
			]
			responses: {
				"200": {description: "User list"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/users/{id}": {
		get: {
			operationId: "getUser"
			summary: "Get a user"
			tags: ["users"]
			parameters: [#PathParams.id]
			responses: {
				"200": {description: "User details"}
				"404": {description: "Not found"}
			}
		}
		put: {
			operationId: "updateUser"
			summary: "Update a user"
			tags: ["users"]
			parameters: [#PathParams.id]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/User"}}}
			}
			responses: {
				"200": {description: "User updated"}
				"404": {description: "Not found"}
				"409": {description: "Conflict"}
			}
		}
		delete: {
			operationId: "deleteUser"
			summary: "Delete a user"
			tags: ["users"]
			parameters: [#PathParams.id]
			responses: {
				"204": {description: "User deleted"}
				"404": {description: "Not found"}
			}
		}
	}
	"/users:batch-create": {
		post: {
			operationId: "batchCreateUsers"
			summary: "Batch create users"
			tags: ["users"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/UserBatchCreateRequest"}}}
			}
			responses: {
				"207": {description: "Batch creation result"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/users:batch-update": {
		patch: {
			operationId: "batchUpdateUsers"
			summary: "Batch update users"
			tags: ["users"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/UserBatchUpdateRequest"}}}
			}
			responses: {
				"200": {description: "Batch update result"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
	}
	"/users:batch": {
		delete: {
			operationId: "batchDeleteUsers"
			summary: "Batch delete users"
			tags: ["users"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/UserBatchDeleteRequest"}}}
			}
			responses: {
				"200": {description: "Batch delete result"}
				"400": {description: "Bad request"}
			}
		}
	}
}
