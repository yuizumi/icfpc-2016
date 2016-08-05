using System;
using System.Text;

namespace NFlat.Micro
{
    internal abstract class ExprBuilder : IExprBuilder
    {
        private readonly StringBuilder mBody = new StringBuilder();

        protected ExprBuilder(ICompileContext owner)
        {
            Owner = owner;
        }

        public ICompileContext Owner { get; }

        public void RawEmit(string code) => mBody.Append($"{code}\n");

        public void Emit(CSharpExpr expr)
        {
            if (expr.Type == typeof(void)) {
                RawEmit($"{expr.Code};");
            } else {
                CSharpExpr local = MakeVariable(expr.Type);
                RawEmit($"{local.Code} = {expr.Code};");
                Owner.Stack.Push( new Local(local) );
            }
        }

        public abstract string Build();
        public abstract CSharpExpr MakeVariable(Type type);
        public abstract CSharpExpr LoopVariable(Type type);

        protected string GetBody() => mBody.ToString();
    }
}
