using System;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class KeywordPlusMatcher : BasicMatcher
    {
        private const string Pattern = "(?<keyword>" + Patterns.Word + ")" +
            "\\((?<value>[0-9]+)" + Patterns.NumberSuffix + "\\)";

        public KeywordPlusMatcher() : base(Pattern)
        {
        }

        internal override int Priority => 1;

        protected override Stem GetStem(Match match)
        {
            string name = Lexing.Canonicalize(match.Groups["keyword"].Value);
            return (Keywords.Get(name) as KeywordPlus)
                ?.WithSpec(Int32.Parse(match.Groups["value"].Value));
        }
    }
}
