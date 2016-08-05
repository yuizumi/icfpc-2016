namespace NFlat.Micro
{
    internal class DeclareParams : Directive, ICommand
    {
        internal const string _Text = "引数";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;
    }
}
