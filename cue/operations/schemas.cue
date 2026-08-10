// operations/schemas.cue - OpenAPI component schemas defined as plain CUE types.
//
// These types are converted to JSON Schema by the Python script using `cue def`.
// Plain CUE types with @json annotations for documentation.

package operations

// ── Shared / framework ──────────────────────────────────────────────────

// ErrorDetail describes a single error in responses
ErrorDetail: {
	code: string        @json(description: "Machine-readable error code")
	message: string     @json(description: "Human-readable error message")
	field?: string      @json(description: "Field that caused the error (if applicable)")
	suggestion?: string @json(description: "Suggested fix")
}

// ErrorResponse is a standard error envelope for all 4xx/5xx responses
ErrorResponse: {
	error: ErrorDetail              @json(description: "Error details")
	timestamp?: string              @json(description: "Time the error occurred")
	traceId?: string                @json(description: "Distributed trace identifier")
}

// PaginationMeta is metadata included in every list response
PaginationMeta: {
	pageNumber: int                 @json(description: "Current page (1-based)")
	pageSize: int                   @json(description: "Items per page")
	totalCount: int                 @json(description: "Total items across all pages")
	hasMore: bool                   @json(description: "Whether more pages exist")
}

// BatchItemResult is the result of a single item in a batch operation
BatchItemResult: {
	index: int                      @json(description: "Zero-based position of the item in the request array")
	success: bool                   @json(description: "Whether this item was processed successfully")
	resourceId?: string             @json(description: "ID of the newly created resource (present on success)")
	error?: ErrorDetail             @json(description: "Error details if failed")
}

// BatchCreateResponse is the result of a batch-create operation (HTTP 207)
BatchCreateResponse: {
	succeeded: int                  @json(description: "Number of items successfully created")
	failed: int                     @json(description: "Number of items that failed")
	results: [...BatchItemResult]   @json(description: "Individual item results")
}

// BatchUpdateResponse is the result of a batch-update operation
BatchUpdateResponse: {
	updated: int                    @json(description: "Number of resources updated")
	skipped: int                    @json(description: "Number of resources skipped")
	dryRun: bool                    @json(description: "True when the request was a dry run and no changes were persisted")
}

// BatchDeleteResponse is the result of a batch-delete operation
BatchDeleteResponse: {
	deleted: int                    @json(description: "Number of resources deleted")
	skipped: int                    @json(description: "Number of resources skipped")
}

// ── Documents ────────────────────────────────────────────────────────────

// Document is a document managed by the system
Document: {
	id?: string                     @json(description: "Unique document identifier")
	title: string                   @json(description: "Document title")
	description?: string            @json(description: "Optional human-readable description")
	status: string                  @json(description: "Publication status")
	contentType: string             @json(description: "File/content type")
	tags?: [...string]              @json(description: "Free-form tag labels")
	isPublic?: bool                 @json(description: "Whether the document is publicly visible")
	createdAt?: string              @json(description: "Creation timestamp (ISO 8601)")
	updatedAt?: string              @json(description: "Last-update timestamp (ISO 8601)")
}

// DocumentListItem is a lightweight document representation for list responses
DocumentListItem: {
	id: string                      @json(description: "Unique document identifier")
	title: string                   @json(description: "Document title")
	status: string                  @json(description: "Publication status")
	createdAt: string               @json(description: "Creation timestamp")
	updatedAt?: string              @json(description: "Last-update timestamp")
}

// DocumentFilter contains query parameters for filtering and paginating documents
DocumentFilter: {
	"search.query"?: string         @json(description: "Full-text search query")
	"search.caseSensitive"?: bool   @json(description: "Enable case-sensitive search")
	"search.fuzzy"?: bool           @json(description: "Enable fuzzy matching")
	statusIn?: [...string]          @json(description: "Filter by one or more statuses")
	isPublic?: bool                 @json(description: "Filter by visibility")
	tagIds?: [...string]            @json(description: "Filter by tag identifiers")
	"dateRange.from"?: string       @json(description: "Earliest createdAt to include")
	"dateRange.to"?: string         @json(description: "Latest createdAt to include")
	pageNumber?: int                @json(description: "Page number (1-based)")
	pageSize?: int                  @json(description: "Items per page")
	sortBy?: string                 @json(description: "Field to sort by")
	sortDirection?: string          @json(description: "Sort direction")
}

// DocumentBatchCreateRequest is a batch create request for documents
DocumentBatchCreateRequest: {
	items: [...Document]            @json(description: "Documents to create")
	continueOnError?: bool          @json(description: "Continue processing remaining items after a failure")
}

