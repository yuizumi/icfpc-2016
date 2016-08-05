using System;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class ArrayKeywordMatcher : BasicMatcher
    {
        private const string Pattern = "(?<keyword>" + Patterns.Word + ")" +
            "\\((?<dimension>[0-9]+)" + Patterns.NumberSuffix + "\\)";

        public ArrayKeywordMatcher() : base(Pattern)
        {
        }

        internal override int Priority => 1;

        protected override Stem GetStem(Match match)
        {
            string name = Lexing.Canonicalize(match.Groups["keyword"].Value);
            return (Keywords.Get(name) as ArrayKeyword)
                ?.WithDimension(Int32.Parse(match.Groups["dimension"].Value));
        }
    }
}
