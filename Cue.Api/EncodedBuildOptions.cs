using System.Runtime.InteropServices;

namespace Cuelang.Cue;

internal readonly unsafe struct EncodedBuildOptions : IDisposable
{
    private readonly IEnumerable<NativeUtf8String> _ownedStrings;
    
    private EncodedBuildOptions(cue_bopt* options, IEnumerable<NativeUtf8String> ownedStrings)
    {
        Options = options;
        _ownedStrings = ownedStrings;
    }
    
    public static EncodedBuildOptions From(ReadOnlySpan<BuildOption> options)
    {
        var ptr = (cue_bopt*)NativeMemory.AllocZeroed((nuint)(options.Length + 1), (nuint)sizeof(cue_bopt));
        var allocatedStrings = new List<NativeUtf8String>();

        for (var i = 0; i < options.Length; i++)
        {
            ref var option = ref ptr[i];

            switch (options[i])
            {
                case BuildOption.FileName fileName:
                    option.tag = NativeMethods.CUE_BUILD_FILENAME;
                    var str = NativeUtf8String.From(fileName.Name);
                    allocatedStrings.Add(str);
                    option.str = str.Str;
                    break;
                case BuildOption.ImportPath importPath:
                    option.tag = NativeMethods.CUE_BUILD_IMPORT_PATH;
                    var str2 = NativeUtf8String.From(importPath.Path);
                    allocatedStrings.Add(str2);
                    option.str = str2.Str;
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
        
        // A "none" option which acts as a list terminator
        ref var terminator = ref ptr[options.Length];
        terminator.tag = NativeMethods.CUE_BUILD_NONE;

        return new EncodedBuildOptions(ptr, allocatedStrings);
    }

    public cue_bopt* Options { get; }

    public void Dispose()
    {
        foreach (var str in _ownedStrings)
        {
            str.Dispose();
        }

        if (Options != null)
        {
            NativeMemory.Free(Options);
        }
    }
}