using System.Runtime.InteropServices;
using System.Text;

namespace Cuelang.Cue;

internal static unsafe class NativeDynamicAllocation
{
    /// <summary>
    /// Given a native "malloc"ed UTF8 string, returns a C# string with its contents and frees "malloc"ed string,
    /// </summary>
    /// <param name="value">A null-terminated dynamically allocated string</param>
    /// <returns>A C# string</returns>
    public static string? ToString(byte* value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        var str = Marshal.PtrToStringUTF8((IntPtr)value);

        if ((nint)value != IntPtr.Zero)
        {
            NativeMemory.Free(value);
        }

        return str;
    }

    /// <summary>
    /// Given a native "malloc"ed byte array, returns a C# byte array with its contents and frees "malloc"ed byte array
    /// </summary>
    /// <param name="source">pointer to start of byte array</param>
    /// <param name="length">length of byte array</param>
    /// <returns>A C# byte array</returns>
    public static byte[] ToByteArray(byte* source, nuint length)
    {
        if (source == null || length == 0)
        {
            return [];
        }

        var value = new ReadOnlySpan<byte>(source, checked((int)length)).ToArray();

        if ((nint)source != IntPtr.Zero)
        {
            NativeMemory.Free(source);
        }

        return value;
    }

    public static TResult[] ToArray<TNative, TResult>(
        TNative* source,
        nuint length,
        Func<TNative, TResult> mapper)
        where TNative : unmanaged
    {
        if (source == null || length == 0)
            return [];

        try
        {
            var count = checked((int)length);
            var values = new ReadOnlySpan<TNative>(source, count);
            var result = new TResult[count];

            for (var i = 0; i < count; i++)
            {
                result[i] = mapper(values[i]);
            }

            return result;
        }
        finally
        {
            NativeMemory.Free(source);
        }
    }
}
