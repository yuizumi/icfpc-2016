using System;

namespace NFlat.Micro
{
    internal class Local : Value
    {
        internal Local(CSharpExpr expr)
        {
            Code = expr.Code;
            Type = expr.Type;
        }

        protected override string Code { get; }
        public override Type Type { get; }

        public override string Message => $"{Type} 型の値";
    }
}
