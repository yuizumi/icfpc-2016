using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class IndexesMatcher : BasicMatcher
    {
        private const string Pattern = @"(?<head>" + Patterns.Identifier + ")\\[.+\\]";

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
            List<Stem> args = match.Groups["args"].Value.Split('、')
                .Select(Matchers.MatchStem).ToList();
            Stem error = args.FirstOrDefault(x => !IsValidArgument(x));
            if (error != null) throw Error.InvalidIndexArgument(error);
            return new IndexStem(args);
        }

        private static bool IsValidArgument(Stem stem)
        {
            return (stem is Identifier)
                || (stem is NumberLiteral) || (stem is StringLiteral);
        }
    }
}
