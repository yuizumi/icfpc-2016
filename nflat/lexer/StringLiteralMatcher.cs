using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class StringLiteralMatcher : BasicMatcher
    {
        private const string Pattern = @"「.*?」|"".*?""";

        public StringLiteralMatcher() : base(Pattern)
        {
        }

        protected override Stem GetStem(Match match)
        {
            string stem = match.Groups["stem"].Value;
            return new StringLiteral(stem.Substring(1, stem.Length - 2));
        }
    }
}
