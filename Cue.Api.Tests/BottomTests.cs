namespace Cuelang.Cue.Tests;

public class BottomTests
{
    [Fact]
    public void BottomLiteral_CausesCueError()
    {
        const string cueSource = "_|_";
        
        using var ctx = new CueContext();
        var e = Assert.Throws<CueError>(() => ctx.Compile(cueSource, new BuildOption.InferBuiltins(true)));
        Assert.Contains("explicit error (_|_ literal) in source", e.Message);
    }
    
    [Fact]
    public void SimpleConjunctionResolvingAsBottom_CausesCueError()
    {
        const string cueSource = "int & bool";
        
        using var ctx = new CueContext();
        var e = Assert.Throws<CueError>(() => ctx.Compile(cueSource, new BuildOption.InferBuiltins(true)));
        Assert.Equal("conflicting values int and bool (mismatched types int and bool)", e.Message);
    }
    
    [Fact]
    public void StructConjunction_WithDifferentFields_ResolvingAsBottom_CausesCueError()
    {
        const string cueSource = "close({a: int}) & {b: int}";
        
        using var ctx = new CueContext();
        var e = Assert.Throws<CueError>(() => ctx.Compile(cueSource, new BuildOption.InferBuiltins(true)));
        Assert.Equal("b: field not allowed", e.Message);
    }
    
    [Fact]
    public void StructConjunction_WithDifferentTypes_ResolvingAsBottom_CausesCueError()
    {
        const string cueSource = "close({a: int}) & {a: bool}";
        
        using var ctx = new CueContext();
        var e = Assert.Throws<CueError>(() => ctx.Compile(cueSource, new BuildOption.InferBuiltins(true)));
        Assert.Equal("a: conflicting values bool and int (mismatched types bool and int)", e.Message);
    }
}