package api

// ErrorDetail describes a single error in responses
#ErrorDetail: {
	// Machine-readable error code
	code: string
	// Human-readable error message
	message: string
	// Field that caused the error (if applicable)
	field?: string
	// Suggested fix
	suggestion?: string
}

// ErrorResponse is a standard error envelope for all 4xx/5xx responses
#ErrorResponse: {
	// Error details
	error: #ErrorDetail
	// Time the error occurred
	timestamp?: string
	// Distributed trace identifier
	traceId?: string
}

// BatchItemResult is the result of a single item in a batch operation
#BatchItemResult: {
	// Zero-based position of the item in the request array
	index: int
	// Whether this item was processed successfully
	success: bool
	// ID of the newly created resource (present on success)
	resourceId?: string
	// Error details if failed
	error?: #ErrorDetail
}

// BatchCreateResponse is the result of a batch-create operation (HTTP 207)
#BatchCreateResponse: {
	// Number of items successfully created
	succeeded: int
	// Number of items that failed
	failed: int
	// Individual item results
	results: [...#BatchItemResult]
}

// BatchUpdateResponse is the result of a batch-update operation
#BatchUpdateResponse: {
	// Number of resources updated
	updated: int
	// Number of resources skipped
	skipped: int
	// True when the request was a dry run and no changes were persisted
	dryRun: bool
}

// BatchDeleteResponse is the result of a batch-delete operation
#BatchDeleteResponse: {
	// Number of resources deleted
	deleted: int
	// Number of resources skipped
	skipped: int
}

// schema used for pagination query parameters

// Page number (1-based)
#pageNumber: int

// Items per page
#pageSize: int

// Sort by field
#sortBy: string

// Sort direction
#sortDirection: "asc" | "desc"
