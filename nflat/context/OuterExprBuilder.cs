using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class OuterExprBuilder : ExprBuilder
    {
        private readonly List<CSharpExpr> mVariables =
            new List<CSharpExpr>();

        private int mNextVarId = 0;

        internal OuterExprBuilder(ICompileContext owner)
            : base(owner)
        {
        }

        public override CSharpExpr MakeVariable(Type type)
        {
            var newVar = new CSharpExpr($"_{mNextVarId++}", type);
            mVariables.Add(newVar);
            return newVar;
        }

        public override CSharpExpr LoopVariable(Type type)
        {
            var newVar = new CSharpExpr($"_{mNextVarId++}", type);
            return newVar;
        }

        public override string Build()
        {
            var decls = mVariables.Select(v => $"{CSharpString.Type(v.Type)} {v.Code};\n");
            return "{\n" + String.Join("", decls) + GetBody() + "}";
        }
    }
}
