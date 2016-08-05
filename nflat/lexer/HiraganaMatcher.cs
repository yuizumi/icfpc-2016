using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class HiraganaMatcher : Matcher
    {
        private static readonly Regex Hiragana =
            new Regex(@"^\p{IsHiragana}+$", RegexOptions.Compiled);

        internal override int Priority => 1;

        internal override Token MatchToken(string text)
        {
            Match match = Hiragana.Match(text);
            if (!match.Success)
                return null;
            Suffix suffix = Suffix.FindBestMatch(text);
            string stem = text.Substring(0, text.Length - suffix.Text.Length);
            return new Token(WordMatcher.GetStem(stem), suffix);
        }

        internal override Stem MatchStem(string text)
        {
            Match match = Hiragana.Match(text);
            return (match.Success) ? WordMatcher.GetStem(match.Value) : null;
        }
    }
}
