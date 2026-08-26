// Tests primitive disjunctions, named struct unions,
// discriminated unions, alternative discriminator field names,
// nested polymorphism, and lists of discriminated union values.

#StringOrInteger: {
	value: string | int
}

#EmailContact: {
	address: string
}

#PhoneContact: {
	number: string
}

#Contact: {
	value: #EmailContact | #PhoneContact
}

#Cat: {
	type:  "cat"
	name:  string
	lives: int
}

#Dog: {
	type:   "dog"
	name:   string
	breed?: string
}

#Bird: {
	type:   "bird"
	name:   string
	canFly: bool
}

#Pet: {
	value: #Cat | #Dog | #Bird
}

#CreatedEvent: {
	status: "created"
	id:     string
}

#DeletedEvent: {
	status: "deleted"
	id:     string
}

#Event: {
	value: #CreatedEvent | #DeletedEvent
}

#Circle: {
	kind:   "circle"
	radius: number
}

#Rectangle: {
	kind:   "rectangle"
	width:  number
	height: number
}

#Drawing: {
	name:  string
	shape: #Circle | #Rectangle
}

#Zoo: {
	animals: [...(#Cat | #Dog | #Bird)]
}

#UnionsAndDiscriminatorsExample: {
	primitiveUnion: #StringOrInteger
	contact:        #Contact
	pet:            #Pet
	event:          #Event
	drawing:        #Drawing
	zoo:            #Zoo
}
