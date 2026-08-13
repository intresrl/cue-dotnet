package api

import (
	F "example.com/apispec/framework"
)

// User is a user account in the system
#User: {
	// Unique user identifier
	id?: string
	// User's email address
	email: string
	// Given name
	firstName: string
	// Family name
	lastName: string
	// Access role
	role: "admin" | "user"
	// Whether the account is enabled
	isActive?: bool
	// Last successful login timestamp
	lastLoginAt?: string
	// Account creation timestamp
	createdAt?: string
}

#UserListResponse: F.#WithPagination & {items: [... #UserListItem]}

// UserListItem is a lightweight user representation for list responses
#UserListItem: {
	// User identifier
	id: string
	// User's email address
	email: string
	// Given name
	firstName: string
	// Family name
	lastName: string
	// Access role
	role: string
	// Whether the account is enabled
	isActive: bool
	// Last successful login timestamp
	lastLoginAt?: string
}

// UserFilter contains query parameters for filtering and paginating users
#UserFilter: {
	// Text search across email/firstName/lastName
	"search.query"?: string
	// Fields to search within
	"search.fields"?: [...string]
	// Filter by one or more roles
	roleIn?: [...string]
	// Filter by account status
	isActive?: bool
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

// UserBatchCreateRequest is a batch create request for users
#UserBatchCreateRequest: {
	// Users to create
	items: [...#User]
	// Continue processing remaining items after a failure
	continueOnError?: bool
}

// UserBatchUpdateRequest is a batch update request for users
#UserBatchUpdateRequest: {
	// Filter selects users
	filter: #UserFilter
	// Updates are applied to all matches
	updates: #User
	// Preview changes without persisting them
	dryRun?: bool
}

// UserBatchDeleteRequest is a batch delete request for users
#UserBatchDeleteRequest: {
	// Filter selects users to delete
	filter: #UserFilter
	// Must be true to confirm destructive batch delete
	confirmDeletion?: bool
}
