package framework

#R400: {
	...
	"400": {
		description: "Bad request"
		content:     _schemas.ErrorResponse
	}
}

#R422: {
	...
	"422": {
		description: "Validation error"
		content:     _schemas.ErrorResponse
	}
}
