namespace NFlat.Micro
{
    internal class DeclareReturn : Directive, ICommand
    {
        internal const string _Text = "戻り値";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;
    }
}
