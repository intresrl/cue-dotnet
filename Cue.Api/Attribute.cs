namespace Cuelang.Cue;

public sealed unsafe class Attribute
{
    private readonly CueResource _resource;

    internal Attribute(CueResource resource)
    {
        _resource = resource;
    }

    public int ArgCount()
    {
        return checked((int)NativeMethods.cue_attr_numargs(_resource.Handle));
    }

    public string Name()
    {
        return NativeDynamicAllocation.ToString(NativeMethods.cue_attr_name(_resource.Handle));
    }

    public string Value()
    {
        return NativeDynamicAllocation.ToString(NativeMethods.cue_attr_value(_resource.Handle));
    }

    public Arg GetArg(int index)
    {
        if (index >= ArgCount())
        {
            throw new IndexOutOfRangeException();
        }

        cue_attr_arg nativeArg = default;
        NativeMethods.cue_attr_getarg(_resource.Handle, (nuint)index, &nativeArg);

        var key = NativeDynamicAllocation.ToString(nativeArg.key);
        var val = NativeDynamicAllocation.ToString(nativeArg.val);

        if (string.IsNullOrEmpty(val))
        {
            return new Arg.Value(key);
        }

        return new Arg.KeyValue(key, val);
    }

    public Arg[] Args()
    {
        var count = ArgCount();
        var args = new Arg[count];

        for (var i = 0; i < count; i++)
        {
            args[i] = GetArg(i);
        }

        return args;
    }

    public abstract record Arg
    {
        public sealed record Value(string Val) : Arg;

        public sealed record KeyValue(string Key, string Val) : Arg;
    }
}

