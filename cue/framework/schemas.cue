// framework/schemas.cue - Framework-level OpenAPI component schemas.
//
// These schemas are shared by all resources and belong to the framework, not
// any specific domain. They are merged into the final OpenAPIDoc by the
// operations/schemas.cue aggregator.
//
// BatchSchemas generates the three per-resource batch-request schemas from a
// resource name, eliminating copy-paste across resource packages.

package framework

// ---------------------------------------------------------------------------
// Shared framework schemas (error, pagination, batch responses)
// ---------------------------------------------------------------------------

// FrameworkSchemas contains all shared schemas to be merged into OpenAPIDoc.
FrameworkSchemas: {
	ErrorDetail: {
		description: "Details of a single error"
		type:        "object"
		required: ["code", "message"]
		properties: {
			code: {type: "string", description: "Machine-readable error code"}
			message: {type: "string", description: "Human-readable error message"}
			field: {type: "string", description: "Field that caused the error (if applicable)"}
			suggestion: {type: "string", description: "Suggested fix"}
		}
	}

	ErrorResponse: {
		description: "Standard error envelope returned for all 4xx/5xx responses"
		type:        "object"
		required: ["error"]
		properties: {
			error: {"$ref": "#/components/schemas/ErrorDetail"}
			timestamp: {type: "string", format: "date-time", description: "Time the error occurred"}
			traceId: {type: "string", description: "Distributed trace identifier"}
		}
	}

	PaginationMeta: {
		description: "Pagination metadata included in every list response"
		type:        "object"
		required: ["pageNumber", "pageSize", "totalCount", "hasMore"]
		properties: {
			pageNumber: {type: "integer", minimum: 1, description: "Current page (1-based)"}
			pageSize: {type: "integer", minimum: 1, maximum: 100, description: "Items per page"}
			totalCount: {type: "integer", minimum: 0, description: "Total items across all pages"}
			hasMore: {type: "boolean", description: "Whether more pages exist"}
		}
	}

	BatchItemResult: {
		description: "Result of a single item in a batch-create operation"
		type:        "object"
		required: ["index", "success"]
		properties: {
			index: {type: "integer", description: "Zero-based position of the item in the request array"}
			success: {type: "boolean", description: "Whether this item was created successfully"}
			resourceId: {type: "string", description: "ID of the newly created resource (present on success)"}
			error: {"$ref": "#/components/schemas/ErrorDetail"}
		}
	}

	BatchCreateResponse: {
		description: "Result of a batch-create operation (HTTP 207 Multi-Status)"
		type:        "object"
		required: ["succeeded", "failed", "results"]
		properties: {
			succeeded: {type: "integer", minimum: 0, description: "Number of items successfully created"}
			failed: {type: "integer", minimum: 0, description: "Number of items that failed"}
			results: {type: "array", items: {"$ref": "#/components/schemas/BatchItemResult"}}
		}
	}

	BatchUpdateResponse: {
		description: "Result of a batch-update operation"
		type:        "object"
		required: ["updated", "skipped", "dryRun"]
		properties: {
			updated: {type: "integer", minimum: 0, description: "Number of resources updated"}
			skipped: {type: "integer", minimum: 0, description: "Number of resources skipped (did not match or unchanged)"}
			dryRun: {type: "boolean", description: "True when the request was a dry run and no changes were persisted"}
		}
	}

	BatchDeleteResponse: {
		description: "Result of a batch-delete operation"
		type:        "object"
		required: ["deleted", "skipped"]
		properties: {
			deleted: {type: "integer", minimum: 0, description: "Number of resources deleted"}
			skipped: {type: "integer", minimum: 0, description: "Number of resources skipped"}
		}
	}
}

// ---------------------------------------------------------------------------
// Batch-request schema templates — used by each resource's schemas.cue.
//
// Each template is a CUE #-definition (closed struct) that enforces the
// required shape while leaving resource-specific fields open for the caller
// to fill in via &.
//
// Usage in a resource schemas.cue:
//   DocumentBatchCreateRequest: fw.#BatchCreateRequestOf & {
//     description: "Batch create request for documents"
//     properties: items: {
//       description: "Documents to create"
//       items: "$ref": "#/components/schemas/Document"
//     }
//   }
// ---------------------------------------------------------------------------

// #BatchCreateRequestOf is the structural template for batch-create schemas.
#BatchCreateRequestOf: {
	description: string
	type:        "object"
	required: ["items"]
	properties: {
		items: {type: "array", description: string, items: "$ref": string}
		continueOnError: {type: "boolean", description: "Continue processing remaining items after a failure"}
	}
}

// #BatchUpdateRequestOf is the structural template for batch-update schemas.
#BatchUpdateRequestOf: {
	description: string
	type:        "object"
	required: ["filter", "updates"]
	properties: {
		filter: "$ref":  string
		updates: "$ref": string
		dryRun: {type: "boolean", description: "Preview changes without persisting them"}
	}
}

// #BatchDeleteRequestOf is the structural template for batch-delete schemas.
#BatchDeleteRequestOf: {
	description: string
	type:        "object"
	required: ["filter"]
	properties: {
		filter: "$ref": string
		confirmDeletion: {type: "boolean", description: "Must be true to confirm destructive batch delete"}
	}
}

// ---------------------------------------------------------------------------
// Schema Generation Functions
// ---------------------------------------------------------------------------

// #GenerateResourceSchema converts a CUE Resource type into an OpenAPI schema.
// 
// Usage in schemas.cue:
//   Document: fw.#GenerateResourceSchema & {
//     value: Resource  // from types.cue
//     description: "A document managed by the system"
//   }
#GenerateResourceSchema: {
	value:       _
	description: string
	output: {
		description: description
		type:        "object"
		required: [...string] // caller should specify required fields
		properties: {...} // caller should fill in properties
	}
}

