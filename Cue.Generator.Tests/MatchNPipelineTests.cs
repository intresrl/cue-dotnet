namespace Cue.Generator.Tests;

public sealed class MatchNPipelineTests
{
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
        var fields = resultField.Fields(true);
        
        Console.WriteLine($"=== MatchN Value Diagnostics ===");
        Console.WriteLine($"Kind: {kind}");
        Console.WriteLine($"IncompleteKind: {incompleteKind}");
        Console.WriteLine($"Disjunctions count: {disjunctions.Length}");
        Console.WriteLine($"Fields count: {fields.Length}");
        Console.WriteLine($"Path: {resultField.Path()}");
        
        // Try to iterate fields
        if (fields.Length > 0)
        {
            Console.WriteLine($"Fields:");
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
        
        Console.WriteLine($"=== Pipe vs MatchN Comparison ===");
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

        var node = value.ToCueValueNode();
        Console.WriteLine($"\n=== String Value Inspection ===");
        Console.WriteLine($"Root: {node}\n");

        if (node is not CueStructValue root) return;

        foreach (var field in root.Fields)
        {
            Console.WriteLine($"Field '{field.Name}': {field.Value}");
            
            if (field.Value is CueSimpleValue simple)
            {
                Console.WriteLine($"  -> Simple value, kind={simple.Kind}");
            }
            else if (field.Value is CueDisjunction disj)
            {
                Console.WriteLine($"  -> Disjunction with {disj.Branches.Count} branches");
                Console.WriteLine($"  -> Is discriminated: {disj.IsDiscriminated}");
                foreach (var (idx, branch) in disj.Branches.Select((b, i) => (i, b)))
                {
                    Console.WriteLine($"     Branch {idx}: {branch}");
                }
            }
            
            // For structs, inspect nested fields
            if (field.Value is CueStructValue fieldStruct)
            {
                foreach (var inner in fieldStruct.Fields)
                {
                    Console.WriteLine($"  Nested '{inner.Name}': {inner.Value}");
                }
            }
        }
    }
}
