using System;
using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal class NegOperator : Keyword, ICompileCommand
    {
        internal const string _Text = "符号反転";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var x = ctx.Stack.Pop();
            ctx.Output.Emit(GetExpression(x));
        }

        private CSharpExpr GetExpression(IValue x)
        {
            var xDummy = Expression.Parameter(x.Type);
            Type type;
            try {
                type = Expression.Negate(xDummy).Type;
            } catch (InvalidOperationException) {
                throw Error.OperatorNotApplicable(this, x.Type);
            }
            string xCode = x.Get(x.Type).Code;
            return new CSharpExpr($"-{xCode}", type);
        }
    }
}
