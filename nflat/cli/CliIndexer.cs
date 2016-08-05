using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliIndexer : CliGroupMember
    {
        private readonly PropertyInfo mIndexer;

        internal CliIndexer(PropertyInfo indexer)
        {
            mIndexer = indexer;
        }

        internal override MemberInfo MemberInfo => mIndexer;
        internal override bool HasThis => mIndexer.HasThis();

        internal override ParameterInfo[] GetParameters()
            => mIndexer.GetIndexParameters();

        internal CSharpExpr GetExpression(IValue instance, CliArguments args)
        {
            IEnumerable<string> indexes = Enumerable.Zip(
                GetParameters(), args.Get(GetArity()),
                (param, arg) => arg.Get(param.ParameterType).Code);
            string joined = String.Join(", ", indexes);
            return new CSharpExpr(
                $"{instance.Get(DeclaringType)}[{joined}]", mIndexer.PropertyType);
        }
    }
}
