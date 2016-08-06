using System;

namespace NFlat.Micro
{
    internal class Indexed : Value
    {
        internal Indexed(CSharpExpr expr, bool assignable)
        {
            Code = expr.Code;
            Type = expr.Type;
            IsAssignable = assignable;
        }

        protected override string Code { get; }
        public override Type Type { get; }

        public override bool IsAssignable { get; }
        public override bool IsStable => false;

        public override string Message => "配列／インデクサーの要素";
    }
}
