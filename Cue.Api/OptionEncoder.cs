using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal static unsafe class OptionEncoder
{
    public static EncodedBuildOptions EncodeBuildOptions(ReadOnlySpan<BuildOption> options)
    {
        if (options.IsEmpty)
        {
            return new EncodedBuildOptions(null, []);
        }

        var ptr = (cue_bopt*)NativeMemory.AllocZeroed((nuint)(options.Length + 1), (nuint)sizeof(cue_bopt));
        var allocatedStrings = new List<nint>();

        for (var i = 0; i < options.Length; i++)
        {
            ref var option = ref ptr[i];

            switch (options[i])
            {
                case BuildOption.FileName fileName:
                    option.tag = NativeMethods.CUE_BUILD_FILENAME;
                    option.str = NativeMarshalling.AllocUtf8(fileName.Name);
                    allocatedStrings.Add((nint)option.str);
                    break;
                case BuildOption.ImportPath importPath:
                    option.tag = NativeMethods.CUE_BUILD_IMPORT_PATH;
                    option.str = NativeMarshalling.AllocUtf8(importPath.Path);
                    allocatedStrings.Add((nint)option.str);
                    break;
                case BuildOption.InferBuiltins inferBuiltins:
                    option.tag = NativeMethods.CUE_BUILD_INFER_BUILTINS;
                    option.b = inferBuiltins.Value ? (byte)1 : (byte)0;
                    break;
                case BuildOption.Scope scope:
                    option.tag = NativeMethods.CUE_BUILD_SCOPE;
                    option.value = scope.Value.Handle;
                    break;
                default:
                    throw new NotSupportedException($"Unknown build option type: {options[i].GetType().Name}");
            }
        }

        ptr[options.Length].tag = NativeMethods.CUE_BUILD_NONE;
        return new EncodedBuildOptions(ptr, allocatedStrings);
    }

    public static cue_eopt* EncodeEvalOptions(ReadOnlySpan<EvalOption> options)
    {
        if (options.IsEmpty)
        {
            return null;
        }

        var ptr = (cue_eopt*)NativeMemory.AllocZeroed((nuint)(options.Length + 1), (nuint)sizeof(cue_eopt));

        for (var i = 0; i < options.Length; i++)
        {
            ref var option = ref ptr[i];

            switch (options[i])
            {
                case EvalOption.All:
                    option.tag = NativeMethods.CUE_OPT_ALL;
                    break;
                case EvalOption.Attributes attributes:
                    option.tag = NativeMethods.CUE_OPT_ATTR;
                    option.value = attributes.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Concrete concrete:
                    option.tag = NativeMethods.CUE_OPT_CONCRETE;
                    option.value = concrete.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Definitions definitions:
                    option.tag = NativeMethods.CUE_OPT_DEFS;
                    option.value = definitions.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.DisallowCycles disallowCycles:
                    option.tag = NativeMethods.CUE_OPT_DISALLOW_CYCLES;
                    option.value = disallowCycles.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Docs docs:
                    option.tag = NativeMethods.CUE_OPT_DOCS;
                    option.value = docs.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.ErrorsAsValues errorsAsValues:
                    option.tag = NativeMethods.CUE_OPT_ERRORS_AS_VALUES;
                    option.value = errorsAsValues.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Final:
                    option.tag = NativeMethods.CUE_OPT_FINAL;
                    break;
                case EvalOption.Hidden hidden:
                    option.tag = NativeMethods.CUE_OPT_HIDDEN;
                    option.value = hidden.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.InlineImports inlineImports:
                    option.tag = NativeMethods.CUE_OPT_INLINE_IMPORTS;
                    option.value = inlineImports.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Optionals optionals:
                    option.tag = NativeMethods.CUE_OPT_OPTIONALS;
                    option.value = optionals.Value ? (byte)1 : (byte)0;
                    break;
                case EvalOption.Raw:
                    option.tag = NativeMethods.CUE_OPT_RAW;
                    break;
                case EvalOption.Schema:
                    option.tag = NativeMethods.CUE_OPT_SCHEMA;
                    break;
                default:
                    throw new NotSupportedException($"Unknown eval option type: {options[i].GetType().Name}");
            }
        }

        ptr[options.Length].tag = NativeMethods.CUE_OPT_NONE;
        return ptr;
    }

    public static void FreeEvalOptions(cue_eopt* options)
    {
        if (options != null)
        {
            NativeMemory.Free(options);
        }
    }
}

internal readonly unsafe struct EncodedBuildOptions
{
    public EncodedBuildOptions(cue_bopt* options, IReadOnlyList<nint> ownedStrings)
    {
        Options = options;
        OwnedStrings = ownedStrings;
    }

    public cue_bopt* Options { get; }

    public IReadOnlyList<nint> OwnedStrings { get; }

    public void Dispose()
    {
        foreach (var str in OwnedStrings)
        {
            NativeMarshalling.FreeUtf8((byte*)str);
        }

        if (Options != null)
        {
            NativeMemory.Free(Options);
        }
    }
}

