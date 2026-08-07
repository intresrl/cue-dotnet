using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal readonly unsafe struct NativeUtf8String : IDisposable
{
    private NativeUtf8String(byte* str)
    {
        Str = str;
    }

    public static NativeUtf8String From(string value)
    {
        return new NativeUtf8String((byte*)Marshal.StringToCoTaskMemUTF8(value));
    }

    public byte* Str { get; }

    public void Dispose()
    {
        Marshal.FreeCoTaskMem((nint)Str);
    }
}