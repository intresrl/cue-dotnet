using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal static unsafe partial class NativeMethods
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

    [LibraryImport("cue")]
    internal static partial nuint cue_newctx();

    [LibraryImport("cue")]
    internal static partial void cue_free(nuint handle);

    [LibraryImport("cue")]
    internal static partial byte* cue_error_string(nuint error);

    [LibraryImport("cue")]
    internal static partial nuint cue_compile_string_raw(nuint context, byte* source, cue_bopt* options, nuint count, nuint* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_compile_bytes_raw(nuint context, byte* source, nuint size, cue_bopt* options, nuint count, nuint* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_top(nuint context);

    [LibraryImport("cue")]
    internal static partial nuint cue_bottom(nuint context);

    [LibraryImport("cue")]
    internal static partial nuint cue_unify(nuint left, nuint right);

    [LibraryImport("cue")]
    internal static partial nuint cue_instance_of_raw(nuint value, nuint schema, cue_eopt* options, nuint count);

    [LibraryImport("cue")]
    internal static partial nuint cue_lookup_string(nuint value, byte* path, nuint* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_lookup_any_index(nuint value, nuint* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_lookup_any_string(nuint value, nuint* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_int64(nuint context, long value);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_uint64(nuint context, ulong value);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_bool(nuint context, [MarshalAs(UnmanagedType.I1)] bool value);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_double(nuint context, double value);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_string(nuint context, byte* value);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_bytes(nuint context, byte* value, nuint size);

    [LibraryImport("cue")]
    internal static partial nuint cue_from_list(nuint context, nuint* values, nuint count);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_int64(nuint value, long* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_uint64(nuint value, ulong* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_bool(nuint value, byte* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_double(nuint value, double* result);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_string(nuint value, byte** result);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_bytes(nuint value, byte** result, nuint* length);

    [LibraryImport("cue")]
    internal static partial nuint cue_dec_json(nuint value, byte** result, nuint* length);

    [LibraryImport("cue")]
    internal static partial nuint cue_validate_raw(nuint value, cue_eopt* options, nuint count);

    [LibraryImport("cue")]
    internal static partial nuint cue_default(nuint value, byte* hasDefault);

    [LibraryImport("cue")]
    internal static partial int cue_concrete_kind(nuint value);

    [LibraryImport("cue")]
    internal static partial int cue_incomplete_kind(nuint value);

    [LibraryImport("cue")]
    internal static partial nuint cue_value_error(nuint value);

    [LibraryImport("cue")]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool cue_is_equal(nuint left, nuint right);

    [LibraryImport("cue")]
    internal static partial nuint* cue_attrs(nuint value, int kind, nuint* length);

    [LibraryImport("cue")]
    internal static partial nuint* cue_fields(nuint value, [MarshalAs(UnmanagedType.I1)] bool definitions, nuint* length);

    [LibraryImport("cue")]
    internal static partial nuint* cue_list(nuint value, nuint* length);

    [LibraryImport("cue")]
    internal static partial nuint* cue_disjunctions(nuint value, nuint* length);

    [LibraryImport("cue")]
    internal static partial byte* cue_path(nuint value);

    [LibraryImport("cue")]
    internal static partial nuint cue_attr_numargs(nuint attribute);

    [LibraryImport("cue")]
    internal static partial byte* cue_attr_name(nuint attribute);

    [LibraryImport("cue")]
    internal static partial byte* cue_attr_value(nuint attribute);

    [LibraryImport("cue")]
    internal static partial void cue_attr_getarg(nuint attribute, nuint index, cue_attr_arg* result);
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
