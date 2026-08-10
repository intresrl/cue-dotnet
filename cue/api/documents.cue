package api

import (
    F "example.com/apispec/framework"
)

// Document is a document managed by the system
#Document: {
	// Unique document identifier
	id?: string
	// Document title
	title: string
	// Optional human-readable description
	description?: string
	// Publication status
	status: string
	// File/content type
	contentType: string
	// Free-form tag labels
	tags?: [...string]
	// Whether the document is publicly visible
	isPublic?: bool
	// Creation timestamp (ISO 8601)
	createdAt?: string
	// Last-update timestamp (ISO 8601)
	updatedAt?: string
}

#DocumentListResponse: F.#WithPagination & { items: [... #DocumentListItem] }

// DocumentListItem is a lightweight document representation for list responses
#DocumentListItem: {
	// Unique document identifier
	id: string
	// Document title
	title: string
	// Publication status
	status: string
	// Creation timestamp
	createdAt: string
	// Last-update timestamp
	updatedAt?: string
}

// DocumentFilter contains query parameters for filtering and paginating documents
#DocumentFilter: {
	// Full-text search query
	"search.query"?: string
	// Enable case-sensitive search
	"search.caseSensitive"?: bool
	// Enable fuzzy matching
	"search.fuzzy"?: bool
	// Filter by one or more statuses
	statusIn?: [...string]
	// Filter by visibility
	isPublic?: bool
	// Filter by tag identifiers
	tagIds?: [...string]
	// Earliest createdAt to include
	"dateRange.from"?: string
	// Latest createdAt to include
	"dateRange.to"?: string
	// Page number (1-based)
	pageNumber?: int
	// Items per page
	pageSize?: int
	// Field to sort by
	sortBy?: string
	// Sort direction
	sortDirection?: string
}

// DocumentBatchCreateRequest is a batch create request for documents
#DocumentBatchCreateRequest: {
	// Documents to create
	items: [...#Document]
	// Continue processing remaining items after a failure
	continueOnError?: bool
}

// DocumentBatchUpdateRequest is a batch update request for documents
#DocumentBatchUpdateRequest: {
	// Filter selects documents
	filter: #DocumentFilter
	// Updates are applied to all matches
	updates: #Document
	// Preview changes without persisting them
	dryRun?: bool
}

// DocumentBatchDeleteRequest is a batch delete request for documents
#DocumentBatchDeleteRequest: {
	// Filter selects documents to delete
	filter: #DocumentFilter
	// Must be true to confirm destructive batch delete
	confirmDeletion?: bool
}
