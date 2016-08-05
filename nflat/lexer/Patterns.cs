namespace NFlat.Micro
{
    internal static class Patterns
    {
        private const string First = @"\p{Lu}\p{Ll}\p{Lt}\p{Lm}\p{Lo}\p{Nl}";
        private const string Other = @"\p{Mn}\p{Mc}\p{Nd}\p{Pc}\p{Cf}";

        internal const string Word = "[" + First + "][" + First + Other + "]*?\\??";
        internal const string Name = "[_" + First + "][" + First + Other + "]*?";

        internal const string RawIdentifier = @"\|(?:" + Word + "|" + Name + @")\|";
        internal const string CliTypeName = @"\|T:.*?\|";

        internal const string Identifier = Word + "|" + RawIdentifier;

        internal const string Suffix = @"\p{IsHiragana}*";
        internal const string NumberSuffix = @"(?:\p{Lo}+|\p{Sc})?";
    }
}
