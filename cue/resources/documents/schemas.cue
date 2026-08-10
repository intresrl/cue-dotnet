// resources/documents/schemas.cue - OpenAPI component schemas for the documents resource.
// Domain schemas are derived from types.cue; batch-request shapes use framework templates.

package documents

import fw "example.com/apispec/framework"

// Schemas holds all OpenAPI component schemas owned by this resource package.
// Merged into the global Schemas by operations/schemas.cue.
Schemas: {

	Document: fw.#SchemaFromFields & {
		description: "A document managed by the system"
		required: ["title", "status", "contentType"]
		fields: {
			id:          {type: "string", description: "Unique document identifier", optional: true}
			title:       {type: "string", minLength: 1, description: "Document title (non-empty)"}
			description: {type: "string", description: "Optional human-readable description", optional: true}
			status:      {type: "string", enum: ["draft", "published", "archived"], description: "Publication status"}
			contentType: {type: "string", enum: ["pdf", "docx", "markdown", "plaintext"], description: "File/content type"}
			tags:        {type: "array", items: {type: "string"}, description: "Free-form tag labels", optional: true}
			isPublic:    {type: "boolean", description: "Whether the document is publicly visible", optional: true}
			createdAt:   {type: "string", format: "date-time", description: "Creation timestamp (ISO 8601)", optional: true}
			updatedAt:   {type: "string", format: "date-time", description: "Last-update timestamp (ISO 8601)", optional: true}
		}
	}

	DocumentListItem: fw.#SchemaFromFields & {
		description: "Lightweight document representation used in list responses"
		required: ["id", "title", "status", "createdAt"]
		fields: {
			id:        {type: "string", description: "Document identifier"}
			title:     {type: "string", description: "Document title"}
			status:    {type: "string", enum: ["draft", "published", "archived"], description: "Publication status"}
			createdAt: {type: "string", format: "date-time", description: "Creation timestamp"}
			updatedAt: {type: "string", format: "date-time", description: "Last-update timestamp", optional: true}
		}
	}

	DocumentFilter: fw.#FilterSchemaOf & {
		resourceName: "Document"
		customFields: {
			statusIn: {type: "array", items: {type: "string", enum: ["draft", "published", "archived"]}, description: "Filter by one or more statuses"}
			isPublic: {type: "boolean", description: "Filter by visibility"}
			tagIds:   {type: "array", items: {type: "string"}, description: "Filter by tag identifiers"}
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
