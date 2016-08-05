using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class CommentMatcher : Matcher
    {
        private static readonly Regex CommentRegex =
            new Regex(@"^(?:\(.*?\)|※.*)$", RegexOptions.Compiled);

        internal override Token MatchToken(string text)
        {
            var stem = MatchStem(text);
            return (stem == null) ? null : new Token(stem, Suffix.None);
        }

        internal override Stem MatchStem(string text)
        {
            Match match = CommentRegex.Match(text);
            return (match.Success) ? Comment.Stem : null;
        }
    }
}
