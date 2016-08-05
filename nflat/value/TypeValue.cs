using System;

namespace NFlat.Micro
{
    internal class TypeValue : PseudoValue, ICompileCommand, IDeclareCommand
    {
        internal TypeValue(NFType nftype)
        {
            NFType = nftype;
        }

        internal NFType NFType { get; }

        public override string Message => $"型名（「{NFType.Text}」）";

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public void Declare(IDeclareContext ctx)
            => ctx.Stack.Push(this);

        protected override NFlatException GetError()
            => Error.InvalidValue(this);

        public override string ToString() => $"TypeValue({NFType.Text})";
    }
}
