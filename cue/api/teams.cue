package api

import (
	F "example.com/apispec/framework"
)

// Team is a team grouping users with shared access to resources
#Team: {
	// Unique team identifier
	id?: string
	// Team display name
	name: string
	// Optional description of the team's purpose
	description?: string
	// User IDs with owner privileges
	ownerIds: [...string]
	// User IDs who are members
	memberIds: [...string]
	// Whether the team is discoverable by non-members
	isPublic?: bool
	// Team creation timestamp
	createdAt?: string
	// Last-update timestamp
	updatedAt?: string
}

#TeamListResponse: F.#WithPagination & {items: [... #TeamListItem]}

// TeamListItem is a lightweight team representation for list responses
#TeamListItem: {
	// Team identifier
	id: string
	// Team display name
	name: string
	// Number of members
	memberCount: int
	// Number of owners
	ownerCount: int
	// Whether the team is discoverable
	isPublic: bool
	// Team creation timestamp
	createdAt: string
}

// TeamFilter contains query parameters for filtering and paginating teams
#TeamFilter: {
	// Full-text search query
	"search.query"?: string
	// Filter by visibility
	isPublic?: bool
	// Filter teams by owner user ID
	ownerIds?: [...string]
	// Filter teams that include this user ID as a member
	"hasMembers.userId"?: string
	// Filter by the member's role within the team
	"hasMembers.role"?: string
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

// TeamBatchCreateRequest is a batch create request for teams
#TeamBatchCreateRequest: {
	// Teams to create
	items: [...#Team]
	// Continue processing remaining items after a failure
	continueOnError?: bool
}

// TeamBatchUpdateRequest is a batch update request for teams
#TeamBatchUpdateRequest: {
	// Filter selects teams
	filter: #TeamFilter
	// Updates are applied to all matches
	updates: #Team
	// Preview changes without persisting them
	dryRun?: bool
}

// TeamBatchDeleteRequest is a batch delete request for teams
#TeamBatchDeleteRequest: {
	// Filter selects teams to delete
	filter: #TeamFilter
	// Must be true to confirm destructive batch delete
	confirmDeletion?: bool
}
