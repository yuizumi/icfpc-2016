using System;

namespace NFlat.Micro
{
    internal class InnerExprBuilder : ExprBuilder
    {
        private readonly IExprBuilder mOuterBuilder;

        internal InnerExprBuilder(ICompileContext owner, IExprBuilder outer)
            : base(owner)
        {
            mOuterBuilder = outer;
        }

        public override CSharpExpr MakeVariable(Type type)
            => mOuterBuilder.MakeVariable(type);

        public override CSharpExpr LoopVariable(Type type)
            => mOuterBuilder.LoopVariable(type);

        public override string Build() => "{\n" + GetBody() + "}";
    }
}
