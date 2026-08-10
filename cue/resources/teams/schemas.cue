// resources/teams/schemas.cue - OpenAPI component schemas for the teams resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package teams

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	Team: {
		description: "A team grouping users with shared access to resources"
		type:        "object"
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
		type:        "object"
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
		type:        "object"
		properties: {
			"search.query":      {type: "string"}
			isPublic:            {type: "boolean"}
			ownerIds:            {type: "array", items: {type: "string"}, description: "Filter teams by owner user ID"}
			"hasMembers.userId": {type: "string", description: "Filter teams that include this user ID as a member"}
			"hasMembers.role":   {type: "string", enum: ["owner", "member"], description: "Filter by the member's role within the team"}
			"dateRange.from":    {type: "string", format: "date-time"}
			"dateRange.to":      {type: "string", format: "date-time"}
			pageNumber:          {type: "integer", minimum: 1}
			pageSize:            {type: "integer", minimum: 1, maximum: 100}
			sortBy:              {type: "string"}
			sortDirection:       {type: "string", enum: ["asc", "desc"]}
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
