using Cue.Generator.Tests.CueValueNodeVisitor;

namespace Cue.Generator.Tests;

public sealed class MatchNPipelineTests
{
    private static Kind GetKind(CueValueNode node)
    {
        return node switch
        {
            CueBottomValue => Kind.Bottom,
            CueNullValue => Kind.Null,
            CueBoolValue => Kind.Bool,
            CueIntValue => Kind.Int,
            CueFloatValue => Kind.Float,
            CueStringValue => Kind.String,
            CueBytesValue => Kind.Bytes,
            CueNumberValue => Kind.Number,
            CueTopValue => Kind.Top,
            CueStructValue => Kind.Struct,
            CueListValue => Kind.List,
            _ => Kind.Top
        };
    }

    [Fact]
    public void DiagnoseMatchNValue()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            result: matchN(1, [#TypeA, #TypeB])
            """);

        using var resultField = value.Lookup("result");
        
        // Diagnose the value structure
        var kind = resultField.Kind();
        var incompleteKind = resultField.IncompleteKind();
        var disjunctions = resultField.Disjunctions();
        var fields = resultField.Fields(new EvalOption.Definitions(true), new EvalOption.Optionals(true));
        
        Console.WriteLine("=== MatchN Value Diagnostics ===");
        Console.WriteLine($"Kind: {kind}");
        Console.WriteLine($"IncompleteKind: {incompleteKind}");
        Console.WriteLine($"Disjunctions count: {disjunctions.Length}");
        Console.WriteLine($"Fields count: {fields.Length}");
        Console.WriteLine($"Path: {resultField.Path()}");
        
        // Try to iterate fields
        if (fields.Length > 0)
        {
            Console.WriteLine("Fields:");
            foreach (var f in fields)
            {
                Console.WriteLine($"  - {f.Path()}");
                f.Dispose();
            }
        }
        
        // Cleanup
        foreach (var d in disjunctions)
        {
            d.Dispose();
        }
    }

    [Fact]
    public void ComparePipeVsMatchN()
    {
        // Test with pipe union (|)
        using var ctx1 = new CueContext();
        using var value1 = ctx1.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            result: #TypeA | #TypeB
            """);

        using var pipeField = value1.Lookup("result");
        var pipeDisjunctions = pipeField.Disjunctions();
        var pipeKind = pipeField.Kind();
        
        // Test with matchN
        using var ctx2 = new CueContext();
        using var value2 = ctx2.Compile("""
            #TypeA: {type: "a", data: string}
            #TypeB: {type: "b", data: int}
            
            result: matchN(1, [#TypeA, #TypeB])
            """);

        using var matchField = value2.Lookup("result");
        var matchDisjunctions = matchField.Disjunctions();
        var matchKind = matchField.Kind();
        
        Console.WriteLine("=== Pipe vs MatchN Comparison ===");
        Console.WriteLine($"Pipe - Kind: {pipeKind}, Disjunctions: {pipeDisjunctions.Length}");
        Console.WriteLine($"MatchN - Kind: {matchKind}, Disjunctions: {matchDisjunctions.Length}");
        
        // Cleanup
        foreach (var d in pipeDisjunctions) d.Dispose();
        foreach (var d in matchDisjunctions) d.Dispose();
    }

    [Fact]
    public void TestStringConstantVsConstraint()
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile("""
            // Constant string values
            constantA: "hello"
            constantB: "a" | "b"
            
            // String constraint (any string)
            constraint: string
            
            // Struct with mixed string fields
            config: {
                name: "config"
                type: string
                choices: "x" | "y" | "z"
            }
            """);

        var node = CueValueVisitor.ForTests(value);
        Console.WriteLine("\n=== String Value Inspection ===");
        Console.WriteLine($"Root: {node}\n");

        if (node is not CueStructValue root) return;

