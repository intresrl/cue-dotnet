#DateTimeMessage: {
	type: "datetime"
	format: string
	timezone?: string
}

#TextMessage: {
	type: "text"
	maxLength: int
	pattern?: string
}


#Message: {
	message: #DateTimeMessage | #TextMessage	
}
