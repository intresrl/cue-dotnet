// Tests matchN-based exclusive alternatives and combinations
// of matchN definitions inside larger structs and lists.

#EmailValue: {
	type:    "email"
	address: string
}

#PhoneValue: {
	type:   "phone"
	number: string
}

#ContactValue: {
	value: matchN(1, [
		#EmailValue,
		#PhoneValue,
	])
}

#TextValue: {
	kind:  "text"
	value: string
}

#NumberValue: {
	kind:  "number"
	value: number
}

#BooleanValue: {
	kind:  "boolean"
	value: bool
}

#AnnotationValue: {
	value: matchN(1, [
		#TextValue,
		#NumberValue,
		#BooleanValue,
	])
}

#CompositeRecord: {
	id:          string
	annotation:  #AnnotationValue
	contacts:    [...#ContactValue]
	description?: null | string
}

#MatchNAndAdvancedExample: {
	record: #CompositeRecord
}
