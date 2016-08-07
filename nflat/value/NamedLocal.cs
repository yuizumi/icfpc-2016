using System;

namespace NFlat.Micro
{
    internal class NamedLocal : Local, ICompileCommand
    {
        internal NamedLocal(CSharpExpr expr, Identifier name)
            : base(expr)
        {
            Name = name;
        }

        public Identifier Name { get; }

        public override string Message => $"「{Name.Text}」";

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public override string ToString() => $"NamedLocal<{Type}>({Name.Text})";
    }
}
