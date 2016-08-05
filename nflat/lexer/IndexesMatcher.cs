using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class IndexesMatcher : BasicMatcher
    {
        private const string Pattern = "(?<head>" + Patterns.Identifier + @")\[\S+\]";

        private static readonly Regex IndexRegex =
            new Regex(@"\[(?<args>.+?)\]", RegexOptions.Compiled);

        public IndexesMatcher() : base(Pattern)
        {
        }

        protected override Stem GetStem(Match match)
        {
            var head = Matchers.MatchIdentifier(match.Groups["head"].Value);
            // MatchCollection is a non-generic IEnumerable.
            var indexes = IndexRegex.Matches(match.Groups["stem"].Value).Cast<Match>()
                .Select(ParseIndex);
            return new Compound(Single<Stem>(head).Concat(indexes));
        }

        private static IEnumerable<T> Single<T>(T value)
        {
            yield return value;
        }

        private static IndexStem ParseIndex(Match match)
        {
            string text = match.Groups["args"].Value;
            var args = new List<Stem>();
            foreach (string word in Lexer.SplitLine(text, 0)) {
                var arg = Matchers.MatchStem(word);
                if (!IsValidArgument(arg)) throw Error.InvalidIndexArgument(arg);
                args.Add(arg);
            }
            return new IndexStem(args);
        }

        private static bool IsValidArgument(Stem arg)
        {
            return (arg is Identifier) || (arg is NumberLiteral) || (arg is StringLiteral);
        }
    }
}
