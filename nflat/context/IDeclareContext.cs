using System;

namespace NFlat.Micro
{
    internal interface IDeclareContext : IContext
    {
        IParsedSource Source { get; }
        DeclareStack Stack { get; }

        void Compile();

        void CompileToClass(Identifier name);
        void CompileToStruct(Identifier name);
        void CompileToModule(Identifier name);
        void CompileToField(Identifier name, Type type, bool hasThis);
        void CompileToMethod(Identifier name, bool hasThis);
    }
}
