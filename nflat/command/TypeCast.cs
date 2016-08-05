using System;
using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal class TypeCast : Keyword, ICompileCommand
    {
        internal const string _Text = "型変換";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var value = ctx.Stack.Pop();

            var dummy = Expression.Parameter(value.Type);
            try {
                Expression.Convert(dummy, type);
            } catch (InvalidOperationException) {
                throw Error.TypeNotConvertible(value.Type, type);
            }
            var expr = new CSharpExpr(
                $"(({CSharpString.Type(type)}) {value.Get(value.Type)})", type);
            ctx.Output.Emit(expr);
        }
    }
}
