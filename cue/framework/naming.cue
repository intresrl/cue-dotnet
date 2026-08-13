package framework

#CRUDNaming: {
    ...
	resourceName:       string

	pluralResourceName: resourceName + "s"
	tagName:                resourceName
	tagDescription:         resourceName + " endpoints"
	idDescription:          resourceName + " ID"
	listName:               resourceName + "List"
	listDescription:        "List " + pluralResourceName
	createName:             resourceName + "Create"
	createDescription:      "Create single " + resourceName
	readName:               resourceName + "Get"
	readDescription:        "Get single " + resourceName
	updateName:             resourceName + "Update"
	updateDescription:      "Update single " + resourceName
	deleteName:             resourceName + "Delete"
	deleteDescription:      "Delete single " + resourceName
	batchCreateName:        resourceName + "BatchCreate"
	batchCreateDescription: "Batch create " + pluralResourceName
	batchUpdateName:        resourceName + "BatchUpdate"
	batchUpdateDescription: "Batch update " + pluralResourceName
	batchDeleteName:        resourceName + "BatchDelete"
	batchDeleteDescription: "Batch delete " + pluralResourceName
}
