using System;
using System.Linq;

namespace NFlat.Micro
{
    internal class MakeArray : ArrayKeyword, ICompileCommand
    {
        internal const string _Text = "配列作成";

        private readonly int mDimension;

        private MakeArray(int dimension)
        {
            mDimension = dimension;
        }

        public MakeArray() : this(1) {}

        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        internal override Stem WithDimension(int dimension)
            => new MakeArray(dimension);

        public void Compile(ICompileContext ctx)
        {
            Type type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var lengths = new string[mDimension];
            for (int i = mDimension - 1; i >= 0; i--) {
                lengths[i] = ctx.Stack.Pop().Get(typeof(int)).Code;
            }
            Type arrayType = type.MakeArrayType(mDimension);
            var expr = new CSharpExpr(
                $"new {CSharpString.Type(type)}[{String.Join(", ", lengths)}]", arrayType);
            ctx.Output.Emit(expr);
        }
    }
}
