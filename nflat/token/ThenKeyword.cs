namespace NFlat.Micro
{
    internal class ThenKeyword : Keyword
    {
        internal const string _Text = "ならば";
        internal override string Text => _Text;

        internal override ICommand Parse() { throw new NFlatBugException(); }
    }
}
