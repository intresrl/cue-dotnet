// Examples of lists, tuples, and nested structures.

#Profile: {
    displayName: string
    settings: {
        theme:         string
        notifications: bool
    }
    tags?: [...string]
}

#Order: {
    id:    string
    items: [...{sku: string, quantity: int}]
}

// Fixed-position list (concrete indexes)
#Coordinates: [number, number, number]

// Open list (any index)
#Numbers: [...int]
