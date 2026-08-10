// resources/documents/paths.cue - OpenAPI path items for documents

package documents

// Tag definition for OpenAPI
Tag: {
	name: "documents"
	description: "Document management endpoints"
}

// Path parameter schema definitions that Python script will expand
#PathParams: {
	id: {
		name: "id"
		in: "path"
		required: true
		schema: {type: "string", description: "Document ID"}
	}
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
				"201": {
					description: "Document created"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
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
			operationId: "listDocuments"
			summary: "List documents"
			tags: ["documents"]
			parameters: [
				{name: "filter", in: "query", schema: {"$ref": "#/components/schemas/DocumentFilter"}, description: "Filter documents"}
				{name: "pageNumber", in: "query", schema: {type: "integer"}, description: "Page number (1-based)"}
				{name: "pageSize", in: "query", schema: {type: "integer"}, description: "Items per page"}
				{name: "sortBy", in: "query", schema: {type: "string"}, description: "Sort by field"}
				{name: "sortDirection", in: "query", schema: {type: "string", enum: ["asc", "desc"]}, description: "Sort direction"}
			]
			responses: {
				"200": {
					description: "Document list"
					content: {"application/json": {schema: {
						type: "object"
						properties: {
							items: {type: "array", items: {"$ref": "#/components/schemas/DocumentListItem"}}
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
	"/documents/{id}": {
		get: {
			operationId: "getDocument"
			summary: "Get a document"
			tags: ["documents"]
			parameters: [#PathParams.id]
			responses: {
				"200": {
					description: "Document details"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
				}
			}
		}
		put: {
			operationId: "updateDocument"
			summary: "Update a document"
			tags: ["documents"]
			parameters: [#PathParams.id]
			requestBody: {
				required: true
				content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
			}
			responses: {
				"200": {
					description: "Document updated"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/Document"}}}
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
			operationId: "deleteDocument"
			summary: "Delete a document"
			tags: ["documents"]
			parameters: [#PathParams.id]
			responses: {
				"204": {
					description: "Document deleted"
				}
				"404": {
					description: "Not found"
					content: {"application/json": {schema: {"$ref": "#/components/schemas/ErrorResponse"}}}
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
				content: {"application/json": {schema: {"$ref": "#/components/schemas/DocumentBatchCreateRequest"}}}
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
