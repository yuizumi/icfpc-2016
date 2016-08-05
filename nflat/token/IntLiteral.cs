using System;

using static System.Globalization.CultureInfo;

namespace NFlat.Micro
{
    internal class IntLiteral : NumberLiteral
    {
        internal IntLiteral(string value, string type)
            : base(value, type)
        {
        }

        internal override ICommand Parse()
        {
            bool isHex = Value.EndsWith("H", true, InvariantCulture);

            string value = (isHex) ? Value.Remove(Value.Length - 1) : Value;
            int baseNumber = (isHex) ? 16 : 10;

            switch (Type) {
                case null:
                    return new Constant<Int32>(Convert.ToInt32(value, baseNumber));
                case "長":
                    return new Constant<Int64>(Convert.ToInt64(value, baseNumber));
                case "符号無":
                    return new Constant<UInt32>(Convert.ToUInt32(value, baseNumber));
                case "符号無長":
                    return new Constant<UInt64>(Convert.ToUInt64(value, baseNumber));
                default:
                    throw new NFlatBugException();  // Inconsistency with the lexer.
            }
        }
    }
}
