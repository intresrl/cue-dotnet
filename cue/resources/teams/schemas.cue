// resources/teams/schemas.cue - OpenAPI component schemas for the teams resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package teams

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	Team: fw.#SchemaFromFields & {
		description: "A team grouping users with shared access to resources"
		required: ["name", "ownerIds", "memberIds"]
		fields: {
			id:          {type: "string", description: "Unique team identifier", optional: true}
			name:        {type: "string", minLength: 1, description: "Team display name (non-empty)"}
			description: {type: "string", description: "Optional description of the team's purpose", optional: true}
			ownerIds:    {type: "array", items: {type: "string"}, description: "User IDs with owner privileges"}
			memberIds:   {type: "array", items: {type: "string"}, description: "User IDs who are members"}
			isPublic:    {type: "boolean", description: "Whether the team is discoverable by non-members", optional: true}
			createdAt:   {type: "string", format: "date-time", description: "Team creation timestamp", optional: true}
			updatedAt:   {type: "string", format: "date-time", description: "Last-update timestamp", optional: true}
		}
	}

	TeamListItem: fw.#SchemaFromFields & {
		description: "Lightweight team representation used in list responses"
		required: ["id", "name", "memberCount", "ownerCount", "isPublic", "createdAt"]
		fields: {
			id:          {type: "string", description: "Team identifier"}
			name:        {type: "string", description: "Team display name"}
			memberCount: {type: "integer", minimum: 0, description: "Number of members"}
			ownerCount:  {type: "integer", minimum: 1, description: "Number of owners"}
			isPublic:    {type: "boolean", description: "Whether the team is public"}
			createdAt:   {type: "string", format: "date-time", description: "Team creation timestamp"}
		}
	}

	TeamFilter: fw.#FilterSchemaOf & {
		resourceName: "Team"
		customFields: {
			isPublic:            {type: "boolean", description: "Filter by team visibility"}
			ownerIds:            {type: "array", items: {type: "string"}, description: "Filter teams by owner user ID"}
			"hasMembers.userId": {type: "string", description: "Filter teams that include this user ID as a member"}
			"hasMembers.role":   {type: "string", enum: ["owner", "member"], description: "Filter by the member's role within the team"}
		}
	}

	TeamBatchCreateRequest: fw.#BatchCreateRequestOf & {
		description: "Batch create request for teams"
		properties: items: {
			description: "Teams to create"
			items: "$ref": "#/components/schemas/Team"
		}
	}

	TeamBatchUpdateRequest: fw.#BatchUpdateRequestOf & {
		description: "Batch update request for teams"
		properties: {
			filter:  "$ref": "#/components/schemas/TeamFilter"
			updates: "$ref": "#/components/schemas/Team"
		}
	}

	TeamBatchDeleteRequest: fw.#BatchDeleteRequestOf & {
		description: "Batch delete request for teams"
		properties: filter: "$ref": "#/components/schemas/TeamFilter"
	}
}
