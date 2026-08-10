// resources/teams/types.cue - Team resource types and enums

package teams

import fw "example.com/apispec/framework"

// ============================================================================
// TEAM-SPECIFIC ENUMS
// ============================================================================

// Team member roles - only used in teams
TeamRole: "owner" | "member"

// ============================================================================
// TEAM RESOURCE
// ============================================================================

// Team resource type
Resource: {
	id?: string
	name: string & !=""
	description?: string
	ownerIds: [...string]
	memberIds: [...string]
	isPublic?: bool
	createdAt?: string
	updatedAt?: string
}

// Team in list response
ListItem: {
	id: string
	name: string
	memberCount: int & >=0
	ownerCount: int & >=1
	isPublic: bool
	createdAt: string
}

// Team filter for queries
Filter: {
	search?: fw.TextSearch
	isPublic?: bool
	ownerIds?: [...string]
	hasMembers?: {
		userId: string
		role?: TeamRole
	}
	dateRange?: fw.DateRange
} & fw.PaginationRequest
