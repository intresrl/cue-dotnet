// Integration-style example combining core types, composition,
// lists, references, nullability, constraints, and discriminated unions.

#UserStatus: "active" | "disabled"
#Role: "admin" | "editor" | "viewer"

#Address: {
	street:  string
	city:    string
	country?: null | string
}

#BaseEntity: {
	id: string
	...
}

#User: #BaseEntity & {
	name:    string
	email:   string & =~"^.+@.+$"
	status:  #UserStatus
	roles:   [...#Role]
	address?: null | #Address
}

#PhysicalProduct: {
	type:   "physical"
	sku:    string
	weight: number & >0
}

#DigitalProduct: {
	type:        "digital"
	sku:         string
	downloadUrl: string
}

#Product: {
	value: #PhysicalProduct | #DigitalProduct
}

#OrderLine: {
	product:  #Product
	quantity: int & >0
}

#Order: #BaseEntity & {
	customer: #User
	lines:    [...#OrderLine]
}

#Organization: {
	name:   string
	users:  [...#User]
	orders: [...#Order]
}

#CompleteDomainModel: {
	organization: #Organization
}
