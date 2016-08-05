namespace NFlat.Micro
{
    internal interface IIndexer
    {
        CSharpExpr GetForIndex(IValue instance, CliArguments args);
    }
}
