// resources/users/schemas.cue - OpenAPI component schemas for the users resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package users

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	User: fw.#SchemaFromFields & {
		description: "A user account in the system"
		required: ["email", "firstName", "lastName", "role"]
		fields: {
			id: {type: "string", description: "Unique user identifier", optional: true}
			email: {type: "string", format: "email", description: "User's email address"}
			firstName: {type: "string", minLength: 1, description: "Given name (non-empty)"}
			lastName: {type: "string", minLength: 1, description: "Family name (non-empty)"}
			role: {type: "string", enum: ["admin", "editor", "viewer"], description: "Access role"}
			isActive: {type: "boolean", description: "Whether the account is enabled", optional: true}
			lastLoginAt: {type: "string", format: "date-time", description: "Last successful login timestamp", optional: true}
			createdAt: {type: "string", format: "date-time", description: "Account creation timestamp", optional: true}
		}
	}

	UserListItem: fw.#SchemaFromFields & {
		description: "Lightweight user representation used in list responses"
		required: ["id", "email", "firstName", "lastName", "role", "isActive"]
		fields: {
			id: {type: "string", description: "User identifier"}
			email: {type: "string", format: "email", description: "User's email address"}
			firstName: {type: "string", description: "Given name"}
			lastName: {type: "string", description: "Family name"}
			role: {type: "string", enum: ["admin", "editor", "viewer"], description: "Access role"}
			isActive: {type: "boolean", description: "Whether the account is enabled"}
			lastLoginAt: {type: "string", format: "date-time", description: "Last successful login timestamp", optional: true}
		}
	}

	UserFilter: fw.#FilterSchemaOf & {
		resourceName: "User"
		customFields: {
			"search.fields": {type: "array", items: {type: "string", enum: ["email", "firstName", "lastName"]}, description: "Fields to search within"}
			roleIn: {type: "array", items: {type: "string", enum: ["admin", "editor", "viewer"]}, description: "Filter by one or more roles"}
			isActive: {type: "boolean", description: "Filter by account status"}
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
			filter: "$ref":  "#/components/schemas/UserFilter"
			updates: "$ref": "#/components/schemas/User"
		}
	}

	UserBatchDeleteRequest: fw.#BatchDeleteRequestOf & {
		description: "Batch delete request for users"
		properties: filter: "$ref": "#/components/schemas/UserFilter"
	}
}
