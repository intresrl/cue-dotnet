// Tests open lists, lists of primitives, lists of definitions,
// inline list element structs, maps, nested object graphs,
// top-level list definitions, and fixed/index-specific list schemas.
//
// NOTE: fixed-position/index-specific lists are included intentionally as
// examples even though the current visitor/generator does not fully support them.

#Role: "admin" | "editor" | "viewer"

#Item: {
	sku:      string
	quantity: int & >0
}

#Order: {
	id:    string
	items: [...#Item]
}

#User: {
	id:    string
	roles: [...#Role]
}

#InlineOrder: {
	items: [...{
		sku:      string
		quantity: int
	}]
}

#Scores: {
	[string]: int
}

#Department: {
	name:    string
	members: [...#User]
}

#Organization: {
	name:        string
	departments: [...#Department]
}

// Top-level list definitions.
#StringList: [...string]
#IntegerList: [...int]
#RoleList: [...#Role]
#ItemList: [...#Item]

// Fixed/index-specific list definitions.
// These document expected CUE shapes even if they are currently ignored.
#FixedPrimitiveTuple: [string, int, bool]

#FixedLiteralTuple: ["header", 1, true]

#FixedLiteralTupleWithTail: ["header", 1, false, ...{}]

#MixedTuple: [
	string,
	int & >=0,
	null | string,
]

#FixedStructTuple: [
	{
		id: string
	},
	{
		count: int
	},
]

#CollectionsAndReferencesExample: {
	order:        #Order
	user:         #User
	inlineOrder:  #InlineOrder
	scores:       #Scores
	organization: #Organization

	strings: #StringList
	items:   #ItemList
}
