using System.Collections.Generic;

namespace NFlat.Micro
{
    internal abstract class CliArguments
    {
        internal abstract IEnumerable<IValue> Get(int arity);
        internal abstract bool Has(int arity);
    }
}
