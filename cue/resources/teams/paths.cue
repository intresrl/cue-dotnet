// resources/teams/paths.cue - OpenAPI path items for teams
// PathItems are constructed from Endpoints operations defined in endpoints.cue

package teams

// Build path items from Endpoints operations
PathItems: {
	"/teams": {
		post: Endpoints.Create
		get: Endpoints.List
	}
	"/teams/{id}": {
		get: Endpoints.Read
		put: Endpoints.Update
		delete: Endpoints.Delete
	}
	"/teams:batch-create": {
		post: Endpoints.BatchCreate
	}
	"/teams:batch-update": {
		patch: Endpoints.BatchUpdate
	}
	"/teams:batch": {
		delete: Endpoints.BatchDelete
	}
}

// Tag definition for OpenAPI
Tag: {
	name: "teams"
	description: "Team management endpoints"
}
