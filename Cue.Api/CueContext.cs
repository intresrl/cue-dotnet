namespace Cuelang.Cue;

public sealed unsafe class CueContext : IDisposable
{
    private readonly CueResource _resource = new(NativeMethods.cue_newctx());
    private bool _disposed;

    internal nuint Handle => _resource.Handle;

    public Value Top()
    {
        ThrowIfDisposed();
        return new Value(this, NativeMethods.cue_top(Handle));
    }

    public Value Bottom()
    {
        ThrowIfDisposed();
        return new Value(this, NativeMethods.cue_bottom(Handle));
    }

    public Value Compile(string value, params BuildOption[] options)
    {
        ThrowIfDisposed();

        var source = NativeMarshalling.AllocUtf8(value);
        var encoded = OptionEncoder.EncodeBuildOptions(options);

        try
        {
            nuint result = 0;
            var err = NativeMethods.cue_compile_string_raw(Handle, source, encoded.Options, encoded.Count, &result);
            ThrowIfError(err);
            return new Value(this, result);
        }
        finally
        {
            NativeMarshalling.FreeUtf8(source);
            encoded.Dispose();
        }
    }

    public Value Compile(byte[] value, params BuildOption[] options)
    {
        ThrowIfDisposed();

        var encoded = OptionEncoder.EncodeBuildOptions(options);
        try
        {
            fixed (byte* source = value)
            {
                nuint result = 0;
                var err = NativeMethods.cue_compile_bytes_raw(Handle, source, (nuint)value.Length, encoded.Options, encoded.Count, &result);
                ThrowIfError(err);
                return new Value(this, result);
            }
        }
        finally
        {
            encoded.Dispose();
        }
    }

    public Value ToValue(long value) => new(this, value);

    public Value ToValueAsUnsigned(ulong value)
    {
        ThrowIfDisposed();
        return new Value(this, NativeMethods.cue_from_uint64(Handle, value));
    }

    public Value ToValueAsUnsigned(long value)
    {
        return ToValueAsUnsigned(unchecked((ulong)value));
    }

    public Value ToValue(bool value) => new(this, value);

    public Value ToValue(double value) => new(this, value);

    public Value ToValue(string value) => new(this, value);

    public Value ToValue(byte[] value) => new(this, value);

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _resource.Dispose();
        _disposed = true;
    }

    internal void ThrowIfError(nuint error)
    {
        if (error != 0)
        {
            throw new CueError(this, error);
        }
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}

