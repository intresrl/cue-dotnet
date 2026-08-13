package framework

#Pagination: [
	{
		name:     "pageNumber"
		required: true
		schema:   _refs.pageNumber
	},
	{
		name:     "pageSize"
		required: true
		schema:   _refs.pageSize
	},
	{
		name:   "sortBy"
		schema: _refs.sortBy
	},
	{
		name:   "sortDirection"
		schema: _refs.sortDirection
	},
]
