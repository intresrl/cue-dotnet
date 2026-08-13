package paths

import (
	F "example.com/apispec/framework"
)

_usersCfg: F.#CRUDNaming & {resourceName: "users"} & {
	schemaResource:            "User"
	schemaListResponse:        "UserListResponse"
	schemaErrorResponse:       "ErrorResponse"
	schemaListItem:            "UserListItem"
	schemaBatchCreateRequest:  "UserBatchCreateRequest"
	schemaBatchCreateResponse: "BatchCreateResponse"
	schemaBatchUpdateRequest:  "UserBatchUpdateRequest"
	schemaBatchUpdateResponse: "BatchUpdateResponse"
	schemaBatchDeleteRequest:  "UserBatchDeleteRequest"
	schemaBatchDeleteResponse: "BatchDeleteResponse"
}

paths: "/users": {
	post: (F.#GenCreateEndpoint & _usersCfg).post
	get:  (F.#GenListEndpoint & _usersCfg).get
}

paths: "/users/{userId}": {
	_cfg: _usersCfg & {idName: "userId"}

	get:    (F.#GenReadEndpoint & _cfg).get
	put:    (F.#GenUpdateEndpoint & _cfg).put
	delete: (F.#GenDeleteEndpoint & _cfg).delete
}

paths: "/users:batch": {
    post: (F.#GenBatchCreateEndpoint & _usersCfg).post
	patch: (F.#GenBatchUpdateEndpoint & _usersCfg).patch
	delete: (F.#GenBatchDeleteEndpoint & _usersCfg).delete
}
