// resources/documents/paths.cue - OpenAPI path items for documents
// PathItems are constructed from Endpoints operations defined in endpoints.cue

package documents

// Build path items from Endpoints operations
PathItems: {
	"/documents": {
		post: Endpoints.Create
		get: Endpoints.List
	}
	"/documents/{id}": {
		get: Endpoints.Read
		put: Endpoints.Update
		delete: Endpoints.Delete
	}
	"/documents:batch-create": {
		post: Endpoints.BatchCreate
	}
	"/documents:batch-update": {
		patch: Endpoints.BatchUpdate
	}
	"/documents:batch": {
		delete: Endpoints.BatchDelete
	}
}

// Tag definition for OpenAPI
Tag: {
	name: "documents"
	description: "Document management endpoints"
}
