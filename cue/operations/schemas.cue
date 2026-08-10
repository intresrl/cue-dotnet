// operations/schemas.cue - OpenAPI component schemas derived from CUE resource types.
// All schemas are expressed as concrete, exportable objects (no CUE constraints)
// so that `cue export` can serialise them to JSON without "incomplete value" errors.
//
// These are consumed by OpenAPIDoc in openapi_doc.cue via the Schemas symbol.

package operations

// Schemas groups all OpenAPI component schemas.
Schemas: {

	// ── Shared / framework ──────────────────────────────────────────────────

	ErrorDetail: {
		description: "Details of a single error"
		type: "object"
		required: ["code", "message"]
		properties: {
			code:        {type: "string", description: "Machine-readable error code"}
			message:     {type: "string", description: "Human-readable error message"}
			field:       {type: "string", description: "Field that caused the error (if applicable)"}
			suggestion:  {type: "string", description: "Suggested fix"}
		}
	}

	ErrorResponse: {
		description: "Standard error envelope returned for all 4xx/5xx responses"
		type: "object"
		required: ["error"]
		properties: {
			error:     {"$ref": "#/components/schemas/ErrorDetail"}
			timestamp: {type: "string", format: "date-time", description: "Time the error occurred"}
			traceId:   {type: "string", description: "Distributed trace identifier"}
		}
	}

	PaginationMeta: {
		description: "Pagination metadata included in every list response"
		type: "object"
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
		type: "object"
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
		type: "object"
		required: ["succeeded", "failed", "results"]
		properties: {
			succeeded: {type: "integer", minimum: 0, description: "Number of items successfully created"}
			failed:    {type: "integer", minimum: 0, description: "Number of items that failed"}
			results:   {type: "array", items: {"$ref": "#/components/schemas/BatchItemResult"}}
		}
	}

	BatchUpdateResponse: {
		description: "Result of a batch-update operation"
		type: "object"
		required: ["updated", "skipped", "dryRun"]
		properties: {
			updated: {type: "integer", minimum: 0, description: "Number of resources updated"}
			skipped: {type: "integer", minimum: 0, description: "Number of resources skipped (did not match or unchanged)"}
			dryRun:  {type: "boolean", description: "True when the request was a dry run and no changes were persisted"}
		}
	}

	BatchDeleteResponse: {
		description: "Result of a batch-delete operation"
		type: "object"
		required: ["deleted", "skipped"]
		properties: {
			deleted: {type: "integer", minimum: 0, description: "Number of resources deleted"}
			skipped: {type: "integer", minimum: 0, description: "Number of resources skipped"}
		}
	}

	// ── Documents ────────────────────────────────────────────────────────────
	// Derived from resources/documents/types.cue: Resource, ListItem, Filter
	// Enum values mirror docs.DocumentStatus and docs.ContentType disjunctions.

	Document: {
		description: "A document managed by the system"
		type: "object"
		required: ["title", "status", "contentType"]
		properties: {
			id:          {type: "string", description: "Unique document identifier"}
			title:       {type: "string", minLength: 1, description: "Document title (non-empty)"}
			description: {type: "string", description: "Optional human-readable description"}
			status:      {type: "string", enum: ["draft", "published", "archived"], description: "Publication status"}
			contentType: {type: "string", enum: ["pdf", "docx", "markdown", "plaintext"], description: "File/content type"}
			tags:        {type: "array", items: {type: "string"}, description: "Free-form tag labels"}
			isPublic:    {type: "boolean", description: "Whether the document is publicly visible"}
			createdAt:   {type: "string", format: "date-time", description: "Creation timestamp (ISO 8601)"}
			updatedAt:   {type: "string", format: "date-time", description: "Last-update timestamp (ISO 8601)"}
		}
	}

	DocumentListItem: {
		description: "Lightweight document representation used in list responses"
		type: "object"
		required: ["id", "title", "status", "createdAt"]
		properties: {
			id:        {type: "string"}
			title:     {type: "string"}
			status:    {type: "string", enum: ["draft", "published", "archived"]}
			createdAt: {type: "string", format: "date-time"}
			updatedAt: {type: "string", format: "date-time"}
		}
	}

	DocumentFilter: {
		description: "Query parameters for filtering and paginating documents"
		type: "object"
		properties: {
			"search.query":        {type: "string", description: "Full-text search query"}
			"search.caseSensitive":{type: "boolean", description: "Enable case-sensitive search"}
			"search.fuzzy":        {type: "boolean", description: "Enable fuzzy matching"}
			statusIn:              {type: "array", items: {type: "string", enum: ["draft", "published", "archived"]}, description: "Filter by one or more statuses"}
			isPublic:              {type: "boolean", description: "Filter by visibility"}
			tagIds:                {type: "array", items: {type: "string"}, description: "Filter by tag identifiers"}
			"dateRange.from":      {type: "string", format: "date-time", description: "Earliest createdAt to include"}
			"dateRange.to":        {type: "string", format: "date-time", description: "Latest createdAt to include"}
			pageNumber:            {type: "integer", minimum: 1, description: "Page number (1-based)"}
			pageSize:              {type: "integer", minimum: 1, maximum: 100, description: "Items per page"}
			sortBy:                {type: "string", description: "Field to sort by"}
			sortDirection:         {type: "string", enum: ["asc", "desc"], description: "Sort direction"}
		}
	}

	DocumentBatchUpdateRequest: {
		description: "Batch update request: filter selects documents, updates are applied to all matches"
		type: "object"
		required: ["filter", "updates"]
		properties: {
			filter:  {"$ref": "#/components/schemas/DocumentFilter"}
			updates: {"$ref": "#/components/schemas/Document"}
			dryRun:  {type: "boolean", description: "Preview changes without persisting them"}
		}
	}

	DocumentBatchDeleteRequest: {
		description: "Batch delete request: filter selects documents to delete"
		type: "object"
		required: ["filter"]
		properties: {
			filter:           {"$ref": "#/components/schemas/DocumentFilter"}
			confirmDeletion:  {type: "boolean", description: "Must be true to confirm destructive batch delete"}
		}
	}

	DocumentBatchCreateRequest: {
		description: "Batch create request for documents"
		type: "object"
		required: ["items"]
		properties: {
			items:           {type: "array", items: {"$ref": "#/components/schemas/Document"}, description: "Documents to create"}
			continueOnError: {type: "boolean", description: "Continue processing remaining items after a failure"}
		}
	}

	// ── Users ─────────────────────────────────────────────────────────────
	// Derived from resources/users/types.cue: Resource, ListItem, Filter

	User: {
		description: "A user account in the system"
		type: "object"
		required: ["email", "firstName", "lastName", "role"]
		properties: {
			id:          {type: "string", description: "Unique user identifier"}
			email:       {type: "string", format: "email", description: "User's email address"}
			firstName:   {type: "string", minLength: 1, description: "Given name (non-empty)"}
			lastName:    {type: "string", minLength: 1, description: "Family name (non-empty)"}
			role:        {type: "string", enum: ["admin", "editor", "viewer"], description: "Access role"}
			isActive:    {type: "boolean", description: "Whether the account is enabled"}
			lastLoginAt: {type: "string", format: "date-time", description: "Last successful login timestamp"}
			createdAt:   {type: "string", format: "date-time", description: "Account creation timestamp"}
		}
	}

	UserListItem: {
		description: "Lightweight user representation used in list responses"
		type: "object"
		required: ["id", "email", "firstName", "lastName", "role", "isActive"]
		properties: {
			id:          {type: "string"}
			email:       {type: "string", format: "email"}
			firstName:   {type: "string"}
			lastName:    {type: "string"}
			role:        {type: "string", enum: ["admin", "editor", "viewer"]}
			isActive:    {type: "boolean"}
			lastLoginAt: {type: "string", format: "date-time"}
		}
	}

	UserFilter: {
		description: "Query parameters for filtering and paginating users"
		type: "object"
		properties: {
			"search.query":  {type: "string", description: "Text search across email/firstName/lastName"}
			"search.fields": {type: "array", items: {type: "string", enum: ["email", "firstName", "lastName"]}, description: "Fields to search within"}
			roleIn:          {type: "array", items: {type: "string", enum: ["admin", "editor", "viewer"]}, description: "Filter by one or more roles"}
			isActive:        {type: "boolean", description: "Filter by account status"}
			"dateRange.from":{type: "string", format: "date-time"}
			"dateRange.to":  {type: "string", format: "date-time"}
			pageNumber:      {type: "integer", minimum: 1}
			pageSize:        {type: "integer", minimum: 1, maximum: 100}
			sortBy:          {type: "string"}
			sortDirection:   {type: "string", enum: ["asc", "desc"]}
		}
	}

	UserBatchUpdateRequest: {
		description: "Batch update request for users"
		type: "object"
		required: ["filter", "updates"]
		properties: {
			filter:  {"$ref": "#/components/schemas/UserFilter"}
			updates: {"$ref": "#/components/schemas/User"}
			dryRun:  {type: "boolean"}
		}
	}

	UserBatchDeleteRequest: {
		description: "Batch delete request for users"
		type: "object"
		required: ["filter"]
		properties: {
			filter:          {"$ref": "#/components/schemas/UserFilter"}
			confirmDeletion: {type: "boolean"}
		}
	}

	UserBatchCreateRequest: {
		description: "Batch create request for users"
		type: "object"
		required: ["items"]
		properties: {
			items:           {type: "array", items: {"$ref": "#/components/schemas/User"}}
			continueOnError: {type: "boolean"}
		}
	}

	// ── Teams ─────────────────────────────────────────────────────────────
	// Derived from resources/teams/types.cue: Resource, ListItem, Filter

	Team: {
		description: "A team grouping users with shared access to resources"
		type: "object"
		required: ["name", "ownerIds", "memberIds"]
		properties: {
			id:          {type: "string", description: "Unique team identifier"}
			name:        {type: "string", minLength: 1, description: "Team display name (non-empty)"}
			description: {type: "string", description: "Optional description of the team's purpose"}
			ownerIds:    {type: "array", items: {type: "string"}, description: "User IDs with owner privileges"}
			memberIds:   {type: "array", items: {type: "string"}, description: "User IDs who are members"}
			isPublic:    {type: "boolean", description: "Whether the team is discoverable by non-members"}
			createdAt:   {type: "string", format: "date-time", description: "Team creation timestamp"}
			updatedAt:   {type: "string", format: "date-time", description: "Last-update timestamp"}
		}
	}

	TeamListItem: {
		description: "Lightweight team representation used in list responses"
		type: "object"
		required: ["id", "name", "memberCount", "ownerCount", "isPublic", "createdAt"]
		properties: {
			id:          {type: "string"}
			name:        {type: "string"}
			memberCount: {type: "integer", minimum: 0}
			ownerCount:  {type: "integer", minimum: 1}
			isPublic:    {type: "boolean"}
			createdAt:   {type: "string", format: "date-time"}
		}
	}

	TeamFilter: {
		description: "Query parameters for filtering and paginating teams"
		type: "object"
		properties: {
			"search.query":       {type: "string"}
			isPublic:             {type: "boolean"}
			ownerIds:             {type: "array", items: {type: "string"}, description: "Filter teams by owner user ID"}
			"hasMembers.userId":  {type: "string", description: "Filter teams that include this user ID as a member"}
			"hasMembers.role":    {type: "string", enum: ["owner", "member"], description: "Filter by the member's role within the team"}
			"dateRange.from":     {type: "string", format: "date-time"}
			"dateRange.to":       {type: "string", format: "date-time"}
			pageNumber:           {type: "integer", minimum: 1}
			pageSize:             {type: "integer", minimum: 1, maximum: 100}
			sortBy:               {type: "string"}
			sortDirection:        {type: "string", enum: ["asc", "desc"]}
		}
	}

	TeamBatchUpdateRequest: {
		description: "Batch update request for teams"
		type: "object"
		required: ["filter", "updates"]
		properties: {
			filter:  {"$ref": "#/components/schemas/TeamFilter"}
			updates: {"$ref": "#/components/schemas/Team"}
			dryRun:  {type: "boolean"}
		}
	}

	TeamBatchDeleteRequest: {
		description: "Batch delete request for teams"
		type: "object"
		required: ["filter"]
		properties: {
			filter:          {"$ref": "#/components/schemas/TeamFilter"}
			confirmDeletion: {type: "boolean"}
		}
	}

	TeamBatchCreateRequest: {
		description: "Batch create request for teams"
		type: "object"
		required: ["items"]
		properties: {
			items:           {type: "array", items: {"$ref": "#/components/schemas/Team"}}
			continueOnError: {type: "boolean"}
		}
	}
}
