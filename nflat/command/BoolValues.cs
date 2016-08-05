namespace NFlat.Micro
{
    internal class FalseValue : Keyword
    {
        internal const string _Text = "偽";
        internal override string Text => _Text;
        internal override ICommand Parse() => new Constant<bool>(false);
    }

    internal class TrueValue : Keyword
    {
        internal const string _Text = "真";
        internal override string Text => _Text;
        internal override ICommand Parse() => new Constant<bool>(true);
    }
}
