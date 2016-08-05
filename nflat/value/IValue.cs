using System;

namespace NFlat.Micro
{
    internal interface IValue
    {
        bool IsAssignable { get; }
        bool IsStable { get; }
        Type Type { get; }

        string Message { get; }

        CSharpExpr Get(Type type);
        bool Has(Type type);
    }
}
