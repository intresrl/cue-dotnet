package paths

import (
	F "example.com/apispec/framework"
)

// Tag definition for OpenAPI
_users: Tag: {
	name:        "users"
	description: "User account management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	userId: {
		name:     "userId"
		in:       "path"
		required: true
		schema: {type: "string", description: "User ID"}
	}
}

_schemas: F.#SchemaRefs & {
	User:                   _
	UserListResponse:       _
	ErrorResponse:          _
	UserListItem:           _
	UserBatchCreateRequest: _
	BatchCreateResponse:    _
	UserBatchUpdateRequest: _
	BatchUpdateResponse:    _
	UserBatchDeleteRequest: _
	BatchDeleteResponse:    _
}

#UserPaths: [_]: {
	name: _users.Tag.name
}

// PathItems defines all user REST endpoints
paths: {
	...
	"/users": {
		post: {
			operationId: "createUser"
			summary:     "Create a user"
			requestBody: {
				required: true
				content:  _schemas.User
			}
			responses: F.#R400 & F.#R422 & {
				"201": {
					description: "User created"
					content:     _schemas.User
				}
			}
		}
		get: {
			operationId: "listUsers"
			summary:     "List users"
			parameters: F.#Pagination
				//{"$ref": "#/components/parameters/user_filter"}
			responses: F.#R400 & {
				"200": {
					description: "User list"
					content:     _schemas.UserListResponse
				}
			}
		}
	}
	"/users/{userId}": {
		get: {
			operationId: "getUser"
			summary:     "Get a user"
			parameters: [#PathParams.userId]
			responses: {
				"200": {
					description: "User details"
					content:     _schemas.User
				}
				"404": {
					description: "Not found"
					content:     _schemas.ErrorResponse
				}
			}
		}
		put: {
			operationId: "updateUser"
			summary:     "Update a user"
			parameters: [#PathParams.userId]
			requestBody: {
				required: true
				content:  _schemas.User
			}
			responses: {
				"200": {
					description: "User updated"
					content:     _schemas.User
				}
				"404": {
					description: "Not found"
					content:     _schemas.ErrorResponse
				}
				"409": {
					description: "Conflict"
					content:     _schemas.ErrorResponse
				}
			}
		}
		delete: {
			operationId: "deleteUser"
			summary:     "Delete a user"
			parameters: [#PathParams.userId]
			responses: {
				"204": {
					description: "User deleted"
				}
				"404": {
					description: "Not found"
					content:     _schemas.ErrorResponse
				}
			}
		}
	}
	"/users:batch-create": {
		post: {
			operationId: "batchCreateUsers"
			summary:     "Batch create users"
			requestBody: {
				required: true
				content:  _schemas.UserBatchCreateRequest
			}
			responses: F.#R400 & {
				"207": {
					description: "Batch creation result"
					content:     _schemas.BatchCreateResponse
				}
			}
		}
	}
	"/users:batch-update": {
		patch: {
			operationId: "batchUpdateUsers"
			summary:     "Batch update users"
			requestBody: {
				required: true
				content:  _schemas.UserBatchUpdateRequest
			}
			responses: F.#R400 & F.#R422 & {
				"200": {
					description: "Batch update result"
					content:     _schemas.BatchUpdateResponse
				}
			}
		}
	}
	"/users:batch": {
		delete: {
			operationId: "batchDeleteUsers"
			summary:     "Batch delete users"
			requestBody: {
				required: true
				content:  _schemas.UserBatchDeleteRequest
			}
			responses: F.#R400 & {
				"200": {
					description: "Batch delete result"
					content:     _schemas.BatchDeleteResponse
				}
			}
		}
	}
}
