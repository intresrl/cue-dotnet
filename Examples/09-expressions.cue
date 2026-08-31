package operators

// Keep values non-concrete where useful so Expr() preserves
// the operator instead of reducing everything immediately.
x: number
y: number
s: string
b: bool

#Add: x + y
#Sub: x - y
#Mul: x * y
#Div: x / y
#UnaryPlus:  +x
#UnaryMinus: -x
#StringAdd: s + "x"
#StringMul: s * 2
#LogicalAnd: b && true
#LogicalOr:  b || false
#LogicalNot: !b
#Equal:        x == y
#NotEqual:     x != y
#Less:         x < y
#LessEqual:    x <= y
#Greater:      x > y
#GreaterEqual: x >= y
#RegexMatch:    s =~ "^x"
#RegexNotMatch: s !~ "^x"
//#BoundEqual:        ==1 makes Cue crash
#BoundNotEqual:     !=1
#BoundLess:         <1
#BoundLessEqual:    <=1
#BoundGreater:      >1
#BoundGreaterEqual: >=1
#BoundRegexMatch:    =~"^x"
#BoundRegexNotMatch: !~"^x"
#Unify: int & >=0
#Or: int | string
#Default: *1 | 2
#Call: len(s)

// Selector / index / slice

obj: { a: int }
list: [1, 2, 3, ...int]

#Selector: obj.a
#Index:    list[0]
#Slice:    list[0:2]

// Interpolation

#Interpolation: "value=\(x)"

// Composite expressions, useful for checking precedence / nested Expr.Values

#ArithmeticTree: x + y * 2
#BooleanTree:   (x > 0) && (y <= 10)
#MixedTree:     (int & >=0) | string

DateTimeMessage: {
	type: "datetime"
	format: string
	timezone?: string
}

TextMessage: {
	type: "text"
	maxLength: int
	pattern?: string
}

#matchNMessage: matchN(1, [DateTimeMessage, TextMessage])
#simpleOrMessage: DateTimeMessage | TextMessage
