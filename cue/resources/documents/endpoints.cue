// resources/documents/endpoints.cue - Document CRUD endpoints

package documents

import fw "example.com/apispec/framework"

// Specialize CRUD template for documents
Endpoints: {
	Create: fw.CRUDTemplate.Create & {
		request: Resource
	}

	Read: fw.CRUDTemplate.Read & {
		response: "200": Resource
	}

	List: fw.CRUDTemplate.List & {
		request: filter?: Filter
		response: "200": {
			items: [...ListItem]
			pagination: fw.PaginationMeta
		}
	}

	Update: fw.CRUDTemplate.Update & {
		response: "200": Resource
	}

	Delete: fw.CRUDTemplate.Delete

	BatchCreate: fw.CRUDTemplate.BatchCreate & {
		request: items: [...Resource]
	}

	BatchUpdate: fw.CRUDTemplate.BatchUpdate & {
		request: {
			filter: Filter
			updates: Resource
		}
	}

	BatchDelete: fw.CRUDTemplate.BatchDelete & {
		request: filter: Filter
	}
}
