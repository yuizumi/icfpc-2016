using System;
using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class MethodSpecMatcher : BasicMatcher
    {
        private const string Pattern = "(?<name>" + Patterns.Identifier + ")" +
            "\\((?<arity>[0-9]+)" + Patterns.NumberSuffix + "\\)";

        public MethodSpecMatcher() : base(Pattern)
        {
        }

        protected override Stem GetStem(Match match)
        {
            Identifier name = Matchers.MatchIdentifier(match.Groups["name"].Value);
            return new MethodSpec(name, Int32.Parse(match.Groups["arity"].Value));
        }
    }
}
