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
	matchNMessage: matchN(1, [#DateTimeMessage, #TextMessage])
	simpleOrMessage: #DateTimeMessage | #TextMessage
}
