using System;
using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal abstract class BinaryOperator : Keyword, ICompileCommand
    {
        protected abstract ExpressionType Operator { get; }
        protected abstract string CSharp { get; }

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var y = ctx.Stack.Pop();
            var x = ctx.Stack.Pop();
            ctx.Output.Emit(GetExpression(x, y));
        }

        private CSharpExpr GetExpression(IValue x, IValue y)
        {
            Type xType = x.Type;
            Type yType = y.Type;

            if (xType.IsPrimitive && yType.IsPrimitive) {
                if (x.Has(yType)) xType = yType;
                if (y.Has(xType)) yType = xType;
            }

            var xDummy = Expression.Parameter(xType);
            var yDummy = Expression.Parameter(yType);
            Type type;
            try {
                type = Expression.MakeBinary(Operator, xDummy, yDummy).Type;
            } catch (InvalidOperationException) {
                throw Error.OperatorNotApplicable(this, x.Type, y.Type);
            }
            string xCode = x.Get(xType).Code;
            string yCode = y.Get(yType).Code;
            return new CSharpExpr($"({xCode} {CSharp} {yCode})", type);
        }
    }
}
