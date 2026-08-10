// resources/users/schemas.cue - OpenAPI component schemas for the users resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package users

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	User: {
		description: "A user account in the system"
		type:        "object"
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
		type:        "object"
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
		type:        "object"
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

	UserBatchCreateRequest: fw.#BatchCreateRequestOf & {
		description: "Batch create request for users"
		properties: items: {
			description: "Users to create"
			items: "$ref": "#/components/schemas/User"
		}
	}

	UserBatchUpdateRequest: fw.#BatchUpdateRequestOf & {
		description: "Batch update request for users"
		properties: {
			filter:  "$ref": "#/components/schemas/UserFilter"
			updates: "$ref": "#/components/schemas/User"
		}
	}

	UserBatchDeleteRequest: fw.#BatchDeleteRequestOf & {
		description: "Batch delete request for users"
		properties: filter: "$ref": "#/components/schemas/UserFilter"
	}
}
