namespace NFlat.Micro
{
    internal class ExitMethod : Keyword, ICompileCommand
    {
        internal const string _Text = "終わる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx) => ctx.Return();
    }
}
