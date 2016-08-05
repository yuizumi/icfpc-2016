namespace NFlat.Micro
{
    internal class Break : Keyword, ICompileCommand
    {
        internal const string _Text = "打ち切り";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx) => ctx.Break();
    }
}