        foreach (var field in root.Fields)
        {
            Console.WriteLine($"Field '{field.Name}': {field.Value}");
            
            switch (field.Value)
            {
                case CueDisjunction dis:
                {
                    Console.WriteLine($"  -> Disjunction with {dis.Branches.Count} branches");
                    Console.WriteLine($"  -> Is discriminated: {dis.IsDiscriminated}");
                    foreach (var (idx, branch) in dis.Branches.Select((b, i) => (i, b)))
                    {
                        Console.WriteLine($"     Branch {idx}: {branch}");
                    }

                    break;
                }
                // For structs, inspect nested fields
                case CueStructValue fieldStruct:
                {
                    foreach (var inner in fieldStruct.Fields)
                    {
                        Console.WriteLine($"  Nested '{inner.Name}': {inner.Value}");
                    }

                    break;
                }
                case CueListValue or null:
                    break;
                default:
                    Console.WriteLine($"  -> Simple value, kind={GetKind(field.Value)}");
                    break;

            }
        }
    }

    [Fact]
    public void TestValueIsConcrete()
    {
        using var ctx = new CueContext();
        
        Console.WriteLine("=== Concrete Values (IsConcrete == true) ===\n");
        
        // Test constant string
        using var constStr = ctx.Compile("""value: "hello" """);
        var strField = constStr.Lookup("value");
        Assert.True(strField.IsConcrete(), "Constant string should be concrete");
        Assert.Equal(Kind.String, strField.Kind());
        var decodedStr = strField.GetString();
        Console.WriteLine($"String: {strField.IsConcrete()} => \"{decodedStr}\"");
        Assert.Equal("hello", decodedStr);
        
        // Test constant int
        using var constNum = ctx.Compile("value: 42 ");
        var numField = constNum.Lookup("value");
        Assert.True(numField.IsConcrete(), "Constant number should be concrete");
        Assert.Equal(Kind.Int, numField.Kind());
        var decodedNum = numField.GetLong();
        Console.WriteLine($"Int: {numField.IsConcrete()} => {decodedNum}");
        Assert.Equal(42L, decodedNum);
        
        // Test constant float
        using var constFlt = ctx.Compile("value: 3.14 ");
        var fltField = constFlt.Lookup("value");
        Assert.True(fltField.IsConcrete(), "Constant float should be concrete");
        Assert.Equal(Kind.Float, fltField.Kind());
        var decodedFlt = fltField.GetDouble();
        Console.WriteLine($"Float: {fltField.IsConcrete()} => {decodedFlt}");
        Assert.True(Math.Abs(decodedFlt - 3.14) < 0.01);
        
        // Test constant boolean
        using var constBool = ctx.Compile("value: true ");
        var boolField = constBool.Lookup("value");
        Assert.True(boolField.IsConcrete(), "Constant boolean should be concrete");
        Assert.Equal(Kind.Bool, boolField.Kind());
        var decodedBool = boolField.GetBoolean();
        Console.WriteLine($"Bool: {boolField.IsConcrete()} => {decodedBool}");
        Assert.True(decodedBool);
        
        Console.WriteLine("\n=== Constraints (IsConcrete == false) ===\n");
        
        // Test string constraint
        using var constraintStr = ctx.Compile("value: string ");
        var strConstraint = constraintStr.Lookup("value");
        Assert.False(strConstraint.IsConcrete(), "String constraint should not be concrete");
        Console.WriteLine($"String constraint: {strConstraint.IsConcrete()} (kind: {strConstraint.Kind()})");
        
        // Test int constraint
        using var constraintNum = ctx.Compile("value: int ");
        var numConstraint = constraintNum.Lookup("value");
        Assert.False(numConstraint.IsConcrete(), "Int constraint should not be concrete");
        Console.WriteLine($"Int constraint: {numConstraint.IsConcrete()} (kind: {numConstraint.Kind()})");
        
        Console.WriteLine("\n=== Unions (IsConcrete == false) ===\n");
        
        // Test string union
        using var unionStr = ctx.Compile("""value: "a" | "b" | "c" """);
        var unionField = unionStr.Lookup("value");
        Assert.False(unionField.IsConcrete(), "String union should not be concrete");
        var disjuncts = unionField.Disjunctions();
        Console.WriteLine($"String union: {unionField.IsConcrete()} ({disjuncts.Length} branches)");
        foreach (var (idx, branch) in disjuncts.Select((b, i) => (i, b)))
        {
            var decodedBranch = branch.GetString();
            Console.WriteLine($"  Branch {idx}: \"{decodedBranch}\"");
        }
        
        // Test int union
        using var unionNum = ctx.Compile("value: 1 | 2 | 3 ");
        var unionNumField = unionNum.Lookup("value");
        Assert.False(unionNumField.IsConcrete(), "Int union should not be concrete");
        var numDisjuncts = unionNumField.Disjunctions();
        Console.WriteLine($"Int union: {unionNumField.IsConcrete()} ({numDisjuncts.Length} branches)");
        foreach (var (idx, branch) in numDisjuncts.Select((b, i) => (i, b)))
        {
            var decodedBranch = branch.GetLong();
            Console.WriteLine($"  Branch {idx}: {decodedBranch}");
        }
    }
}
