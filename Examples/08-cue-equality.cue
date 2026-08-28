// current implementation uses CUE equality to determine if a definitions or inline is redundant. this is essential as
// matchN expressions are parsed as expressions and therefore do not contain the refrerence to the struct value
// keep as sanity check for now

#A: [string, int, bool]
#B: ["header", 1, true]
#C: ["header", 1, false, ...{}]