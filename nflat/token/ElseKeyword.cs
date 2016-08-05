namespace NFlat.Micro
{
    internal class ElseKeyword : Keyword
    {
        internal const string _Text = "さもなければ";
        internal override string Text => _Text;

        internal override ICommand Parse() { throw new NFlatBugException(); }
    }
}
