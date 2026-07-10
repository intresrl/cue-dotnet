namespace Cuelang.Cue.Tests;

internal static class LibcueAvailability
{
    private static readonly Lazy<bool> IsAvailableLazy = new(() =>
    {
        if (NativeLibrary.TryLoad("cue", out var handle))
        {
            NativeLibrary.Free(handle);
            return true;
        }

        return false;
    });

    public static bool IsAvailable => IsAvailableLazy.Value;
}

