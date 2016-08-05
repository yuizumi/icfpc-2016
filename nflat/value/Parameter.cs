using System;

namespace NFlat.Micro
{
    internal class Parameter : Local
    {
        internal Parameter(CSharpExpr expr) : base(expr)
        {
        }

        public override string Message => $"{Type} 型の引数";
    }
}
