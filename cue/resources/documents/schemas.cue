// resources/documents/schemas.cue - OpenAPI component schemas for the documents resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package documents

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	Document: {
		description: "A document managed by the system"
		type:        "object"
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
		type:        "object"
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
		type:        "object"
		properties: {
			"search.query":         {type: "string",  description: "Full-text search query"}
			"search.caseSensitive": {type: "boolean", description: "Enable case-sensitive search"}
			"search.fuzzy":         {type: "boolean", description: "Enable fuzzy matching"}
			statusIn:               {type: "array", items: {type: "string", enum: ["draft", "published", "archived"]}, description: "Filter by one or more statuses"}
			isPublic:               {type: "boolean", description: "Filter by visibility"}
			tagIds:                 {type: "array", items: {type: "string"}, description: "Filter by tag identifiers"}
			"dateRange.from":       {type: "string", format: "date-time", description: "Earliest createdAt to include"}
			"dateRange.to":         {type: "string", format: "date-time", description: "Latest createdAt to include"}
			pageNumber:             {type: "integer", minimum: 1,   description: "Page number (1-based)"}
			pageSize:               {type: "integer", minimum: 1, maximum: 100, description: "Items per page"}
			sortBy:                 {type: "string",  description: "Field to sort by"}
			sortDirection:          {type: "string",  enum: ["asc", "desc"], description: "Sort direction"}
		}
	}

	DocumentBatchCreateRequest: fw.#BatchCreateRequestOf & {
		description: "Batch create request for documents"
		properties: items: {
			description: "Documents to create"
			items: "$ref": "#/components/schemas/Document"
		}
	}

	DocumentBatchUpdateRequest: fw.#BatchUpdateRequestOf & {
		description: "Batch update request: filter selects documents, updates are applied to all matches"
		properties: {
			filter:  "$ref": "#/components/schemas/DocumentFilter"
			updates: "$ref": "#/components/schemas/Document"
		}
	}

	DocumentBatchDeleteRequest: fw.#BatchDeleteRequestOf & {
		description: "Batch delete request: filter selects documents to delete"
		properties: filter: "$ref": "#/components/schemas/DocumentFilter"
	}
}
