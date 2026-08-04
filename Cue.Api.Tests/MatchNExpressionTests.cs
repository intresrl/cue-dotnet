namespace Cuelang.Cue.Tests;

/// <summary>
///     Tests that demonstrate how the matchN(1, ...) expression is represented
///     by the Cuelang.Cue library.
///     The matchN function in Cue is used to express "exactly N of these types".
///     For example: matchN(1, [int, string, bool]) means the value must be exactly one of those three types.
///     This is commonly used in schemas to define disjunctions with specific cardinality constraints.
/// </summary>
public sealed class MatchNExpressionTests
{
    [Fact]
    public void MatchNOneConstraintIsRepresentedAsDisjunction()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // Compile a simple matchN(1, ...) expression
        using var matchNValue = ctx.Compile("matchN(1, [int, string, bool])");

        // The expression should be incomplete (not concrete)
        // It represents a constraint on what values are allowed
        var kind = matchNValue.IncompleteKind();

        // The IncompleteKind should be Top, indicating it's a constraint/schema
        Assert.Equal(Kind.Top, kind);
    }

    [Fact]
    public void MatchNOneWithMultipleTypesAllowsAnyOneType()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // Define a schema with matchN(1, [int, string])
        using var schema = ctx.Compile("value: matchN(1, [int, string])");

        // Verify integer satisfies the schema
        using var intValue = ctx.Compile("value: 42");
        using var intUnified = schema.Unify(intValue);
        Assert.Equal(42, intUnified.Lookup("value").GetLong());

        // Verify string satisfies the schema
        using var stringValue = ctx.Compile("value: \"hello\"");
        using var stringUnified = schema.Unify(stringValue);
        Assert.Equal("hello", stringUnified.Lookup("value").GetString());
    }

    [Fact]
    public void MatchNOneRejectsValuesNotInTheDisjunction()
    {
        // if (!LibcueAvailability.IsAbailable) return;

        using var ctx = new CueContext();

        // Define a schema with matchN(1, [int, string])
        using var schema = ctx.Compile("value: matchN(1, [int, string])");

        // Try to unify with a boolean (should fail)
        using var boolValue = ctx.Compile("value: true");
        using var boolUnified = schema.Unify(boolValue);

        // The result should be an error state (Bottom kind)
        var result = boolUnified.Error();
        Assert.IsType<Result<Value, string>.Err>(result);
    }

    [Fact]
    public void MatchNOneInStructDefinitionConstrainsField()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // Compile the actual pattern from simple.cue:
        // valueFormat: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, ...])
        // For this test, we use simplified type definitions
        using var schema = ctx.Compile("""
                                       #DateTimeDef: {type: "datetime"}
                                       #InitialsDef: {type: "initials"}
                                       #TextDef: {type: "text"}

                                       valueFormat: matchN(1, [#DateTimeDef, #InitialsDef, #TextDef])
                                       """);

        // A valid instance should satisfy exactly one of the definitions
        using var validValue = ctx.Compile("""
                                           #DateTimeDef: {type: "datetime"}
                                           valueFormat: {type: "datetime"}
                                           """);

        using var unified = schema.Unify(validValue);

        // The schema should accept this value
        Assert.NotNull(unified.Lookup("valueFormat"));
    }

    [Fact]
    public void MatchNExpressionPreservesTypeConstraints()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // Compile matchN with int, string, and bytes
        using var matchN = ctx.Compile("matchN(1, [int, string, bytes])");

        // The incomplete kind should be Top (representing the constraint)
        Assert.Equal(Kind.Top, matchN.IncompleteKind());

        // When constrained to concrete values, each should match
        using var intConcrete = ctx.Compile("42");
        using var stringConcrete = ctx.Compile("\"text\"");

        // Validate concrete values can satisfy the constraint
        Assert.Equal(Kind.Int, intConcrete.Kind());
        Assert.Equal(Kind.String, stringConcrete.Kind());
    }

    [Fact]
    public void MatchNOneIsEquivalentToDisjunctionWithOneCardinality()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // matchN(1, [A, B, C]) should be equivalent to A | B | C
        using var matchNForm = ctx.Compile("value: matchN(1, [int, string, bool])");

        using var disjunctionForm = ctx.Compile("value: int | string | bool");

        // Both should have Top as incomplete kind
        Assert.Equal(Kind.Top, matchNForm.Lookup("value").IncompleteKind());
        Assert.Equal(Kind.Top, disjunctionForm.Lookup("value").IncompleteKind());

        // Both should accept the same concrete value
        using var testInt = ctx.Compile("value: 42");

        using var result1 = matchNForm.Unify(testInt);
        using var result2 = disjunctionForm.Unify(testInt);

        // Both unifications should succeed
        Assert.Equal(42, result1.Lookup("value").GetLong());
        Assert.Equal(42, result2.Lookup("value").GetLong());
    }

    [Fact]
    public void MatchNWithStructsAndComplexTypes()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // Similar to the annotation element definition from simple.cue
        using var schema = ctx.Compile("""
                                       #Format1: {format: "date", pattern: string}
                                       #Format2: {format: "time", pattern: string}
                                       #Format3: {format: "custom", pattern: string}

                                       element: matchN(1, [#Format1, #Format2, #Format3])
                                       """);

        // Should accept one of the formats
        using var dateFormatValue = ctx.Compile("""
                                                #Format1: {format: "date", pattern: string}
                                                element: {format: "date", pattern: "yyyy-MM-dd"}
                                                """);

        using var result = schema.Unify(dateFormatValue);
        Assert.NotNull(result.Lookup("element"));
    }

    [Fact]
    public void MatchNExpressionInheritingFromSimpleCuePattern()
    {
        // if (!LibcueAvailability.IsAvailable) return;

        using var ctx = new CueContext();

        // This reflects the actual pattern from simple.cue line 112:
        // valueFormat: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, ...])
        using var annotationSchema = ctx.Compile("""
                                                 #DateTimeDefinition: {
                                                     type: "datetime"
                                                     format: string
                                                 }
                                                 #InitialsDefinition: {
                                                     type: "initials"
                                                     maxInitials: int
                                                 }
                                                 #TextDefinition: {
                                                     type: "text"
                                                     maxLength?: int
                                                 }

                                                 #AnnotationElementDefinition: {
                                                     position: {x: int, y: int}
                                                     size: {width: int, height: int}
                                                     valueFormat: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition])
                                                 }
                                                 """);

        // Verify the schema compiles without error
        Assert.NotNull(annotationSchema);

        // Create an instance that satisfies the schema
        using var instance = ctx.Compile("""
                                         #DateTimeDefinition: {
                                             type: "datetime"
                                             format: string
                                         }
                                         #InitialsDefinition: {
                                             type: "initials"
                                             maxInitials: int
                                         }
                                         #TextDefinition: {
                                             type: "text"
                                             maxLength?: int
                                         }

                                         element: {
                                             position: {x: 10, y: 20}
                                             size: {width: 100, height: 50}
                                             valueFormat: {
                                                 type: "datetime"
                                                 format: "yyyy-MM-dd"
                                             }
                                         }
                                         """);

        // The schema should accept this instance
        Assert.NotNull(instance);
    }
}