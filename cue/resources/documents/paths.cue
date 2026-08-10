// resources/documents/paths.cue - OpenAPI path items for the documents resource.
// CRUDPaths from the framework generates all 8 operations; only the spec differs per resource.

package documents

import fw "example.com/apispec/framework"

_crud: fw.CRUDPaths & {spec: {
	tag:            "documents"
	single:         "Document"
	listItem:       "DocumentListItem"
	basePath:       "/documents"
	tagDescription: "Document management endpoints"
	extraFilterParams: [
		{name: "statusIn", in: "query", schema: {type: "array", items: {type: "string", enum: ["draft", "published", "archived"]}}, description: "Filter by one or more statuses"},
		{name: "isPublic", in: "query", schema: {type: "boolean"}, description: "Filter by visibility"},
		{name: "tagIds",   in: "query", schema: {type: "array", items: {type: "string"}}, description: "Filter by tag IDs"},
	]
}}

PathItems: _crud.PathItems
Tag: _crud.Tag