// DocumentBatchUpdateRequest is a batch update request for documents
DocumentBatchUpdateRequest: {
	filter: DocumentFilter          @json(description: "Filter selects documents")
	updates: Document               @json(description: "Updates are applied to all matches")
	dryRun?: bool                   @json(description: "Preview changes without persisting them")
}

// DocumentBatchDeleteRequest is a batch delete request for documents
DocumentBatchDeleteRequest: {
	filter: DocumentFilter          @json(description: "Filter selects documents to delete")
	confirmDeletion?: bool          @json(description: "Must be true to confirm destructive batch delete")
}

// ── Users ─────────────────────────────────────────────────────────────

// User is a user account in the system
User: {
	id?: string                     @json(description: "Unique user identifier")
	email: string                   @json(description: "User's email address")
	firstName: string               @json(description: "Given name")
	lastName: string                @json(description: "Family name")
	role: string                    @json(description: "Access role")
	isActive?: bool                 @json(description: "Whether the account is enabled")
	lastLoginAt?: string            @json(description: "Last successful login timestamp")
	createdAt?: string              @json(description: "Account creation timestamp")
}

// UserListItem is a lightweight user representation for list responses
UserListItem: {
	id: string                      @json(description: "User identifier")
	email: string                   @json(description: "User's email address")
	firstName: string               @json(description: "Given name")
	lastName: string                @json(description: "Family name")
	role: string                    @json(description: "Access role")
	isActive: bool                  @json(description: "Whether the account is enabled")
	lastLoginAt?: string            @json(description: "Last successful login timestamp")
}

// UserFilter contains query parameters for filtering and paginating users
UserFilter: {
	"search.query"?: string         @json(description: "Text search across email/firstName/lastName")
	"search.fields"?: [...string]   @json(description: "Fields to search within")
	roleIn?: [...string]            @json(description: "Filter by one or more roles")
	isActive?: bool                 @json(description: "Filter by account status")
	"dateRange.from"?: string       @json(description: "Earliest createdAt to include")
	"dateRange.to"?: string         @json(description: "Latest createdAt to include")
	pageNumber?: int                @json(description: "Page number (1-based)")
	pageSize?: int                  @json(description: "Items per page")
	sortBy?: string                 @json(description: "Field to sort by")
	sortDirection?: string          @json(description: "Sort direction")
}

// UserBatchCreateRequest is a batch create request for users
UserBatchCreateRequest: {
	items: [...User]                @json(description: "Users to create")
	continueOnError?: bool          @json(description: "Continue processing remaining items after a failure")
}

// UserBatchUpdateRequest is a batch update request for users
UserBatchUpdateRequest: {
	filter: UserFilter              @json(description: "Filter selects users")
	updates: User                   @json(description: "Updates are applied to all matches")
	dryRun?: bool                   @json(description: "Preview changes without persisting them")
}

// UserBatchDeleteRequest is a batch delete request for users
UserBatchDeleteRequest: {
	filter: UserFilter              @json(description: "Filter selects users to delete")
	confirmDeletion?: bool          @json(description: "Must be true to confirm destructive batch delete")
}

// ── Teams ─────────────────────────────────────────────────────────────

// Team is a team grouping users with shared access to resources
Team: {
	id?: string                     @json(description: "Unique team identifier")
	name: string                    @json(description: "Team display name")
	description?: string            @json(description: "Optional description of the team's purpose")
	ownerIds: [...string]           @json(description: "User IDs with owner privileges")
	memberIds: [...string]          @json(description: "User IDs who are members")
	isPublic?: bool                 @json(description: "Whether the team is discoverable by non-members")
	createdAt?: string              @json(description: "Team creation timestamp")
	updatedAt?: string              @json(description: "Last-update timestamp")
}

// TeamListItem is a lightweight team representation for list responses
TeamListItem: {
	id: string                      @json(description: "Team identifier")
	name: string                    @json(description: "Team display name")
	memberCount: int                @json(description: "Number of members")
	ownerCount: int                 @json(description: "Number of owners")
	isPublic: bool                  @json(description: "Whether the team is discoverable")
	createdAt: string               @json(description: "Team creation timestamp")
}

// TeamFilter contains query parameters for filtering and paginating teams
TeamFilter: {
	"search.query"?: string         @json(description: "Full-text search query")
	isPublic?: bool                 @json(description: "Filter by visibility")
	ownerIds?: [...string]          @json(description: "Filter teams by owner user ID")
	"hasMembers.userId"?: string    @json(description: "Filter teams that include this user ID as a member")
	"hasMembers.role"?: string      @json(description: "Filter by the member's role within the team")
	"dateRange.from"?: string       @json(description: "Earliest createdAt to include")
	"dateRange.to"?: string         @json(description: "Latest createdAt to include")
	pageNumber?: int                @json(description: "Page number (1-based)")
	pageSize?: int                  @json(description: "Items per page")
	sortBy?: string                 @json(description: "Field to sort by")
	sortDirection?: string          @json(description: "Sort direction")
}

