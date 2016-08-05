namespace NFlat.Micro
{
    internal class NewEntity : PseudoValue, ICompileCommand, IDeclareCommand
    {
        internal NewEntity(Identifier name)
        {
            Name = name;
        }

        internal Identifier Name { get; }

        public override string Message => $"未定義の識別子（「{Name.Text}」）";

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public void Declare(IDeclareContext ctx)
            => ctx.Stack.Push(this);

        protected override NFlatException GetError()
            => Error.IncompleteDefinition(Name);

        public override string ToString() => $"NewEntity({Name.Text})";
    }
}
