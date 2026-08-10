// resources/users/paths.cue - OpenAPI path items for users
// PathItems are constructed from Endpoints operations defined in endpoints.cue

package users

// Build path items from Endpoints operations
PathItems: {
	"/users": {
		post: Endpoints.Create
		get: Endpoints.List
	}
	"/users/{id}": {
		get: Endpoints.Read
		put: Endpoints.Update
		delete: Endpoints.Delete
	}
	"/users:batch-create": {
		post: Endpoints.BatchCreate
	}
	"/users:batch-update": {
		patch: Endpoints.BatchUpdate
	}
	"/users:batch": {
		delete: Endpoints.BatchDelete
	}
}

// Tag definition for OpenAPI
Tag: {
	name: "users"
	description: "User account management endpoints"
}
