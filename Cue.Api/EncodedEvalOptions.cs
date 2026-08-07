using System.Runtime.InteropServices;

using static Cuelang.Cue.NativeMethods;

namespace Cuelang.Cue;

internal readonly unsafe struct EncodedEvalOptions : IDisposable
{
    private EncodedEvalOptions(cue_eopt* options)
    {
        Options = options;
    }
    
    public static EncodedEvalOptions From(ReadOnlySpan<EvalOption> options)
    {
        var ptr = (cue_eopt*)NativeMemory.Alloc(
            (nuint)(options.Length + 1),
            (nuint)sizeof(cue_eopt)
        );

        for (var i = 0; i < options.Length; i++)
        {
            var (tag, value) = options[i] switch
            {
                EvalOption.All => (CUE_OPT_ALL, false),
                EvalOption.Final => (CUE_OPT_FINAL, false),
                EvalOption.Raw => (CUE_OPT_RAW, false),
                EvalOption.Schema => (CUE_OPT_SCHEMA, false),
                
                EvalOption.Attributes { Value: var v } => (CUE_OPT_ATTR, v),
                EvalOption.Concrete { Value: var v } => (CUE_OPT_CONCRETE, v),
                EvalOption.Definitions { Value: var v } => (CUE_OPT_DEFS, v),
                EvalOption.DisallowCycles { Value: var v } => (CUE_OPT_DISALLOW_CYCLES, v),
                EvalOption.Docs { Value: var v } => (CUE_OPT_DOCS, v),
                EvalOption.ErrorsAsValues { Value: var v } => (CUE_OPT_ERRORS_AS_VALUES, v),
                EvalOption.Hidden { Value: var v } => (CUE_OPT_HIDDEN, v),
                EvalOption.InlineImports { Value: var v } => (CUE_OPT_INLINE_IMPORTS, v),
                EvalOption.Optionals { Value: var v } => (CUE_OPT_OPTIONALS, v),
                
                { } o => throw new NotSupportedException($"Unknown eval option type: {o.GetType().Name}")
            };

            ref var option = ref ptr[i];
            option.tag = tag;
            option.value = value ? (byte)1 : (byte)0;
        }

        // A "none" option which acts as a list terminator
        ref var terminator = ref ptr[options.Length];
        terminator.tag = CUE_OPT_NONE;
        terminator.value = 0;

        return new EncodedEvalOptions(ptr);
    }

    public cue_eopt* Options { get; }
    
    public void Dispose()
    {
        NativeMemory.Free(Options);
    }
}
