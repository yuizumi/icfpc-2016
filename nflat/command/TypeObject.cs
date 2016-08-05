using System;

namespace NFlat.Micro
{
    internal class TypeObject : Keyword, ICompileCommand
    {
        internal const string _Text = "型オブジェクト";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var expr = new CSharpExpr(
                $"typeof({CSharpString.Type(type)})", typeof(Type));
            ctx.Output.Emit(expr);
        }
    }
}
