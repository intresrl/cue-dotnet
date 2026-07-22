using System.Runtime.InteropServices;
using System.Text;

namespace Cuelang.Cue;

internal static unsafe class NativeMarshalling
{
    public static byte* AllocUtf8(string value)
    {
        return (byte*)Marshal.StringToCoTaskMemUTF8(value);
    }

    public static void FreeUtf8(byte* value)
    {
        if (value != null)
        {
            Marshal.FreeCoTaskMem((IntPtr)value);
        }
    }

    // Strings returned by the native library are Go-managed; the caller must NOT free them.
    public static string PtrToUtf8AndFree(byte* value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        return Marshal.PtrToStringUTF8((IntPtr)value) ?? string.Empty;
    }

    // Byte buffers returned by the native library are Go-managed; copy the data and do NOT free.
    public static byte[] CopyBytesAndFree(byte* source, nuint length)
    {
        if (source == null || length == 0)
        {
            return [];
        }

        return new ReadOnlySpan<byte>(source, checked((int)length)).ToArray();
    }

    // Same as CopyBytesAndFree but decodes UTF-8.
    public static string CopyUtf8BytesAndFree(byte* source, nuint length)
    {
        if (source == null || length == 0)
        {
            return string.Empty;
        }

        var data = new ReadOnlySpan<byte>(source, checked((int)length));
        return Encoding.UTF8.GetString(data);
    }
}
