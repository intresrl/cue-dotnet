using System.Runtime.InteropServices;
// ReSharper disable InconsistentNaming

namespace Cuelang.Cue;

internal static unsafe class NativeMethods
{
    internal const int CUE_ATTR_FIELD = 1;
    internal const int CUE_ATTR_DECL = 2;
    internal const int CUE_ATTR_VALUE = 3;

    internal const int CUE_KIND_BOTTOM = 0;
    internal const int CUE_KIND_NULL = 1;
    internal const int CUE_KIND_BOOL = 2;
    internal const int CUE_KIND_INT = 3;
    internal const int CUE_KIND_FLOAT = 4;
    internal const int CUE_KIND_STRING = 5;
    internal const int CUE_KIND_BYTES = 6;
    internal const int CUE_KIND_STRUCT = 7;
    internal const int CUE_KIND_LIST = 8;
    internal const int CUE_KIND_NUMBER = 9;
    internal const int CUE_KIND_TOP = 10;

    internal const int CUE_OP_NO = 0;
    internal const int CUE_OP_AND = 1;
    internal const int CUE_OP_OR = 2;
    internal const int CUE_OP_SELECTOR = 3;
    internal const int CUE_OP_INDEX = 4;
    internal const int CUE_OP_SLICE = 5;
    internal const int CUE_OP_CALL = 6;
    internal const int CUE_OP_BOOLEAN_AND = 7;
    internal const int CUE_OP_BOOLEAN_OR = 8;
    internal const int CUE_OP_EQUAL = 9;
    internal const int CUE_OP_NOT = 10;
    internal const int CUE_OP_NOT_EQUAL = 11;
    internal const int CUE_OP_LESS_THAN = 12;
    internal const int CUE_OP_LESS_THAN_EQUAL = 13;
    internal const int CUE_OP_GREATER_THAN = 14;
    internal const int CUE_OP_GREATER_THAN_EQUAL = 15;
    internal const int CUE_OP_REGEX_MATCH = 16;
    internal const int CUE_OP_NOT_REGEX_MATCH = 17;
    internal const int CUE_OP_ADD = 18;
    internal const int CUE_OP_SUBTRACT = 19;
    internal const int CUE_OP_MULTIPLY = 20;
    internal const int CUE_OP_FLOAT_QUOTIENT = 21;
    internal const int CUE_OP_INTERPOLATION = 22;
    internal const int CUE_OP_SPREAD = 23;

    internal const int CUE_OPT_NONE = 0;
    internal const int CUE_OPT_ALL = 1;
    internal const int CUE_OPT_ATTR = 2;
    internal const int CUE_OPT_CONCRETE = 3;
    internal const int CUE_OPT_DEFS = 4;
    internal const int CUE_OPT_DISALLOW_CYCLES = 5;
    internal const int CUE_OPT_DOCS = 6;
    internal const int CUE_OPT_ERRORS_AS_VALUES = 7;
    internal const int CUE_OPT_FINAL = 8;
    internal const int CUE_OPT_HIDDEN = 9;
    internal const int CUE_OPT_INLINE_IMPORTS = 10;
    internal const int CUE_OPT_OPTIONALS = 11;
    internal const int CUE_OPT_RAW = 12;
    internal const int CUE_OPT_SCHEMA = 13;

    internal const int CUE_BUILD_NONE = 0;
    internal const int CUE_BUILD_FILENAME = 1;
    internal const int CUE_BUILD_IMPORT_PATH = 2;
    internal const int CUE_BUILD_INFER_BUILTINS = 3;
    internal const int CUE_BUILD_SCOPE = 4;

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_newctx();

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void cue_free(nuint handle);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern byte* cue_error_string(nuint error);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_compile_string(nuint context, byte* source, cue_bopt* options, nuint* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_compile_bytes(nuint context, byte* source, nuint size, cue_bopt* options, nuint* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_top(nuint context);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_bottom(nuint context);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_unify(nuint left, nuint right);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_instance_of(nuint value, nuint schema, cue_eopt* options);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_lookup_string(nuint value, byte* path, nuint* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_lookup_any_index(nuint value, nuint* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_lookup_any_string(nuint value, nuint* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_int64(nuint context, long value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_uint64(nuint context, ulong value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_bool(nuint context, [MarshalAs(UnmanagedType.I1)] bool value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_double(nuint context, double value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_string(nuint context, byte* value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_bytes(nuint context, byte* value, nuint size);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_from_list(nuint context, nuint* values, nuint count);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_int64(nuint value, long* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_uint64(nuint value, ulong* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_bool(nuint value, byte* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_double(nuint value, double* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_string(nuint value, byte** result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_bytes(nuint value, byte** result, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_dec_json(nuint value, byte** result, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_validate(nuint value, cue_eopt* options);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_default(nuint value, byte* hasDefault);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int cue_concrete_kind(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern int cue_incomplete_kind(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_value_error(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool cue_is_equal(nuint left, nuint right);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static extern bool cue_is_concrete(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint* cue_attrs(nuint value, int kind, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint* cue_fields(nuint value, cue_eopt* options, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint* cue_list(nuint value, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint* cue_disjunctions(nuint value, nuint* length);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern byte* cue_path(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_attr_numargs(nuint attribute);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern byte* cue_attr_name(nuint attribute);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern byte* cue_attr_value(nuint attribute);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern void cue_attr_getarg(nuint attribute, nuint index, cue_attr_arg* result);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern cue_expr_result cue_expr(nuint value);

    [DllImport("cue", CallingConvention = CallingConvention.Cdecl)]
    internal static extern nuint cue_len(nuint value);
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct cue_bopt
{
    public int tag;
    private int _padding;
    public nuint value;
    public byte* str;
    public byte b;
    private fixed byte _reserved[7];
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct cue_eopt
{
    public int tag;
    public byte value;
    private fixed byte _reserved[3];
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct cue_attr_arg
{
    public byte* key;
    public byte* val;
}

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct cue_expr_result
{
    public int op;
    public byte* call_name;
    public nuint* values;
    public nuint count;
}