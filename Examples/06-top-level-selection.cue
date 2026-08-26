// Tests generator root selection behavior.
//
// Expected behavior:
// - top-level #definitions are candidates for conversion;
// - ordinary top-level fields/properties are ignored.
//
// The plain fields below are intentionally valid CUE values but are not definitions.

plainString: "this top-level property should be ignored"
plainInt: 123
plainBool: true

plainStruct: {
	name: "ignored"
}

plainList: [
	"ignored",
	"also ignored",
]

// These definitions should be discovered and converted.
#ConvertedString: string

#ConvertedPerson: {
	name: string
	age:  int
}

#ConvertedList: [...string]

#ConvertedSettings: {
	enabled: bool
	mode:    "simple" | "advanced"
}
