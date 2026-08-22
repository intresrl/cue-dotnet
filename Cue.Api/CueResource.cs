using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal sealed class CueResource : SafeHandle, IEquatable<CueResource>
{
    private readonly nuint _handleValue;
    
    public CueResource(nuint handleValue)
        : base(IntPtr.Zero, ownsHandle: true)
    {
        SetHandle((IntPtr)handleValue);
        _handleValue = handleValue;
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

    public bool Equals(CueResource? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return _handleValue == other._handleValue;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is CueResource other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _handleValue.GetHashCode();
    }
}

