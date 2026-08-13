package framework

#IdPathParam: {
	_name: string
	_description: string

	name:     _name
	"in":     "path"
	required: true
	schema: {
		type:        "string"
		description: _description
	}
}

// #GenListEndpoint - GET /{resource}
#GenListEndpoint: {
	tagName: string
	listName: string
	listDescription: string
	schemaListResponse: string

	get: {
		operationId: listName
		summary:     listDescription
		tags: [tagName]
		parameters: #Pagination
		responses: #R400 & {
			"200": {
				content: #ContentSchema & {_schemaName: schemaListResponse}
			}
		}
	}
}

// #GenCreateEndpoint - POST /{resource}
#GenCreateEndpoint: {
    tagName: string,
    createName: string,
    createDescription: string
    schemaResource: string

	post: {
		operationId: createName
		summary:     createDescription
		tags: [tagName]
		requestBody: {
			required: true
			content: #ContentSchema & {_schemaName: schemaResource}
		}
		responses: #R400 & #R422 & {
			"201": {
				content: #ContentSchema & {_schemaName: schemaResource}
			}
		}
	}
}

// #GenReadEndpoint - GET /{resource}/{id}
#GenReadEndpoint: {
	tagName: string
	readName: string
	readDescription: string
	schemaResource: string
	idName: string
	idDescription: string

	get: {
		operationId: readName
		summary:     readDescription
		tags: [tagName]
		parameters: [#IdPathParam & {_name: idName, _description: idDescription}]
		responses: #R404 & {
			"200": {
				content: #ContentSchema & {_schemaName: schemaResource}
			}
		}
	}
}

// #GenUpdateEndpoint - PUT /{resource}/{id}
#GenUpdateEndpoint: {
	tagName: string
	updateName: string
	updateDescription: string
	schemaResource: string
	idName: string
	idDescription: string

	put: {
		operationId: updateName
		summary:     updateDescription
		tags: [tagName]
		parameters: [#IdPathParam & {_name: idName, _description: idDescription}]
		requestBody: {
			required: true
			content: #ContentSchema & {_schemaName: schemaResource}
		}
		responses: #R404 & #R409 & {
			"200": {
				content: #ContentSchema & {_schemaName: schemaResource}
			}
		}
	}
}

// #GenDeleteEndpoint - DELETE /{resource}/{id}
#GenDeleteEndpoint: {
	tagName: string
	deleteName: string
	deleteDescription: string
	idName: string
	idDescription: string

	delete: {
		operationId: deleteName
		summary:     deleteDescription
		tags: [tagName]
		parameters: [#IdPathParam & {_name: idName, _description: idDescription}]
		responses: #R404 & {
			"204": {}
		}
	}
}

// #GenBatchCreateEndpoint - POST /{resource}:batch-create
#GenBatchCreateEndpoint: {
	tagName: string
	batchCreateName: string
	batchCreateDescription: string
	schemaBatchCreateRequest: string
	schemaBatchCreateResponse: string

	post: {
		operationId: batchCreateName
		summary:     batchCreateDescription
		tags: [tagName]
		requestBody: {
			required: true
			content: #ContentSchema & {_schemaName: schemaBatchCreateRequest}
		}
		responses: #R400 & {
			"207": {
				content: #ContentSchema & {_schemaName: schemaBatchCreateResponse}
			}
		}
	}
}

// #GenBatchUpdateEndpoint - PATCH /{resource}:batch-update
#GenBatchUpdateEndpoint: {
	tagName: string
	batchUpdateName: string
	batchUpdateDescription: string
	schemaBatchUpdateRequest: string
	schemaBatchUpdateResponse: string

	patch: {
		operationId: batchUpdateName
		summary:     batchUpdateDescription
		tags: [tagName]
		requestBody: {
			required: true
			content: #ContentSchema & {_schemaName: schemaBatchUpdateRequest}
		}
		responses: #R400 & #R422 & {
			"200": {
				content: #ContentSchema & {_schemaName: schemaBatchUpdateResponse}
			}
		}
	}
}

// #GenBatchDeleteEndpoint - DELETE /{resource}:batch
#GenBatchDeleteEndpoint: {
	tagName: string
	batchDeleteName: string
	batchDeleteDescription: string
	schemaBatchDeleteRequest: string
	schemaBatchDeleteResponse: string

	delete: {
		operationId: batchDeleteName
		summary:     batchDeleteDescription
		tags: [tagName]
		requestBody: {
			required: true
			content: #ContentSchema & {_schemaName: schemaBatchDeleteRequest}
		}
		responses: #R400 & {
			"200": {
				content: #ContentSchema & {_schemaName: schemaBatchDeleteResponse}
			}
		}
	}
}
