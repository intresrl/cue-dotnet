// operations/api.cue - Top-level API metadata.
// This is the single place to update version, title, description, and server URLs.

package operations

// APISpec holds all API-wide metadata consumed by OpenAPIDoc.
APISpec: {
	version:     "1.0.0"
	title:       "Document Management & Collaboration API"
	description: "Complete API with modular namespaces and CRUD patterns"
	servers: [
		{url: "http://localhost:8080",   description: "Development"},
		{url: "https://api.example.com", description: "Production"},
	]
}
