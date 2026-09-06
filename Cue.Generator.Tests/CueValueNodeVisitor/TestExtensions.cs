namespace Cue.Generator.Tests.CueValueNodeVisitor;

public static class TestExtensions
{
    public static CueValueNode CueVisit(Value value)
    {
        return new CueValueVisitor([], null, new CueExprVisitor(null)).Visit(value);
    }
}