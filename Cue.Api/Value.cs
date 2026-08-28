using System.Runtime.CompilerServices;
using System.Text;

namespace Cuelang.Cue;

public sealed class WeakSet<T> where T : class 
{
    private static readonly object Marker = new();
    private readonly ConditionalWeakTable<T, object> _set = new();

    public void Add(T value)
    {
        _set.TryAdd(value, Marker);
    }
    
    public void Union(WeakSet<T> value)
    {
        foreach (var (k, _) in value._set)
        {
            _set.Add(k, Marker);
        }
    }

    public bool Contains(T value)
    {
        return _set.TryGetValue(value, out _);
    }
}

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
    };

    private static readonly Dictionary<int, ExprOp> ExprOpMap = new()
    {
        [NativeMethods.CUE_OP_NO] = ExprOp.No,
        [NativeMethods.CUE_OP_AND] = ExprOp.And,
        [NativeMethods.CUE_OP_OR] = ExprOp.Or,
        [NativeMethods.CUE_OP_SELECTOR] = ExprOp.Selector,
        [NativeMethods.CUE_OP_INDEX] = ExprOp.Index,
        [NativeMethods.CUE_OP_SLICE] = ExprOp.Slice,
        [NativeMethods.CUE_OP_CALL] = ExprOp.Call,
        [NativeMethods.CUE_OP_BOOLEAN_AND] = ExprOp.BooleanAnd,
        [NativeMethods.CUE_OP_BOOLEAN_OR] = ExprOp.BooleanOr,
        [NativeMethods.CUE_OP_EQUAL] = ExprOp.Equal,
        [NativeMethods.CUE_OP_NOT] = ExprOp.Not,
        [NativeMethods.CUE_OP_NOT_EQUAL] = ExprOp.NotEqual,
        [NativeMethods.CUE_OP_LESS_THAN] = ExprOp.LessThan,
        [NativeMethods.CUE_OP_LESS_THAN_EQUAL] = ExprOp.LessThanEqual,
        [NativeMethods.CUE_OP_GREATER_THAN] = ExprOp.GreaterThan,
        [NativeMethods.CUE_OP_GREATER_THAN_EQUAL] = ExprOp.GreaterThanEqual,
        [NativeMethods.CUE_OP_REGEX_MATCH] = ExprOp.RegexMatch,
        [NativeMethods.CUE_OP_NOT_REGEX_MATCH] = ExprOp.NotRegexMatch,
        [NativeMethods.CUE_OP_ADD] = ExprOp.Add,
        [NativeMethods.CUE_OP_SUBTRACT] = ExprOp.Subtract,
        [NativeMethods.CUE_OP_MULTIPLY] = ExprOp.Multiply,
        [NativeMethods.CUE_OP_FLOAT_QUOTIENT] = ExprOp.FloatQuotient,
        [NativeMethods.CUE_OP_INTERPOLATION] = ExprOp.Interpolation,
        [NativeMethods.CUE_OP_SPREAD] = ExprOp.Spread,
    };
    
    private sealed class SchemaEqualityComparer : IEqualityComparer<Value>
    {
        private static readonly ConditionalWeakTable<Value, WeakSet<Value>> EqualTo = new();
        private readonly Lock _lock = new();
        
        public bool Equals(Value? x, Value? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            if (Equals(x._resource, y._resource)) return true;

            lock (_lock)
            {
                if (EqualTo.TryGetValue(x, out var v) && v.Contains(y))
                {
                    return true;
                }

                if (x.CheckSchema(y, new EvalOption.Schema()) is not null ||
                    y.CheckSchema(x, new EvalOption.Schema()) is not null) return false;

                var xEq = EqualTo.GetOrAdd(x, _ => new WeakSet<Value>());
                var yEq = EqualTo.GetOrAdd(y, _ => new WeakSet<Value>());

                xEq.Union(yEq);
                xEq.Add(x);
                xEq.Add(y);
                EqualTo.AddOrUpdate(y, xEq);

                return true;
            }
        }

        public int GetHashCode(Value obj)
        {
            return 0; // todo: finish
        }
    }

    public static readonly IEqualityComparer<Value> SchemaComparer = new SchemaEqualityComparer();
    
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
        using var utf8 = NativeUtf8String.From(value);
        _resource = new CueResource(NativeMethods.cue_from_string(context.Handle, utf8.Str));
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
        if (error == 0) return new Result<Value, string>.Ok(this);
        
        var msg = NativeDynamicAllocation.ToString(NativeMethods.cue_error_string(error));
        return new Result<Value, string>.Err(msg!);
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
        using var evalOpts = EncodedEvalOptions.From(options);
        var err = NativeMethods.cue_validate(Handle, evalOpts.Options);
        Context.ThrowIfError(err);
    }

    public CueError? CheckSchema(Value value, params EvalOption[] options)
    {
        using var evalOpts = EncodedEvalOptions.From(options);
        var err = NativeMethods.cue_instance_of(Handle, value.Handle, evalOpts.Options);
        return err != 0 ? new CueError(Context, err) : null;
    }

    public Value Lookup(string path)
    {
        using var utf8 = NativeUtf8String.From(path);
        nuint result = 0;
        var err = NativeMethods.cue_lookup_string(Handle, utf8.Str, &result);
        Context.ThrowIfError(err);
        return new Value(Context, result);
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

    public string? GetString()
    {
        byte* result = null;
        var err = NativeMethods.cue_dec_string(Handle, &result);
        Context.ThrowIfError(err);
        return NativeDynamicAllocation.ToString(result);
    }

    public byte[] GetBytes()
    {
        byte* result = null;
        nuint len = 0;
        var err = NativeMethods.cue_dec_bytes(Handle, &result, &len);
        Context.ThrowIfError(err);
        return NativeDynamicAllocation.ToByteArray(result, len);
    }

    public string GetJson()
    {
        byte* result = null;
        nuint len = 0;
        var err = NativeMethods.cue_dec_json(Handle, &result, &len);
        Context.ThrowIfError(err);
        return Encoding.UTF8.GetString(NativeDynamicAllocation.ToByteArray(result, len));
    }

    /// <summary>
    ///     May be called only if Kind is Struct
    /// </summary>
    /// <returns>The list of properties on this object as Value objects</returns>
    public Value[] Fields(params EvalOption[] options)
    {
        using var evalOpts = EncodedEvalOptions.From(options);
        nuint len = 0;
        var fields = NativeMethods.cue_fields(Handle, evalOpts.Options, &len);
        return NativeDynamicAllocation.ToArray(fields, len, handle => new Value(Context, handle));
    }

    /// <summary>
    ///     May be called only if Kind is List
    /// </summary>
    /// <returns>The elements of the list as Value objects</returns>
    public Value[] List()
    {
        nuint len = 0;
        var elements = NativeMethods.cue_list(Handle, &len);
        return NativeDynamicAllocation.ToArray(elements, len, handle => new Value(Context, handle));
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
        return NativeDynamicAllocation.ToArray(disjuncts, len, handle => new Value(Context, handle));
    }

    /// <summary>
    ///     String representation of the path of this property in the payload
    /// </summary>
    public string Path()
    {
        return NativeDynamicAllocation.ToString(NativeMethods.cue_path(Handle))
            ?? throw new InvalidDataException("libcue path should always be non-null");
    }

    public Attribute[] Attributes(AttributeKind kind = AttributeKind.Value)
    {
        nuint len = 0;
        var attrs = NativeMethods.cue_attrs(Handle, (int)kind, &len);

        return NativeDynamicAllocation.ToArray(attrs, len, attr => new Attribute(new CueResource(attr)));
    }

    public ExprResult Expr()
    {
        var result = NativeMethods.cue_expr(Handle);
        var name = NativeDynamicAllocation.ToString(result.call_name);
        var values = NativeDynamicAllocation.ToArray(result.values, result.count, handle => new Value(Context, handle));
        return new ExprResult(ExprOpMap[result.op], name, values);
    }
    
    public Value Len()
    {
        var handle = NativeMethods.cue_len(Handle);
        return new Value(Context, handle);
    }
}
