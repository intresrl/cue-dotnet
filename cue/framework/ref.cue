package framework

#Refs: [Name=string]: {
	"$ref": "#/components/schemas/\(Name)"
}

#SchemaRefs: [Name=string]: {
	"application/json": schema: "$ref": "#/components/schemas/\(Name)"
}
