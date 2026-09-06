using System;
using System.Collections.Generic;
using System.Numerics;
using ExtendedNumerics;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Examples.cue_equality
{
    public sealed class CueList<TConcrete, TAnyIndex>
    {
        public required TConcrete Concrete { get; init; }
        public List<TAnyIndex> AnyIndex { get; init; } = [];
    }

    public readonly struct A((string, long, bool) value)
    {
        public (string, long, bool) Value { get; } = value;

        public static implicit operator A((string, long, bool) value) => new(value);
    }

    public readonly struct B((string, long, bool) value)
    {
        public (string, long, bool) Value { get; } = value;

        public static implicit operator B((string, long, bool) value) => new(value);
    }

    public readonly struct C(CueList<(string, long, bool), CAny> value)
    {
        public CueList<(string, long, bool), CAny> Value { get; } = value;

        public static implicit operator C(CueList<(string, long, bool), CAny> value) => new(value);
    }

    public class CAny
    {
    }
}