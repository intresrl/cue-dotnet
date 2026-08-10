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
			code:       {type: "string", description: "Machine-readable error code"}
			message:    {type: "string", description: "Human-readable error message"}
			field:      {type: "string", description: "Field that caused the error (if applicable)"}
			suggestion: {type: "string", description: "Suggested fix"}
		}
	}

	ErrorResponse: {
		description: "Standard error envelope returned for all 4xx/5xx responses"
		type:        "object"
		required: ["error"]
		properties: {
			error:     {"$ref": "#/components/schemas/ErrorDetail"}
			timestamp: {type: "string", format: "date-time", description: "Time the error occurred"}
			traceId:   {type: "string", description: "Distributed trace identifier"}
		}
	}

	PaginationMeta: {
		description: "Pagination metadata included in every list response"
		type:        "object"
		required: ["pageNumber", "pageSize", "totalCount", "hasMore"]
		properties: {
			pageNumber: {type: "integer", minimum: 1,   description: "Current page (1-based)"}
			pageSize:   {type: "integer", minimum: 1, maximum: 100, description: "Items per page"}
			totalCount: {type: "integer", minimum: 0,   description: "Total items across all pages"}
			hasMore:    {type: "boolean",               description: "Whether more pages exist"}
		}
	}

	BatchItemResult: {
		description: "Result of a single item in a batch-create operation"
		type:        "object"
		required: ["index", "success"]
		properties: {
			index:      {type: "integer", description: "Zero-based position of the item in the request array"}
			success:    {type: "boolean", description: "Whether this item was created successfully"}
			resourceId: {type: "string",  description: "ID of the newly created resource (present on success)"}
			error:      {"$ref": "#/components/schemas/ErrorDetail"}
		}
	}

	BatchCreateResponse: {
		description: "Result of a batch-create operation (HTTP 207 Multi-Status)"
		type:        "object"
		required: ["succeeded", "failed", "results"]
		properties: {
			succeeded: {type: "integer", minimum: 0, description: "Number of items successfully created"}
			failed:    {type: "integer", minimum: 0, description: "Number of items that failed"}
			results:   {type: "array",  items: {"$ref": "#/components/schemas/BatchItemResult"}}
		}
	}

	BatchUpdateResponse: {
		description: "Result of a batch-update operation"
		type:        "object"
		required: ["updated", "skipped", "dryRun"]
		properties: {
			updated: {type: "integer", minimum: 0, description: "Number of resources updated"}
				skipped: {type: "integer", minimum: 0, description: "Number of resources skipped (did not match or unchanged)"}
				dryRun:  {type: "boolean",             description: "True when the request was a dry run and no changes were persisted"}
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
		filter:  "$ref": string
		updates: "$ref": string
		dryRun:  {type: "boolean", description: "Preview changes without persisting them"}
	}
}

// #BatchDeleteRequestOf is the structural template for batch-delete schemas.
#BatchDeleteRequestOf: {
	description: string
	type:        "object"
	required: ["filter"]
	properties: {
		filter:          "$ref": string
		confirmDeletion: {type: "boolean", description: "Must be true to confirm destructive batch delete"}
	}
}
