using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal abstract class NumberLiteralMatcher : BasicMatcher
    {
        public NumberLiteralMatcher(string value, string type)
            : base(CreatePattern(value, type))
        {
        }

        private static string CreatePattern(string value, string type)
        {
            return $@"(?<value>{value}){Patterns.NumberSuffix}" +
                $@"(?:\((?<type>{type})\))?";
        }

        protected override Stem GetStem(Match match)
        {
            string value = match.Groups["value"].Value;
            if (!match.Groups["type"].Success)
                return GetLiteral(value, null);
            return GetLiteral(value, match.Groups["type"].Value);
        }

        protected abstract Stem GetLiteral(string value, string type);
    }
}
