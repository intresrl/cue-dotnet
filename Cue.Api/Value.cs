namespace Cuelang.Cue;

public sealed unsafe class Value : IDisposable
{
    private static readonly Dictionary<int, Kind> KindMap = new()
    {
        [NativeMethods.CUE_KIND_BOTTOM] = Cue.Kind.Bottom,
        [NativeMethods.CUE_KIND_NULL] = Cue.Kind.Null,
        [NativeMethods.CUE_KIND_BOOL] = Cue.Kind.Bool,
        [NativeMethods.CUE_KIND_INT] = Cue.Kind.Int,
        [NativeMethods.CUE_KIND_FLOAT] = Cue.Kind.Float,
        [NativeMethods.CUE_KIND_STRING] = Cue.Kind.String,
        [NativeMethods.CUE_KIND_BYTES] = Cue.Kind.Bytes,
        [NativeMethods.CUE_KIND_STRUCT] = Cue.Kind.Struct,
        [NativeMethods.CUE_KIND_LIST] = Cue.Kind.List,
        [NativeMethods.CUE_KIND_NUMBER] = Cue.Kind.Number,
        [NativeMethods.CUE_KIND_TOP] = Cue.Kind.Top,
        [NativeMethods.CUE_KIND_UNKNOWN] = Cue.Kind.Unknown
    };

    private readonly CueResource _resource;

    public Value(CueContext context, long value)
    {
        Context = context;
        _resource = new CueResource(NativeMethods.cue_from_int64(context.Handle, value));
    }

    public Value(CueContext context, bool value)
    {
        Context = context;
        _resource = new CueResource(NativeMethods.cue_from_bool(context.Handle, value));
    }

    public Value(CueContext context, double value)
    {
        Context = context;
        _resource = new CueResource(NativeMethods.cue_from_double(context.Handle, value));
    }

    public Value(CueContext context, string value)
    {
        Context = context;
        var utf8 = NativeMarshalling.AllocUtf8(value);
        try
        {
            _resource = new CueResource(NativeMethods.cue_from_string(context.Handle, utf8));
        }
        finally
        {
            NativeMarshalling.FreeUtf8(utf8);
        }
    }

    public Value(CueContext context, byte[] value)
    {
        Context = context;
        fixed (byte* ptr = value)
        {
            _resource = new CueResource(NativeMethods.cue_from_bytes(context.Handle, ptr, (nuint)value.Length));
        }
    }

    internal Value(CueContext context, nuint handle)
    {
        Context = context;
        _resource = new CueResource(handle);
    }

    internal nuint Handle => _resource.Handle;

    public CueContext Context { get; }

    public void Dispose()
    {
        _resource.Dispose();
    }

    public Result<Value, string> Error()
    {
        var error = NativeMethods.cue_value_error(Handle);
        if (error != 0)
        {
            var msg = NativeMarshalling.PtrToUtf8AndFree(NativeMethods.cue_error_string(error));
            return new Result<Value, string>.Err(msg);
        }

        return new Result<Value, string>.Ok(this);
    }

    public Value Unify(Value value)
    {
        return new Value(Context, NativeMethods.cue_unify(Handle, value.Handle));
    }

    public bool Equals(Value value)
    {
        return NativeMethods.cue_is_equal(Handle, value.Handle);
    }

    public bool IsConcrete()
    {
        return NativeMethods.cue_is_concrete(Handle);
    }

    public Kind Kind()
    {
        return KindMap[NativeMethods.cue_concrete_kind(Handle)];
    }

    public Kind IncompleteKind()
    {
        return KindMap[NativeMethods.cue_incomplete_kind(Handle)];
    }

    public void Validate(params EvalOption[] options)
    {
        var evalOpts = OptionEncoder.EncodeEvalOptions(options);
        try
        {
            var err = NativeMethods.cue_validate_raw(Handle, evalOpts.Options, evalOpts.Count);
            Context.ThrowIfError(err);
        }
        finally
        {
            evalOpts.Dispose();
        }
    }

    public void CheckSchema(Value value, params EvalOption[] options)
    {
        var evalOpts = OptionEncoder.EncodeEvalOptions(options);
        try
        {
            var err = NativeMethods.cue_instance_of_raw(Handle, value.Handle, evalOpts.Options, evalOpts.Count);
            Context.ThrowIfError(err);
        }
        finally
        {
            evalOpts.Dispose();
        }
    }

    public Value Lookup(string path)
    {
        var utf8 = NativeMarshalling.AllocUtf8(path);
        try
        {
            nuint result = 0;
            var err = NativeMethods.cue_lookup_string(Handle, utf8, &result);
            Context.ThrowIfError(err);
            return new Value(Context, result);
        }
        finally
        {
            NativeMarshalling.FreeUtf8(utf8);
        }
    }

