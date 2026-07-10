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

    public static string PtrToUtf8AndFree(byte* value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        try
        {
            return Marshal.PtrToStringUTF8((IntPtr)value) ?? string.Empty;
        }
        finally
        {
            NativeMethods.libc_free(value);
        }
    }

    public static byte[] CopyBytesAndFree(byte* source, nuint length)
    {
        if (source == null || length == 0)
        {
            if (source != null)
            {
                NativeMethods.libc_free(source);
            }

            return [];
        }

        try
        {
            return new ReadOnlySpan<byte>(source, checked((int)length)).ToArray();
        }
        finally
        {
            NativeMethods.libc_free(source);
        }
    }

    public static string CopyUtf8BytesAndFree(byte* source, nuint length)
    {
        if (source == null || length == 0)
        {
            if (source != null)
            {
                NativeMethods.libc_free(source);
            }

            return string.Empty;
        }

        try
        {
            var data = new ReadOnlySpan<byte>(source, checked((int)length));
            return Encoding.UTF8.GetString(data);
        }
        finally
        {
            NativeMethods.libc_free(source);
        }
    }
}

