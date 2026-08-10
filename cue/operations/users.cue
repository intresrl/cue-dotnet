// operations/users.cue - User endpoints registry

package operations

import (
	users "example.com/apispec/resources/users"
)

// User endpoints
UserEndpoints: {
	"POST /users":                       users.Endpoints.Create
	"GET /users/{id}":                   users.Endpoints.Read
	"GET /users":                        users.Endpoints.List
	"PUT /users/{id}":                   users.Endpoints.Update
	"DELETE /users/{id}":                users.Endpoints.Delete
	"POST /users:batch-create":          users.Endpoints.BatchCreate
	"PATCH /users:batch-update":         users.Endpoints.BatchUpdate
	"DELETE /users:batch":               users.Endpoints.BatchDelete
}