    /// <summary>
    ///     Looks up the element constraint value defined by the any-index (<c>[int]</c>) pattern selector.
    ///     Useful for retrieving the element type of list constraint.
    /// </summary>
    public Value LookupAnyIndex()
    {
        nuint result = 0;
        var err = NativeMethods.cue_lookup_any_index(Handle, &result);
        Context.ThrowIfError(err);
        return new Value(Context, result);
    }

    /// <summary>
    ///     Looks up the element constraint value defined by the any-string (<c>[string]</c>) pattern selector.
    ///     Useful for retrieving the value type of map/struct constraint keyed by arbitrary strings.
    /// </summary>
    public Value LookupAnyString()
    {
        nuint result = 0;
        var err = NativeMethods.cue_lookup_any_string(Handle, &result);
        Context.ThrowIfError(err);
        return new Value(Context, result);
    }

    public Value? DefaultValue()
    {
        byte hasDefault = 0;
        var value = NativeMethods.cue_default(Handle, &hasDefault);
        return hasDefault == 1 ? new Value(Context, value) : null;
    }

    public long GetLong()
    {
        long result = 0;
        var err = NativeMethods.cue_dec_int64(Handle, &result);
        Context.ThrowIfError(err);
        return result;
    }

    public ulong GetLongAsUnsigned()
    {
        ulong result = 0;
        var err = NativeMethods.cue_dec_uint64(Handle, &result);
        Context.ThrowIfError(err);
        return result;
    }

    public bool GetBoolean()
    {
        byte result = 0;
        var err = NativeMethods.cue_dec_bool(Handle, &result);
        Context.ThrowIfError(err);
        return result == 1;
    }

    public double GetDouble()
    {
        double result = 0;
        var err = NativeMethods.cue_dec_double(Handle, &result);
        Context.ThrowIfError(err);
        return result;
    }

    public string GetString()
    {
        byte* result = null;
        var err = NativeMethods.cue_dec_string(Handle, &result);
        Context.ThrowIfError(err);
        return NativeMarshalling.PtrToUtf8AndFree(result);
    }

    public byte[] GetBytes()
    {
        byte* result = null;
        nuint len = 0;
        var err = NativeMethods.cue_dec_bytes(Handle, &result, &len);
        Context.ThrowIfError(err);
        return NativeMarshalling.CopyBytesAndFree(result, len);
    }

    public string GetJson()
    {
        byte* result = null;
        nuint len = 0;
        var err = NativeMethods.cue_dec_json(Handle, &result, &len);
        Context.ThrowIfError(err);
        return NativeMarshalling.CopyUtf8BytesAndFree(result, len);
    }

    /// <summary>
    ///     May be called only if Kind is Struct
    /// </summary>
    /// <returns>The list of properties on this object as Value objects</returns>
    public Value[] Fields(bool definitions = false)
    {
        nuint len = 0;
        var fields = NativeMethods.cue_fields(Handle, definitions, &len);
        return FromNativeValueArray(fields, len);
    }

    /// <summary>
    ///     May be called only if Kind is List
    /// </summary>
    /// <returns>The elements of the list as Value objects</returns>
    public Value[] List()
    {
        nuint len = 0;
        var elements = NativeMethods.cue_list(Handle, &len);
        return FromNativeValueArray(elements, len);
    }

    /// <summary>
    ///     Returns all disjunctions for this value, or an empty array if the value contains no disjunctions.
    ///     For a value with a disjunction like `int | string | bool`, Disjunctions will
    ///     return an array containing three separate Value objects, one for each option.
    ///     For a non-disjunction value, it returns an empty array.
    /// </summary>
    /// <returns>An array of Value objects representing each disjunction option</returns>
    public Value[] Disjunctions()
    {
        nuint len = 0;
        var disjuncts = NativeMethods.cue_disjunctions(Handle, &len);
        return FromNativeValueArray(disjuncts, len);
    }

    /// <summary>
    ///     String representation of the path of this property in the payload
    /// </summary>
    public string Path()
    {
        return NativeMarshalling.PtrToUtf8AndFree(NativeMethods.cue_path(Handle));
    }

    public Attribute[] Attributes(AttributeKind kind = AttributeKind.Value)
    {
        nuint len = 0;
        var attrs = NativeMethods.cue_attrs(Handle, (int)kind, &len);

        var count = checked((int)len);
        if (count == 0) return [];

        var values = new Attribute[count];
        for (var i = 0; i < count; i++) values[i] = new Attribute(new CueResource(attrs[i]));

        return values;
    }

    private Value[] FromNativeValueArray(nuint* handles, nuint length)
    {
        if (handles == null || length == 0) return [];

        var count = checked((int)length);
        var values = new Value[count];
        for (var i = 0; i < count; i++) values[i] = new Value(Context, handles[i]);

        return values;
    }
}