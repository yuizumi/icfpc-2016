using System;
using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal class NegOperator : UnaryOperator, ICompileCommand
    {
        internal override string Text => "符号反転";

        protected override string GetCode(IValue x)
            => $"-{x.Get(x.Type)}";

        protected override Expression GetExpression(Expression x)
            => Expression.Negate(x);
    }

    internal class NotOperator : UnaryOperator, ICompileCommand
    {
        internal override string Text => "NOT";

        protected override Expression GetExpression(Expression x)
            => Expression.Not(x);

        protected override string GetCode(IValue x)
        {
            return ((x.Type == typeof(bool)) ? "!" : "~") + x.Get(x.Type).Code;
        }
    }

    internal class TrueOperator : UnaryOperator, ICompileCommand
    {
        internal override string Text => "真？";

        protected override Expression GetExpression(Expression x)
            => Expression.IsTrue(x);

        protected override string GetCode(IValue x)
            => GetCodeImpl(x);

        internal static string GetCodeImpl(IValue x)
        {
            string xCode = x.Get(x.Type).Code;
            return (x.Type == typeof(bool)) ? xCode : $"({xCode} ? true : false)";
        }
    }

    internal class FalseOperator : UnaryOperator, ICompileCommand
    {
        internal override string Text => "偽？";

        protected override Expression GetExpression(Expression x)
            => Expression.IsFalse(x);

        protected override string GetCode(IValue x)
            => "!" + TrueOperator.GetCodeImpl(x);
    }
}
