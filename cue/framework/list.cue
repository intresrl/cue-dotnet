package framework

// PaginationMeta is metadata included in every list response
#PaginationMeta: {
	// Current page (1-based)
	pageNumber: int
	// Items per page
	pageSize: int
	// Total items across all pages
	totalCount: int
	// Whether more pages exist
	hasMore: bool
}

#WithPagination: {
    items: [... _]
    pagination: #PaginationMeta
}
