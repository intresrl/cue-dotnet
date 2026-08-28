using Cue.Generator.Roslyn;
using Microsoft.Extensions.DependencyInjection;

namespace Cue.Generator.Tests;

public sealed class ListGenerationTests
{
    private readonly IRoslynGenerator _generator;

    public ListGenerationTests()
    {
        var services = new ServiceCollection();
        services.RegisterGenerator(null);
        using var serviceProvider = services.BuildServiceProvider();
        _generator = serviceProvider.GetRequiredService<IRoslynGenerator>();
    }

    [Fact]
    public void FixedListGeneratesTuple()
    {
        var code = Generate("""
            #Example: {
                value: [string, int, bool]
            }
            """);

        Assert.Contains("public (string, long, bool) Value", code);
        Assert.DoesNotContain("CueList<", code);
    }

    [Fact]
    public void SingleElementFixedListGeneratesValueTuple()
    {
        var code = Generate("""
            #Example: {
                value: [string]
            }
            """);

        Assert.Contains("public ValueTuple<string> Value", code);
    }

    [Fact]
    public void OpenListWithoutConcreteElementsGeneratesList()
    {
        var code = Generate("""
            #Example: {
                value: [...int]
            }
            """);

        Assert.Contains("public List<long> Value", code);
        Assert.DoesNotContain("CueList<", code);
    }

    [Fact]
    public void MixedListGeneratesCueListWithTupleAndListElementTypes()
    {
        var code = Generate("""
            #Example: {
                value: [string, int, ...bool]
            }
            """);

        Assert.Contains("public CueList<(string, long), bool> Value", code);
        Assert.Contains("public sealed class CueList<TConcrete, TAnyIndex>", code);
        Assert.Contains("public required TConcrete Concrete", code);
        Assert.Contains("public List<TAnyIndex> AnyIndex", code);
    }

    [Fact]
    public void MixedListSupportsMultipleReferencedConcreteTypes()
    {
        var code = Generate("""
            #First: { name: string }
            #Second: { count: int }
            #Example: {
                value: [#First, #Second, ...string]
            }
            """);

        Assert.Contains("public CueList<(First, Second), string> Value", code);
    }

    private string Generate(string cue)
    {
        using var ctx = new CueContext();
        using var value = ctx.Compile(cue);
        return _generator.GenerateCode(CueValueVisitor.VisitRoot(value));
    }
}
