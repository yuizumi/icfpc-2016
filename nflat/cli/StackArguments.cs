using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class StackArguments : CliArguments
    {
        private readonly IReadOnlyList<IValue> mValues;
        private int mReserved;

        internal StackArguments(NFStack stack, int reserved)
        {
            mValues = stack.Content;
            mReserved = reserved;
        }

        private int Count => mValues.Count - mReserved;

        internal override bool Has(int arity)
            => (Count >= arity);

        internal override IEnumerable<IValue> Get(int arity)
            => mValues.Skip(Count - arity).Take(arity);
    }
}
