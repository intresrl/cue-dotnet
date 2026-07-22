using System.Runtime.InteropServices;

namespace Cuelang.Cue;

public sealed class CueError : Exception
{
    private readonly CueResource _resource;

    internal CueError(CueContext context, nuint handle)
        : base(GetErrorString(handle))
    {
        _resource = new CueResource(handle);
        Context = context;
    }

    internal CueError(CueResource resource)
        : base(GetErrorString(resource.Handle))
    {
        _resource = resource;
    }

    public CueContext? Context { get; }

    internal nuint Handle => _resource.Handle;

    private static unsafe string GetErrorString(nuint handle)
    {
        var errorPtr = NativeMethods.cue_error_string(handle);
        // cue_error_string returns a Go-managed string; the caller must NOT free it.
        return Marshal.PtrToStringUTF8((IntPtr)errorPtr) ?? string.Empty;
    }
}

