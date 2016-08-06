namespace NFlat.Micro
{
    internal class CharLiteral : Stem
    {
        internal CharLiteral(char value)
        {
            Value = value;
        }

        private char Value { get; }

        internal override string Text => Value.ToPrettyString();

        internal override ICommand Parse()
            => new Constant<char>(Value);
    }
}
