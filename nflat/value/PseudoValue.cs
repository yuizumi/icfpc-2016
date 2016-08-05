using System;

namespace NFlat.Micro
{
    internal abstract class PseudoValue : IValue
    {
        bool IValue.IsAssignable => false;
        bool IValue.IsStable => false;
        Type IValue.Type => null;

        public abstract string Message { get; }

        CSharpExpr IValue.Get(Type type) { throw GetError(); }
        bool IValue.Has(Type type) => false;

        protected abstract NFlatException GetError();
    }
}
