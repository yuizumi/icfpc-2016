using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliArrayAccess : ITypeMember, IIndexer
    {
        private readonly Type mArrayType;

        internal CliArrayAccess(Type arrayType)
        {
            mArrayType = arrayType;
        }

        public string Message => "（配列）";

        public void Compile(ICompileContext ctx)
        {
            throw new NFlatBugException();
        }

        public ITypeMember NonStatic() => this;

        public CSharpExpr GetForIndex(IValue instance, CliArguments args)
        {
            if (!args.Has(mArrayType.GetArrayRank()))
                throw Error.DimensionMismatch(instance);
            IEnumerable<string> indexes = args.Get(mArrayType.GetArrayRank())
                .Select(arg => arg.Get(typeof(int)).Code);
            string joined = String.Join(", ", indexes);
            return new CSharpExpr(
                $"{instance.Get(mArrayType)}[{joined}]", mArrayType.GetElementType());
        }
    }
}