// #GenerateListItemSchema converts a CUE ListItem type into an OpenAPI list schema.
//
// Usage in schemas.cue:
//   DocumentListItem: fw.#GenerateListItemSchema & {
//     value: ListItem
//     description: "Lightweight document representation used in list responses"
//   }
#GenerateListItemSchema: {
	value:       _
	description: string
	output: {
		description: description
		type:        "object"
		required: [...string]
		properties: {...}
	}
}

// ---------------------------------------------------------------------------
// Type-to-Schema Generator — Derive OpenAPI schemas from CUE field definitions
// ---------------------------------------------------------------------------

// Common properties for all field types
_BaseField: {
	description: string
	optional?:   bool
}

// Type-specific field variants
_StringField: {
	type:       "string"
	format?:    string
	minLength?: int
	maxLength?: int
	enum?: [...string]
}

_IntField: {
	type:     "integer"
	minimum?: int
	maximum?: int
	enum?: [...int]
}

_NumberField: {
	type:     "number"
	minimum?: int | float
	maximum?: int | float
	enum?: [...(int | float)]
}

_BoolField: {
	type: "boolean"
	enum?: [...bool]
}

_ArrayField: {
	type:      "array"
	items:     _
	minItems?: int
	maxItems?: int
}

_ObjectField: {
	type: "object"
	properties?: {[string]: _}
}

// #FieldMetadata combines base properties with one of the type-specific variants
#FieldMetadata: (_StringField | _IntField | _NumberField | _BoolField | _ArrayField | _ObjectField) & _BaseField

// #SchemaFromFields generates an OpenAPI component schema from field metadata.
// All field types are validated at definition time based on their OpenAPI type.
// 
// Usage in schemas.cue:
//   User: fw.#SchemaFromFields & {
//     description: "A user account in the system"
//     required: ["email", "firstName", "lastName", "role"]
//     fields: {
//       id: {type: "string", description: "Unique user identifier", optional: true}
//       email: {type: "string", format: "email", description: "User's email address"}
//       firstName: {type: "string", minLength: 1, description: "Given name (non-empty)"}
//       lastName: {type: "string", minLength: 1, description: "Family name (non-empty)"}
//       role: {type: "string", enum: ["admin", "editor", "viewer"], description: "Access role"}
//       isActive: {type: "boolean", description: "Whether the account is enabled", optional: true}
//       lastLoginAt: {type: "string", format: "date-time", description: "Last successful login timestamp", optional: true}
//       createdAt: {type: "string", format: "date-time", description: "Account creation timestamp", optional: true}
//       memberCount: {type: "integer", minimum: 0, description: "Number of members"}
//       tags: {type: "array", items: {type: "string"}, description: "Tag labels"}
//     }
//   }
// Type constraints are enforced:
//   - String fields: can use format, minLength, maxLength, enum
//   - Integer/Number fields: can use minimum, maximum, enum
//   - Boolean fields: can use enum only
//   - Array fields: must have items, can use minItems, maxItems
//   - Object fields: can have properties
// Using a constraint on the wrong type will fail validation.
#SchemaFromFields: {
	description: string
	required?: [...string]
	fields: {[string]: #FieldMetadata}

	// Generate properties object by spreading field metadata
	// (undefined constraint fields are naturally excluded)
	_properties: {
		for fieldName, fieldMeta in fields {
			(fieldName): fieldMeta
		}
	}

	// Output the complete schema directly (not wrapped)
	type:        "object"
	description: description
	required:    required
	properties:  _properties
}

// #InferFromType provides type-specific field helpers to reduce boilerplate
// when defining schemas. Pre-configured with correct type and optional fields.
//
// Usage pattern in #SchemaFromFields:
//   User: fw.#SchemaFromFields & {
//     description: "A user account"
//     required: ["email", "firstName", "lastName", "role"]
//     fields: {
//       id: fw.#InferFromType.string & {description: "User ID", optional: true}
//       email: fw.#InferFromType.string & {description: "User email", format: "email"}
//       firstName: fw.#InferFromType.string & {description: "Given name", minLength: 1}
//       role: fw.#InferFromType.string & {description: "Access role", enum: ["admin", "viewer"]}
//       isActive: fw.#InferFromType.boolean & {description: "Account active", optional: true}
//       tags: fw.#InferFromType.array & {description: "Tags", items: {type: "string"}}
//     }
//   }
#InferFromType: {
	string: {
		type:        "string"
		description: string
		format?:     string
		minLength?:  int
		maxLength?:  int
		enum?: [...string]
		optional?: bool
	}

	integer: {
		type:        "integer"
		description: string
		minimum?:    int
		maximum?:    int
		enum?: [...int]
		optional?: bool
	}

	number: {
		type:        "number"
		description: string
		minimum?:    int | float
		maximum?:    int | float
		enum?: [...(int | float)]
		optional?: bool
	}

	boolean: {
		type:        "boolean"
		description: string
		enum?: [...bool]
		optional?: bool
	}

	array: {
		type:        "array"
		description: string
		items:       _
		minItems?:   int
		maxItems?:   int
		optional?:   bool
	}

	object: {
		type:        "object"
		description: string
		properties?: {[string]: _}
		optional?: bool
	}
}

// ---------------------------------------------------------------------------
// Filter Schema Factory — Dramatically Simplifies Filter Definitions
// ---------------------------------------------------------------------------

// #FilterSchemaOf generates a complete OpenAPI filter schema from minimal input.
