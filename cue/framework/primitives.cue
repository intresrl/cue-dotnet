// framework/primitives.cue - Core types and primitives used globally

package framework

// ISO 8601 timestamp
Timestamp: string

// Error detail for responses
ErrorDetail: {
	code: string
	message: string
	field?: string
	suggestion?: string
}

// Standard error response used across all endpoints
ErrorResponse: {
	error: ErrorDetail
	timestamp?: Timestamp
	traceId?: string
}

// Pagination metadata for list responses
PaginationMeta: {
	pageNumber: int & >=1
	pageSize: int & >=1 & <=100
	totalCount: int & >=0
	hasMore: bool
}

// ============================================================================
// SHARED SEARCH & PAGINATION - Generic filters used across all resources
// ============================================================================

// Text search with optional advanced features
TextSearch: {
	query: string
	caseSensitive?: bool
	fuzzy?: bool
}

// Date range with constraints
DateRange: {
	from?: Timestamp
	to?: Timestamp
}

// Sort direction for any list operation
SortDirection: "asc" | "desc"

// Pagination request parameters
PaginationRequest: {
	pageNumber?: int & >=1
	pageSize?: int & >=1 & <=100
	sortBy?: string
	sortDirection?: SortDirection
}
