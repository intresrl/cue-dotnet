namespace Cuelang.Cue.Tests;

internal static class TestGuard
{
    public static void RequireLibcue()
    {
        if (!LibcueAvailability.IsAvailable)
        {
        }
    }
}

