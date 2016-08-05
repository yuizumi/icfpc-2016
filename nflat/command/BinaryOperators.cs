using System.Linq.Expressions;

namespace NFlat.Micro
{
    internal class AddOperator : BinaryOperator
    {
        internal override string Text => "足す";
        protected override ExpressionType Operator => ExpressionType.Add;
        protected override string CSharp => "+";
    }

    internal class SubOperator : BinaryOperator
    {
        internal override string Text => "引く";
        protected override ExpressionType Operator => ExpressionType.Subtract;
        protected override string CSharp => "-";
    }

    internal class MulOperator : BinaryOperator
    {
        internal override string Text => "掛ける";
        protected override ExpressionType Operator => ExpressionType.Multiply;
        protected override string CSharp => "*";
    }

    internal class DivOperator : BinaryOperator
    {
        internal override string Text => "割る";
        protected override ExpressionType Operator => ExpressionType.Divide;
        protected override string CSharp => "/";
    }

    internal class ModOperator : BinaryOperator
    {
        internal override string Text => "余り";
        protected override ExpressionType Operator => ExpressionType.Modulo;
        protected override string CSharp => "%";
    }

    internal class AndOperator : BinaryOperator
    {
        internal override string Text => "ＡＮＤ";
        protected override ExpressionType Operator => ExpressionType.And;
        protected override string CSharp => "&";
    }

    internal class OrOperator : BinaryOperator
    {
        internal override string Text => "ＯＲ";
        protected override ExpressionType Operator => ExpressionType.Or;
        protected override string CSharp => "|";
    }

    internal class XorOperator : BinaryOperator
    {
        internal override string Text => "ＸＯＲ";
        protected override ExpressionType Operator => ExpressionType.ExclusiveOr;
        protected override string CSharp => "^";
    }

    internal class ShlOperator : BinaryOperator
    {
        internal override string Text => "左シフト";
        protected override ExpressionType Operator => ExpressionType.LeftShift;
        protected override string CSharp => "<<";
    }

    internal class ShrOperator : BinaryOperator
    {
        internal override string Text => "右シフト";
        protected override ExpressionType Operator => ExpressionType.RightShift;
        protected override string CSharp => ">>";
    }

    internal class LeOperator : BinaryOperator
    {
        internal override string Text => "以下";
        protected override ExpressionType Operator => ExpressionType.LessThanOrEqual;
        protected override string CSharp => "<=";
    }

    internal class LtOperator : BinaryOperator
    {
        internal override string Text => "小さい";
        protected override ExpressionType Operator => ExpressionType.LessThan;
        protected override string CSharp => "<";
    }

    internal class GeOperator : BinaryOperator
    {
        internal override string Text => "以上";
        protected override ExpressionType Operator => ExpressionType.GreaterThanOrEqual;
        protected override string CSharp => ">=";
    }

    internal class GtOperator : BinaryOperator
    {
        internal override string Text => "大きい";
        protected override ExpressionType Operator => ExpressionType.GreaterThan;
        protected override string CSharp => ">";
    }
}
