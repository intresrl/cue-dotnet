// resources/teams/paths.cue - OpenAPI path items for the teams resource.
// CRUDPaths from the framework generates all 8 operations; only the spec differs per resource.

package teams

import fw "example.com/apispec/framework"

_crud: fw.CRUDPaths & {spec: {
	tag:            "teams"
	single:         "Team"
	listItem:       "TeamListItem"
	basePath:       "/teams"
	tagDescription: "Team management endpoints"
	extraFilterParams: [
		{name: "isPublic",          in: "query", schema: {type: "boolean"},                                   description: "Filter by visibility"},
		{name: "ownerIds",          in: "query", schema: {type: "array", items: {type: "string"}},            description: "Filter by owner user ID"},
		{name: "hasMembers.userId", in: "query", schema: {type: "string"},                                    description: "Filter teams containing this member"},
		{name: "hasMembers.role",   in: "query", schema: {type: "string", enum: ["owner", "member"]},         description: "Filter by member role within team"},
	]
}}

PathItems: _crud.PathItems
Tag: _crud.Tag
