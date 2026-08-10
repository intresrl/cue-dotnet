// operations/documents.cue - Document endpoints registry

package operations

import (
	docs "example.com/apispec/resources/documents"
)

// Document endpoints
DocumentEndpoints: {
	"POST /documents":                   docs.Endpoints.Create
	"GET /documents/{id}":               docs.Endpoints.Read
	"GET /documents":                    docs.Endpoints.List
	"PUT /documents/{id}":               docs.Endpoints.Update
	"DELETE /documents/{id}":            docs.Endpoints.Delete
	"POST /documents:batch-create":      docs.Endpoints.BatchCreate
	"PATCH /documents:batch-update":     docs.Endpoints.BatchUpdate
	"DELETE /documents:batch":           docs.Endpoints.BatchDelete
}
