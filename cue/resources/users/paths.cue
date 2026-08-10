// resources/users/paths.cue - OpenAPI path items for the users resource.
// CRUDPaths from the framework generates all 8 operations; only the spec differs per resource.

package users

import fw "example.com/apispec/framework"

_crud: fw.CRUDPaths & {spec: {
	tag:            "users"
	single:         "User"
	listItem:       "UserListItem"
	basePath:       "/users"
	tagDescription: "User account management endpoints"
	extraFilterParams: [
		{name: "search.fields", in: "query", schema: {type: "array", items: {type: "string", enum: ["email", "firstName", "lastName"]}}, description: "Fields to search within"},
		{name: "roleIn",        in: "query", schema: {type: "array", items: {type: "string", enum: ["admin", "editor", "viewer"]}},      description: "Filter by one or more roles"},
		{name: "isActive",      in: "query", schema: {type: "boolean"},                                                                  description: "Filter by active status"},
	]
}}

PathItems: _crud.PathItems
Tag: _crud.Tag
