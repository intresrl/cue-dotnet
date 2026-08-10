// operations/teams.cue - Team endpoints registry

package operations

import (
	teams "example.com/apispec/resources/teams"
)

// Team endpoints
TeamEndpoints: {
	"POST /teams":                       teams.Endpoints.Create
	"GET /teams/{id}":                   teams.Endpoints.Read
	"GET /teams":                        teams.Endpoints.List
	"PUT /teams/{id}":                   teams.Endpoints.Update
	"DELETE /teams/{id}":                teams.Endpoints.Delete
	"POST /teams:batch-create":          teams.Endpoints.BatchCreate
	"PATCH /teams:batch-update":         teams.Endpoints.BatchUpdate
	"DELETE /teams:batch":               teams.Endpoints.BatchDelete
}
