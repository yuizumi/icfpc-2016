using System;

namespace NFlat.Micro
{
    internal class Variable : Value, ICompileCommand
    {
        internal Variable(CSharpExpr expr, Identifier name)
        {
            Code = expr.Code;
            Type = expr.Type;
            Name = name;
        }

        protected override string Code { get; }
        public override Type Type { get; }

        internal Identifier Name { get; }

        public override bool IsAssignable => true;
        public override bool IsStable => Type.IsValueType;

        public override string Message => $"変数「{Name.Text}」";

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public override string ToString() => $"Variable<{Type}>({Name.Text})";
    }
}
