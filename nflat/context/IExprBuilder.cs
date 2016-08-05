using System;

namespace NFlat.Micro
{
    internal interface IExprBuilder
    {
        ICompileContext Owner { get; }

        void RawEmit(string code);
        void Emit(CSharpExpr expr);
        CSharpExpr MakeVariable(Type type);
        CSharpExpr LoopVariable(Type type);
        string Build();
    }
}
