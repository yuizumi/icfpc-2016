namespace NFlat.Micro
{
    internal class Duplicate : Keyword, ICompileCommand
    {
        internal const string _Text = "複写";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx) => ctx.Stack.Push(ctx.Stack.Peek());
    }

    internal class Discard : Keyword, ICompileCommand
    {
        internal const string _Text = "捨てる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx) => ctx.Stack.Pop();
    }
}
