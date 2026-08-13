package framework

#Refs: [Name=string]: {
	"$ref": "#/components/schemas/\(Name)"
}

// #ContentSchema - Helper for generating OpenAPI content with schema references
// Use with: (#ContentSchema & {_schemaName: "SchemaName"})
#ContentSchema: {
	_schemaName: string
	"application/json": {
		schema: {
			"$ref": "#/components/schemas/\(_schemaName)"
		}
	}
}
