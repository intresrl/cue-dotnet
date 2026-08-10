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
				"201": {
					description: "User created"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/User"}}}
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
			operationId: "listUsers"
			summary: "List users"
			tags: ["users"]
			parameters: [
				{"$ref": "#/components/parameters/user_filter"}
				{"$ref": "#/components/parameters/pageNumber"}
				{"$ref": "#/components/parameters/pageSize"}
				{"$ref": "#/components/parameters/sortBy"}
				{"$ref": "#/components/parameters/sortDirection"}
			]
			responses: {
				"200": {
					description: "User list"
					content: {"application/json": {schema: {
						type: "object"
						properties: {
							items: {type: "array", items: {"$ref": "#/components/schemas/UserListItem"}}
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
	"/users/{id}": {
		get: {
			operationId: "getUser"
			summary: "Get a user"
			tags: ["users"]
			parameters: [#PathParams.id]
			responses: {
				"200": {
					description: "User details"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/User"}}}
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
				"200": {
					description: "User updated"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/User"}}}
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
			operationId: "deleteUser"
			summary: "Delete a user"
			tags: ["users"]
			parameters: [#PathParams.id]
			responses: {
				"204": {
					description: "User deleted"
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
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
