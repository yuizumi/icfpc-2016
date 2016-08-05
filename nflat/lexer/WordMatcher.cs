using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class WordMatcher : BasicMatcher
    {
        public WordMatcher() : base(Patterns.Word) {}

        protected override Stem GetStem(Match match)
            => GetStem(match.Groups["stem"].Value);

        internal static Stem GetStem(string text)
        {
            string name = Lexing.Canonicalize(text);
            return (Keywords.Get(name) as Stem) ?? Identifier.Of(name);
        }
    }
}
