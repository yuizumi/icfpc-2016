using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class CharLiteralMatcher : BasicMatcher
    {
        private const string Pattern = @"'.'";

        public CharLiteralMatcher() : base(Pattern)
        {
        }

        protected override Stem GetStem(Match match)
        {
            string value = match.Groups["stem"].Value;
            return new CharLiteral(value[1]);
        }
    }
}
