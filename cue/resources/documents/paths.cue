// resources/documents/paths.cue - OpenAPI path items for documents

package documents

// Tag definition for OpenAPI
Tag: {
	name: "documents"
	description: "Document management endpoints"
}

// PathItems defines all document REST endpoints
PathItems: {
	"/documents": {
		post: {
			operationId: "createDocument"
			summary: "Create a document"
			tags: ["documents"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
			}
			responses: {
				"201": {description: "Document created"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
		get: {
			operationId: "listDocuments"
			summary: "List documents"
			tags: ["documents"]
			parameters: [
				{name: "pageNumber", in: "query", schema: {type: "integer"}}
				{name: "pageSize", in: "query", schema: {type: "integer"}}
				{name: "sortBy", in: "query", schema: {type: "string"}}
			]
			responses: {
				"200": {description: "Document list"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/documents/{id}": {
		get: {
			operationId: "getDocument"
			summary: "Get a document"
			tags: ["documents"]
			parameters: [{name: "id", in: "path", required: true, schema: {type: "string"}}]
			responses: {
				"200": {description: "Document details"}
				"404": {description: "Not found"}
			}
		}
		put: {
			operationId: "updateDocument"
			summary: "Update a document"
			tags: ["documents"]
			parameters: [{name: "id", in: "path", required: true, schema: {type: "string"}}]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
			}
			responses: {
				"200": {description: "Document updated"}
				"404": {description: "Not found"}
				"409": {description: "Conflict"}
			}
		}
		delete: {
			operationId: "deleteDocument"
			summary: "Delete a document"
			tags: ["documents"]
			parameters: [{name: "id", in: "path", required: true, schema: {type: "string"}}]
			responses: {
				"204": {description: "Document deleted"}
				"404": {description: "Not found"}
			}
		}
	}
	"/documents:batch-create": {
		post: {
			operationId: "batchCreateDocuments"
			summary: "Batch create documents"
			tags: ["documents"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/DocumentBatchCreateRequest"}}}
			}
			responses: {
				"207": {description: "Batch creation result"}
				"400": {description: "Bad request"}
			}
		}
	}
	"/documents:batch-update": {
		patch: {
			operationId: "batchUpdateDocuments"
			summary: "Batch update documents"
			tags: ["documents"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/DocumentBatchUpdateRequest"}}}
			}
			responses: {
				"200": {description: "Batch update result"}
				"400": {description: "Bad request"}
				"422": {description: "Validation error"}
			}
		}
	}
	"/documents:batch": {
		delete: {
			operationId: "batchDeleteDocuments"
			summary: "Batch delete documents"
			tags: ["documents"]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/DocumentBatchDeleteRequest"}}}
			}
			responses: {
				"200": {description: "Batch delete result"}
				"400": {description: "Bad request"}
			}
		}
	}
}
