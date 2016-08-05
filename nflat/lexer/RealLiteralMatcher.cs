using System.Text.RegularExpressions;

namespace NFlat.Micro
{
    internal class RealLiteralMatcher : NumberLiteralMatcher
    {
        private const string ValuePattern = @"[+-]?[0-9]+(?:\.[0-9]*)?" +
            @"(?:E[+-]?[0-9]+)?";
        private const string TypePattern = "単精度|単|倍精度|倍|十進";

        public RealLiteralMatcher() : base(ValuePattern, TypePattern)
        {
        }

        internal override int Priority => -1;

        protected override Stem GetLiteral(string value, string type)
            => new RealLiteral(value, type ?? "倍精度");
    }
}
