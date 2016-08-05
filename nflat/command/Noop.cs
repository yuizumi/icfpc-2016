namespace NFlat.Micro
{
    internal class Noop : Keyword, ICompileCommand
    {
        internal const string _Text = "無処理";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;
        public void Compile(ICompileContext ctx) {}
    }
}
