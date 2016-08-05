using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class Bracket
    {
        internal static readonly IReadOnlyDictionary<char, Bracket> Brackets =
            GetAllBrackets().ToDictionary(b => b.Open).AsReadOnly();

        internal static readonly Bracket None =
            new Bracket('\0', '\0', false, true);

        private Bracket(char open, char close, bool isHeadOnly, bool normalize)
        {
            Open = open;
            Close = close;
            IsHeadOnly = isHeadOnly;
            Normalize = normalize;
        }

        internal char Open { get; }
        internal char Close { get; }
        internal bool IsHeadOnly { get; }
        internal bool Normalize { get; }

        private static IEnumerable<Bracket> GetAllBrackets()
        {
            // Supplementals, comments.
            yield return new Bracket('(', ')', false, true);
            // Indexes, math expressions.
            yield return new Bracket('[', ']', false, true);
            // CLI type names, raw identifiers.
            yield return new Bracket('|', '|', true, false);
            // String literals.
            yield return new Bracket('「', '」', true, false);
            yield return new Bracket('\"', '\"', true, false);
            // Character literals.
            yield return new Bracket('\'', '\'', true, false);
        }
    }
}
