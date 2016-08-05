using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal static class Matchers
    {
        private static readonly IReadOnlyList<Matcher> MatcherList =
            TypeFinder.CreateObjects<Matcher>()
            .OrderByDescending(m => m.Priority)
            .ToList().AsReadOnly();

        internal static Token MatchToken(string text)
        {
            return MatcherList.Select(matcher => matcher.MatchToken(text))
                .FirstOrDefault(matched => matched != null)
                .ThrowOnNull(Error.InvalidToken(text));
        }

        internal static Stem MatchStem(string text)
        {
            return MatcherList.Select(matcher => matcher.MatchStem (text))
                .FirstOrDefault(matched => matched != null)
                .ThrowOnNull(Error.InvalidToken(text));
        }

        internal static Identifier MatchIdentifier(string text)
        {
            return (MatchStem(text) as Identifier).ThrowOnNull(Error.IdentifierExpected(text));
        }
    }
}
