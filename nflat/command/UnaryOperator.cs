using System;
using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal abstract class UnaryOperator : Keyword, ICompileCommand
    {
        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();
            var x = ctx.Stack.Pop();
            var xDummy = Expression.Parameter(x.Type);
            Type type;
            try {
                type = GetExpression(xDummy).Type;
            } catch (InvalidOperationException) {
                throw Error.OperatorNotApplicable(this, x.Type);
            }
            ctx.Output.Emit(new CSharpExpr(GetCode(x), type));
        }

        protected abstract Expression GetExpression(Expression x);
        protected abstract string GetCode(IValue x);
    }
}
