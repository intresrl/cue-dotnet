package framework

#R400: {
	...
	"400": {
		description: "Bad request"
		content:     _schemas.ErrorResponse
	}
}

#R404: {
	...
	"404": {
		description: "Not found"
		content:     _schemas.ErrorResponse
	}
}

#R409: {
	...
	"409": {
		description: "Conflict"
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
