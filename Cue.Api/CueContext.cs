namespace Cuelang.Cue;

public sealed unsafe class CueContext : IDisposable
{
    // TODO: CueContext should remember cue values and dispose them when it is disposed
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

        using var source = NativeUtf8String.From(value);
        using var encoded = EncodedBuildOptions.From(options);
        
        nuint result = 0;
        var err = NativeMethods.cue_compile_string(Handle, source.Str, encoded.Options, &result);
        ThrowIfError(err);
        return new Value(this, result);
    }

    public Value Compile(byte[] value, params BuildOption[] options)
    {
        ThrowIfDisposed();

        using var encoded = EncodedBuildOptions.From(options);

        fixed (byte* source = value)
        {
            nuint result = 0;
            var err = NativeMethods.cue_compile_bytes(Handle, source, (nuint)value.Length, encoded.Options, &result);
            ThrowIfError(err);
            return new Value(this, result);
        }
    }

    public Value ToValue(long value) => new(this, value);

    public Value ToValueAsUnsigned(ulong value)
    {
        ThrowIfDisposed();
        return new Value(this, NativeMethods.cue_from_uint64(Handle, value));
    }

    public Value ToValue(bool value) => new(this, value);

    public Value ToValue(double value) => new(this, value);

    public Value ToValue(string value) => new(this, value);

    public Value ToValue(byte[] value) => new(this, value);

    public Value ToValue(params Value[] values)
    {
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(values);

        if (values.Length == 0)
        {
            return new Value(this, NativeMethods.cue_from_list(Handle, null, 0));
        }

        var handles = new nuint[values.Length];
        for (var i = 0; i < values.Length; i++)
        {
            var value = values[i] ?? throw new ArgumentException("Values cannot contain null entries.", nameof(values));
            if (!ReferenceEquals(value.Context, this))
            {
                throw new ArgumentException("All values must belong to this context.", nameof(values));
            }

            handles[i] = value.Handle;
        }

        fixed (nuint* ptr = handles)
        {
            return new Value(this, NativeMethods.cue_from_list(Handle, ptr, (nuint)handles.Length));
        }
    }

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

