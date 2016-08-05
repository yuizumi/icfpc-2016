using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class RawIdentifierMatcher : BasicMatcher
    {
        public RawIdentifierMatcher() : base(Patterns.RawIdentifier)
        {
        }

        protected override Stem GetStem(Match match)
        {
            string stem = match.Groups["stem"].Value;
            return Identifier.Of(stem.Substring(1, stem.Length - 2));
        }
    }
}