// TeamBatchCreateRequest is a batch create request for teams
TeamBatchCreateRequest: {
	items: [...Team]                @json(description: "Teams to create")
	continueOnError?: bool          @json(description: "Continue processing remaining items after a failure")
}

// TeamBatchUpdateRequest is a batch update request for teams
TeamBatchUpdateRequest: {
	filter: TeamFilter              @json(description: "Filter selects teams")
	updates: Team                   @json(description: "Updates are applied to all matches")
	dryRun?: bool                   @json(description: "Preview changes without persisting them")
}

// TeamBatchDeleteRequest is a batch delete request for teams
TeamBatchDeleteRequest: {
	filter: TeamFilter              @json(description: "Filter selects teams to delete")
	confirmDeletion?: bool          @json(description: "Must be true to confirm destructive batch delete")
}

// ── Reusable Parameters ─────────────────────────────────────────────────

// Common pagination and sorting parameters for list operations
ListParams: [
	{name: "pageNumber", in: "query", schema: {type: "integer"}, description: "Page number (1-based)"}
	{name: "pageSize", in: "query", schema: {type: "integer"}, description: "Items per page"}
	{name: "sortBy", in: "query", schema: {type: "string"}, description: "Sort by field"}
	{name: "sortDirection", in: "query", schema: {type: "string", enum: ["asc", "desc"]}, description: "Sort direction"}
]

// Filter parameters for each resource
DocumentFilterParam: {name: "filter", in: "query", schema: {"$ref": "#/components/schemas/DocumentFilter"}, description: "Filter documents"}
UserFilterParam: {name: "filter", in: "query", schema: {"$ref": "#/components/schemas/UserFilter"}, description: "Filter users"}
TeamFilterParam: {name: "filter", in: "query", schema: {"$ref": "#/components/schemas/TeamFilter"}, description: "Filter teams"}

// Schemas is an export that maps schema names to concrete example values
// This allows the Python script to extract type information
Schemas: {
	ErrorDetail: ErrorDetail & {code: "", message: ""}
	ErrorResponse: ErrorResponse & {error: {code: "", message: ""}}
	PaginationMeta: PaginationMeta & {pageNumber: 1, pageSize: 10, totalCount: 0, hasMore: false}
	BatchItemResult: BatchItemResult & {index: 0, success: false}
	BatchCreateResponse: BatchCreateResponse & {succeeded: 0, failed: 0, results: []}
	BatchUpdateResponse: BatchUpdateResponse & {updated: 0, skipped: 0, dryRun: false}
	BatchDeleteResponse: BatchDeleteResponse & {deleted: 0, skipped: 0}
	Document: Document & {title: "", status: "", contentType: ""}
	DocumentListItem: DocumentListItem & {id: "", title: "", status: "", createdAt: ""}
	DocumentFilter: DocumentFilter & {}
	DocumentBatchCreateRequest: DocumentBatchCreateRequest & {items: []}
	DocumentBatchUpdateRequest: DocumentBatchUpdateRequest & {filter: {}, updates: {title: "", status: "", contentType: ""}}
	DocumentBatchDeleteRequest: DocumentBatchDeleteRequest & {filter: {}}
	User: User & {email: "", firstName: "", lastName: "", role: ""}
	UserListItem: UserListItem & {id: "", email: "", firstName: "", lastName: "", role: "", isActive: false}
	UserFilter: UserFilter & {}
	UserBatchCreateRequest: UserBatchCreateRequest & {items: []}
	UserBatchUpdateRequest: UserBatchUpdateRequest & {filter: {}, updates: {email: "", firstName: "", lastName: "", role: ""}}
	UserBatchDeleteRequest: UserBatchDeleteRequest & {filter: {}}
	Team: Team & {name: "", ownerIds: [], memberIds: []}
	TeamListItem: TeamListItem & {id: "", name: "", memberCount: 0, ownerCount: 1, isPublic: false, createdAt: ""}
	TeamFilter: TeamFilter & {}
	TeamBatchCreateRequest: TeamBatchCreateRequest & {items: []}
	TeamBatchUpdateRequest: TeamBatchUpdateRequest & {filter: {}, updates: {name: "", ownerIds: [], memberIds: []}}
	TeamBatchDeleteRequest: TeamBatchDeleteRequest & {filter: {}}
}
