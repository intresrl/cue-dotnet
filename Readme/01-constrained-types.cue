// Examples of constrained primitive types showing type selection and validation logic.

#Port: int & >=1 & <=65535
#Age: int & >=0 & <=150
#Percentage: number & >=0 & <=100
#CryptographicHash: int & >0              // unbounded, uses BigInteger
#Timestamp: 1234567890123456789           // large literal, uses BigInteger
#Precision: 3.141592653589793238462643383279  // arbitrary precision, uses BigDecimal
#EmailString: string & =~"^.+@.+$"
#Status: "pending" | "active" | "done"
