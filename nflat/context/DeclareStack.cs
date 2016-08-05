using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class DeclareStack : NFStack
    {
        protected override List<IValue> Values { get; } = new List<IValue>();
    }
}
