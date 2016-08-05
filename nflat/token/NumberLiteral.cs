namespace NFlat.Micro
{
    internal abstract class NumberLiteral : Stem
    {
        internal NumberLiteral(string value, string type)
        {
            Value = value;
            Type = type;
        }

        protected string Value { get; }
        protected string Type { get; }

        internal override string Text
        {
            get {
                return (Type == null) ? Value : $"{Value}（{Type}）";
            }
        }
    }
}
