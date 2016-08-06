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
            ctx.Stack.ForceEvaluate();
            var y = ctx.Stack.Pop();
            var x = ctx.Stack.Pop();
            ctx.Output.Emit(GetExpression(x, y));
        }

        internal CSharpExpr GetExpression(IValue x, IValue y)
        {
            CSharpExpr expr = default(CSharpExpr);
            if (TryGetExpr(x, x.Type, y, y.Type, ref expr)) { return expr; }
            if (TryGetExpr(x, x.Type, y, x.Type, ref expr)) { return expr; }
            if (TryGetExpr(x, y.Type, y, y.Type, ref expr)) { return expr; }
            throw Error.OperatorNotApplicable(this, x.Type, y.Type);
        }

        private bool TryGetExpr(IValue x, Type xType, IValue y, Type yType,
                                ref CSharpExpr expr)
        {
            if (!x.Has(xType) || !y.Has(yType)) {
                return false;
            }
            var xDummy = Expression.Parameter(xType);
            var yDummy = Expression.Parameter(yType);
            Type type;
            try {
                type = Expression.MakeBinary(Operator, xDummy, yDummy).Type;
            } catch (InvalidOperationException) {
                return false;
            }
            expr = new CSharpExpr(
                $"({x.Get(xType).Code} {CSharp} {y.Get(yType).Code})", type);
            return true;
        }
    }
}
