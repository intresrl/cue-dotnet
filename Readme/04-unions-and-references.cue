// Examples of unions and named references.

#EmailContact: {
    address: string
}

#PhoneContact: {
    number: string
}

#Contact: {
    value: #EmailContact | #PhoneContact
}
