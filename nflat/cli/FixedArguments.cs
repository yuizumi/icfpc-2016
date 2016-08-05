using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class FixedArguments : CliArguments
    {
        internal readonly IReadOnlyList<IValue> mArguments;

        internal FixedArguments(IReadOnlyList<IValue> args)
        {
            mArguments = args;
        }

        internal override bool Has(int arity)
            => (mArguments.Count == arity);

        internal override IEnumerable<IValue> Get(int arity) => mArguments;
    }
}
