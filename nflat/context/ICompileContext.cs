namespace NFlat.Micro
{
    internal interface ICompileContext : IContext
    {
        IParsedSource Source { get; }
        IExprBuilder  Output { get; }

        CompileStack Stack { get; }

        void Compile();

        void Return();
        void Break();
        void Continue();

        CSharpExpr GetSelf();

        ICompileContext CondContext(string name);
        ICompileContext LoopContext(string name);
        ICompileContext InitContext(string name);
    }
}
