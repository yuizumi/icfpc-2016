namespace NFlat.Micro
{
    internal class Continue : Keyword, ICompileCommand
    {
        internal const string _Text = "次に移る";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx) => ctx.Continue();
    }
}
