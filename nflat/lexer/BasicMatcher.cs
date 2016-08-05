using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal abstract class BasicMatcher : Matcher
    {
        private readonly Regex mToken;
        private readonly Regex mStem;

        protected BasicMatcher(string stem)
        {
            mToken = new Regex($"^(?<stem>{stem})(?<suffix>{Patterns.Suffix})$",
                               RegexOptions.Compiled);
            mStem = new Regex($"^(?<stem>{stem})$", RegexOptions.Compiled);
        }

        internal override Token MatchToken(string text)
        {
            Match match = mToken.Match(text);
            if (!match.Success)
                return null;
            var stem = GetStem(match);
            if (stem == null)
                return null;
            return new Token(stem, Suffix.FindBestMatch(match.Groups["suffix"].Value));
        }

        internal override Stem MatchStem(string text)
        {
            Match match = mStem.Match(text);
            return (match.Success) ? GetStem(match) : null;
        }

        protected abstract Stem GetStem(Match match);
    }
}
