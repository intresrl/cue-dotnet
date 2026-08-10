// resources/users/types.cue - User resource types and enums

package users

import fw "example.com/apispec/framework"

// ============================================================================
// USER-SPECIFIC ENUMS
// ============================================================================

// User roles - only used in user access control
UserRole: "admin" | "editor" | "viewer"

// ============================================================================
// USER RESOURCE
// ============================================================================

// User resource type
Resource: {
	id?: string
	email: string & =~"^[\\w.-]+@"
	firstName: string & !=""
	lastName: string & !=""
	role: UserRole
	isActive?: bool
	lastLoginAt?: string
	createdAt?: string
}

// User in list response
ListItem: {
	id: string
	email: string
	firstName: string
	lastName: string
	role: UserRole
	isActive: bool
	lastLoginAt?: string
}

// User filter for queries
Filter: {
	search?: fw.TextSearch & {
		fields?: [...("email" | "firstName" | "lastName")]
	}
	roleIn?: [...UserRole]
	isActive?: bool
	dateRange?: fw.DateRange
} & fw.PaginationRequest
