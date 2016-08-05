using System;

namespace NFlat.Micro
{
    internal class RealLiteral : NumberLiteral
    {
        internal RealLiteral(string value, string type)
            : base(value, type)
        {
        }

        internal override ICommand Parse()
        {
            switch (Type) {
                case "単精度": case "単":
                    return new Constant<Single>(Single.Parse(Value));
                case "倍精度": case "倍":
                    return new Constant<Double>(Double.Parse(Value));
                case "十進":
                    return new Constant<Decimal>(Decimal.Parse(Value));
                default:
                    throw new NFlatBugException();  // Inconsistency with the lexer.
            }
        }
    }
}
