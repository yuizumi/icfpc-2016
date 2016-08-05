namespace NFlat.Micro
{
    internal class Undefined : PseudoValue, ICompileCommand
    {
        internal Undefined(Identifier name)
        {
            Name = name;
        }

        internal Identifier Name { get; }

        public override string Message => $"未定義の識別子（「{Name.Text}」）";

        public void Compile(ICompileContext ctx)
        {
            var kw = ctx.Source.Peek() as Keyword;
            if (kw != null && kw.ConsumesUndefined) {
                ctx.Stack.Push(this);
            } else {
                throw Error.UndefinedIdentifier(Name);
            }
        }

        protected override NFlatException GetError()
            => Error.UndefinedIdentifier(Name);

        public override string ToString() => "Undefined({Name.Text})";
    }
}
