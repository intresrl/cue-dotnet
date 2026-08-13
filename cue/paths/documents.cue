package paths

import (
	F "example.com/apispec/framework"
)

_documentsCfg: F.#CRUDNaming & {resourceName: "documents"} & {
	schemaResource:            "Document"
	schemaListResponse:        "DocumentListResponse"
	schemaErrorResponse:       "ErrorResponse"
	schemaListItem:            "DocumentListItem"
	schemaBatchCreateRequest:  "DocumentBatchCreateRequest"
	schemaBatchCreateResponse: "BatchCreateResponse"
	schemaBatchUpdateRequest:  "DocumentBatchUpdateRequest"
	schemaBatchUpdateResponse: "BatchUpdateResponse"
	schemaBatchDeleteRequest:  "DocumentBatchDeleteRequest"
	schemaBatchDeleteResponse: "BatchDeleteResponse"
}

paths: "/documents": {
	post: (F.#GenCreateEndpoint & _documentsCfg).post
	get:  (F.#GenListEndpoint & _documentsCfg).get
}

paths: "/documents/{documentId}": {
	_cfg: _documentsCfg & {idName: "documentId"}

	get:    (F.#GenReadEndpoint & _cfg).get
	put:    (F.#GenUpdateEndpoint & _cfg).put
	delete: (F.#GenDeleteEndpoint & _cfg).delete
}

paths: "/documents:batch": {
	post: (F.#GenBatchCreateEndpoint & _documentsCfg).post
	patch: (F.#GenBatchUpdateEndpoint & _documentsCfg).patch
	delete: (F.#GenBatchDeleteEndpoint & _documentsCfg).delete
}
