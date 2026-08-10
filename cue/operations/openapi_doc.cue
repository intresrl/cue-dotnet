// operations/openapi_doc.cue - Assembles the complete OpenAPI 3.0 document from all resource packages.
//
// This is the single value that gen_openapi.py exports:
//
//   cue export ./operations/... -e OpenAPIDoc --out json
//
// Python receives a complete, valid OpenAPI 3.0 document and only injects
// the x-generated-from extension before printing it.

package operations

import (
	docs  "example.com/apispec/resources/documents"
	users "example.com/apispec/resources/users"
	teams "example.com/apispec/resources/teams"
)

// OpenAPIDoc is the root OpenAPI 3.0 document exported to gen_openapi.py.
OpenAPIDoc: {
	openapi: "3.0.0"

	info: {
		title:       APISpec.title
		version:     APISpec.version
		description: APISpec.description
	}

	servers: APISpec.servers

	tags: [docs.Tag, users.Tag, teams.Tag]

	// paths merges PathItems from every resource package.
	// Each resource defines its own URL namespace so keys never collide.
	paths: docs.PathItems & users.PathItems & teams.PathItems

	components: schemas: Schemas
}
