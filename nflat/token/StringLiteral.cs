namespace NFlat.Micro
{
    internal class StringLiteral : Stem
    {
        internal StringLiteral(string value)
        {
            Value = value;
        }

        private string Value { get; }

        internal override string Text => Value.ToPrettyString();

        internal override ICommand Parse()
            => new Constant<string>(Value);
    }
}
