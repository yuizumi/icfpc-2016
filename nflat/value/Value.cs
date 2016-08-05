using System;

namespace NFlat.Micro
{
    internal abstract class Value : IValue
    {
        protected abstract string Code { get; }
        public abstract Type Type { get; }

        public virtual bool IsAssignable => false;
        public virtual bool IsStable => true;

        public abstract string Message { get; }

        public virtual CSharpExpr Get(Type type)
            => new CSharpExpr(Code, type);

        public virtual bool Has(Type type)
            => Type.HasImplicitTo(type);

        public override string ToString()
            => $"{GetType().Name}<{Type}>({Code})";
    }
}
