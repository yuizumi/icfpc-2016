using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class CliTypeNameMatcher : BasicMatcher
    {
        public CliTypeNameMatcher() : base(Patterns.CliTypeName)
        {
        }

        protected override Stem GetStem(Match match)
        {
            string stem = match.Groups["stem"].Value;
            return CliTypeName.Of(stem.Substring(3, stem.Length - 4));
        }
    }
}
