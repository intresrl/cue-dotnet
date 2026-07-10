using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal sealed class CueResource : SafeHandle
{
    public CueResource(nuint handleValue)
        : base(IntPtr.Zero, ownsHandle: true)
    {
        SetHandle((IntPtr)handleValue);
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    public nuint Handle => (nuint)handle;

    protected override bool ReleaseHandle()
    {
        if (!IsInvalid)
        {
            NativeMethods.cue_free((nuint)handle);
        }

        return true;
    }
}

