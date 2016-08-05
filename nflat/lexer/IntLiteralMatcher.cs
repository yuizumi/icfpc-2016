using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class IntLiteralMatcher : NumberLiteralMatcher
    {
        private const string ValuePattern = "[+-]?[0-9]+|[0-9][0-9A-F]*H";
        private const string TypePattern = "長|符号無長?";

        public IntLiteralMatcher() : base(ValuePattern, TypePattern)
        {
        }

        protected override Stem GetLiteral(string value, string type)
            => new IntLiteral(value, type);
    }
}
