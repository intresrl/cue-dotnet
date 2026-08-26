// Tests core primitive types, concrete values, optional/nullable fields,
// constraints, literal unions, and top-level non-struct definitions.

#PrimitiveTypes: {
	text:    string
	integer: int
	decimal: number
	enabled: bool
}

#ConcreteValues: {
	status:  "active"
	version: 1
	enabled: true
}

#OptionalAndNullable: {
	required:          string
	optional?:         string
	nullable:          null | string
	optionalNullable?: null | string
}

#ConstrainedValues: {
	age:        int & >=0 & <=150
	percentage: number & >=0 & <=100
	email:      string & =~"^.+@.+$"
}

#Status: "pending" | "running" | "completed"
#Priority: 1 | 2 | 3

// Top-level non-struct definitions.
// These exercise primitive and literal definitions directly at definition level.
#AnyString: string
#NonEmptyString: string & !=""
#EmailString: string & =~"^.+@.+$"
#LiteralString: "hello"
#StringChoice: "alpha" | "beta" | "gamma"

#AnyInt: int
#PositiveInt: int & >0
#BoundedInt: int & >=1 & <=10
#LiteralInt: 42
#IntChoice: 1 | 2 | 3

#AnyNumber: number
#PositiveNumber: number & >0
#LiteralNumber: 3.14

#AnyBool: bool
#LiteralBool: true

#NullValue: null

#CoreTypesExample: {
	primitives:  #PrimitiveTypes
	concrete:    #ConcreteValues
	fields:      #OptionalAndNullable
	constrained: #ConstrainedValues
	status:      #Status
	priority:    #Priority
}
