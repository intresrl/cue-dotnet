namespace Cuelang.Cue.Tests;

internal static class LibcueAvailability
{
    private static readonly Lazy<bool> IsAvailableLazy = new(() =>
    {
        // Go-compiled DLLs cannot be unloaded (the Go runtime does not support
        // FreeLibrary), so we intentionally keep the handle open and let the
        // OS reclaim it when the process exits.
        return NativeLibrary.TryLoad("cue", out _);
    });

    public static bool IsAvailable => IsAvailableLazy.Value;
}

