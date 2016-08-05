using System;
using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class Equality : Keyword, ICompileCommand
    {
        internal const string _Text = "等しい";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var y = ctx.Stack.Pop();
            var x = ctx.Stack.Pop();
            ctx.Output.Emit(new CSharpExpr(GetCode(x, y), typeof(bool)));
        }

        internal static string GetCode(IValue x, IValue y)
        {
            string xCode = x.Get(x.Type).Code;
            string yCode = y.Get(y.Type).Code;

            if (CanUseOperator(x, y))
                return $"({xCode} == {yCode})";
            if (x.Type.IsValueType)
                return $"{xCode}.Equals({yCode})";
            if (y.Type.IsValueType)
                return $"{yCode}.Equals({xCode})";

            string prefix = "object";
            if (x.Type == y.Type) {
                Type type = typeof(EqualityComparer<>).MakeGenericType(x.Type);
                prefix = $"{CSharpString.Type(type)}.Default";
            }
            return $"{prefix}.Equals({xCode}, {yCode})";
        }

        private static bool CanUseOperator(IValue x, IValue y)
        {
            if (x.Type.IsPrimitive && y.Type.IsPrimitive)
                return true;
            if ((x is NullConstant) || (y is NullConstant))
                return true;
            if (x.Type.GetMethod("op_Equality", new Type[] {x.Type, y.Type}) != null)
                return true;
            if (y.Type.GetMethod("op_Equality", new Type[] {x.Type, y.Type}) != null)
                return true;
            return false;
        }
    }
}
