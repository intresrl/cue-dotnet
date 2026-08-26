// Tests named structs, nested structs, references, open structs,
// and conjunction-based composition between definitions.

#Address: {
	street:  string
	city:    string
	country: string
}

#Person: {
	name:    string
	address: #Address
}

#Entity: {
	id:        string
	createdAt: string
	...
}

#User: #Entity & {
	name:  string
	email: string
}

#Employee: #User & {
	employeeNumber: string
	department:     string
}

#TaggedEntity: #Entity & {
	tags?: [...string]
}

#Profile: {
	displayName: string

	settings: {
		theme:         string
		notifications: bool
	}
}

#StructsAndCompositionExample: {
	person:   #Person
	user:     #User
	employee: #Employee
	tagged:   #TaggedEntity
	profile:  #Profile
}
