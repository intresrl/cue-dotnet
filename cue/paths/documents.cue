package paths

import (
    F  "example.com/apispec/framework"
)

// Tag definition for OpenAPI
_documents: Tag: {
	name: "documents"
	description: "Document management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	documentId: {
		name: "documentId"
		in: "path"
		required: true
		schema: {type: "string", description: "Document ID"}
	}
}

_schemaRefs: F.#SchemaRefs & {
    Document: _
    DocumentListResponse: _
    ErrorResponse: _
    DocumentListItem: _
    DocumentBatchCreateRequest: _
    BatchCreateResponse: _
    DocumentBatchUpdateRequest: _
    BatchUpdateResponse: _
    DocumentBatchDeleteRequest: _
    BatchDeleteResponse: _
}

// PathItems defines all document REST endpoints
paths: {
    ...,
	"/documents": {
		post: {
			operationId: "createDocument"
			summary: "Create a document"
			tags: ["documents"]
			requestBody: {
				required: true
				content: _schemaRefs.Document
			}
			responses: F.#R400 & F.#R422 & {
				"201": {
					description: "Document created"
					content: _schemaRefs.Document
				}
			}
		}
		get: {
			operationId: "listDocuments"
			summary: "List documents"
			tags: ["documents"]
			parameters: F.#Pagination
				//{"$ref": "#/components/parameters/document_filter"}
			responses: F.#R400 & {
				"200": {
					description: "Document list"
					content: _schemaRefs.DocumentListResponse
				}
			}
		}
	}
	"/documents/{documentId}": {
		get: {
			operationId: "getDocument"
			summary: "Get a document"
			tags: ["documents"]
			parameters: [#PathParams.documentId]
			responses: {
				"200": {
					description: "Document details"
					content: _schemaRefs.Document
				}
				"404": {
					description: "Not found"
					content: _schemaRefs.ErrorResponse
				}
			}
		}
		put: {
			operationId: "updateDocument"
			summary: "Update a document"
			tags: ["documents"]
			parameters: [#PathParams.documentId]
			requestBody: {
				required: true
				content: _schemaRefs.Document
			}
			responses: {
				"200": {
					description: "Document updated"
					content: _schemaRefs.Document
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
			operationId: "deleteDocument"
			summary: "Delete a document"
			tags: ["documents"]
			parameters: [#PathParams.documentId]
			responses: {
				"204": {
					description: "Document deleted"
				}
				"404": {
					description: "Not found"
					content: _schemaRefs.ErrorResponse
				}
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
				content: _schemaRefs.DocumentBatchCreateRequest
			}
			responses: F.#R400 & F.#R422 & {
				"207": {
					description: "Batch creation result"
					content: _schemaRefs.BatchCreateResponse
				}
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
				content: _schemaRefs.DocumentBatchUpdateRequest
			}
			responses: F.#R400 & F.#R422 & {
				"200": {
					description: "Batch update result"
					content: _schemaRefs.BatchUpdateResponse
				}
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
				content: _schemaRefs.DocumentBatchDeleteRequest
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
