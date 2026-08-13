package paths

import (
	F "example.com/apispec/framework"
)

_teamsCfg: F.#CRUDNaming & {resourceName: "teams"} & {
	schemaResource:            "Team"
	schemaListResponse:        "TeamListResponse"
	schemaErrorResponse:       "ErrorResponse"
	schemaListItem:            "TeamListItem"
	schemaBatchCreateRequest:  "TeamBatchCreateRequest"
	schemaBatchCreateResponse: "BatchCreateResponse"
	schemaBatchUpdateRequest:  "TeamBatchUpdateRequest"
	schemaBatchUpdateResponse: "BatchUpdateResponse"
	schemaBatchDeleteRequest:  "TeamBatchDeleteRequest"
	schemaBatchDeleteResponse: "BatchDeleteResponse"
}

paths: "/teams": {
	post: (F.#GenCreateEndpoint & _teamsCfg).post
	get:  (F.#GenListEndpoint & _teamsCfg).get
}

paths: "/teams/{teamId}": {
    _cfg: _teamsCfg & { idName: "teamId" }

	get:    (F.#GenReadEndpoint & _cfg).get
	put:    (F.#GenUpdateEndpoint & _cfg).put
	delete: (F.#GenDeleteEndpoint & _cfg).delete
}

paths: "/teams:batch": {
	post: (F.#GenBatchCreateEndpoint & _teamsCfg).post
	patch: (F.#GenBatchUpdateEndpoint & _teamsCfg).patch
	delete: (F.#GenBatchDeleteEndpoint & _teamsCfg).delete
}
