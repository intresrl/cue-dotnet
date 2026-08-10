// resources/documents/types.cue - Document resource types and enums

package documents

import fw "example.com/apispec/framework"

// ============================================================================
// DOCUMENT-SPECIFIC ENUMS
// ============================================================================

// Document status - only used in documents
DocumentStatus: "draft" | "published" | "archived"

// Content types supported by documents
ContentType: "pdf" | "docx" | "markdown" | "plaintext"

// ============================================================================
// DOCUMENT RESOURCE
// ============================================================================

// Document resource type
Resource: {
	id?: string
	title: string & !=""
	description?: string
	status: DocumentStatus
	contentType: ContentType
	tags?: [...string]
	isPublic?: bool
	createdAt?: string
	updatedAt?: string
}

// Document in list response
ListItem: {
	id: string
	title: string
	status: DocumentStatus
	createdAt: string
	updatedAt?: string
}

// Document filter for queries
Filter: {
	search?: fw.TextSearch
	statusIn?: [...DocumentStatus]
	isPublic?: bool
	tagIds?: [...string]
	dateRange?: fw.DateRange
} & fw.PaginationRequest

