#AnyString: string
#AnyBool: bool
#ExactInt: 42
#PositiveInt: int & >0
#NonNegativeInt: int & >=0
#SmallInt: int & >=0 & <=100
#Percentage: number & >=0 & <=100
#Port: int & >=1 & <=65535
#NegativeInt: int & <0
#NonPositiveInt: int & <=0
#ZeroOrPositiveLarge: 0 | >=1000
#OneOfThree: 1 | 5 | 10
#OutsideRange: <0 | >100
#ByteLike: int & >=0 & <=255
#Int16Like: int & >=-32768 & <=32767
#ExactString: "active"
#ExactBool: true