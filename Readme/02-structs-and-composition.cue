// Examples of struct definitions with required/optional/nullable fields.

#Address: {
    street:   string
    city:     string
    country:  string
}

#Person: {
    name:    string
    address: #Address
    email?:  string                  // optional field
    notes:   null | string           // nullable field
}

#Employee: {
    name:              string
    email?:            string
    notes:             null | string
    employeeNumber:    string
    department:        string
}



