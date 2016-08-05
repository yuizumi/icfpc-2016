using System;

namespace NFlat.Micro
{
    internal class NullConstant : Keyword, IValue, ICompileCommand
    {
        internal const string _Text = "ヌル";
        internal override string Text => _Text;

        public bool IsAssignable => false;
        public bool IsStable => true;
        public Type Type => typeof(object);

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public CSharpExpr Get(Type type)
            => new CSharpExpr("null", type);

        public bool Has(Type type)
        {
            return !type.IsValueType || type.IsGenericOf(typeof(Nullable<>));
        }

        public override string ToString() => "NullConstant()";
    }
}
