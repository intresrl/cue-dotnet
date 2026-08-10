// framework/crud.cue - Generic CRUD template for all resources

package framework

// Parameterized CRUD operations template
// Each operation defines request/response with placeholders for resource-specific types
// Specialization happens by combining with concrete resource definitions
CRUDTemplate: {
	// Create operation - POST endpoint
	Create: {
		request: _  // Filled by specialization with resource type
		response: {
			"201": {
				resourceId: string
			}
			"400": ErrorResponse
			"409": ErrorResponse
			"422": ErrorResponse
		}
	}

	// Read operation - GET {id} endpoint
	Read: {
		request: {
			id: string
		}
		response: {
			"200": _  // Filled by specialization with resource type
			"400": ErrorResponse
			"401": ErrorResponse
			"404": ErrorResponse
		}
	}

	// List operation - GET endpoint with filtering
	List: {
		request: {
			filter?: _  // Filled by specialization with filter type
			pageNumber?: int & >=1
			pageSize?: int & >=1 & <=100
			sortBy?: string
			sortDirection?: "asc" | "desc"
		}
		response: {
			"200": {
				items: [...]
				pagination: PaginationMeta
			}
			"400": ErrorResponse
			"401": ErrorResponse
		}
	}

	// Update operation - PUT {id} endpoint
	Update: {
		request: {
			id: string
		}
		response: {
			"200": _  // Filled by specialization with resource type
			"400": ErrorResponse
			"401": ErrorResponse
			"404": ErrorResponse
			"409": ErrorResponse
		}
	}

	// Delete operation - DELETE {id} endpoint
	Delete: {
		request: {
			id: string
		}
		response: {
			"204": {
				acknowledged: bool
			}
			"400": ErrorResponse
			"401": ErrorResponse
			"404": ErrorResponse
		}
	}

	// Batch create operation
	BatchCreate: {
		request: {
			items: [...]  // Filled by specialization
			continueOnError?: bool
		}
		response: {
			"207": {
				succeeded: int
				failed: int
				results: [...{
					index: int
					success: bool
					resourceId?: string
					error?: ErrorDetail
				}]
			}
			"400": ErrorResponse
		}
	}

	// Batch update operation with filter
	BatchUpdate: {
		request: {
			filter: _  // Filled by specialization with filter type
			updates: _  // Filled by specialization with resource type
			dryRun?: bool
		}
		response: {
			"200": {
				updated: int
				skipped: int
				dryRun: bool
			}
			"400": ErrorResponse
			"422": ErrorResponse
		}
	}

	// Batch delete operation with filter
	BatchDelete: {
		request: {
			filter: _  // Filled by specialization with filter type
			confirmDeletion?: bool
		}
		response: {
			"200": {
				deleted: int
				skipped: int
			}
			"400": ErrorResponse
		}
	}
}
