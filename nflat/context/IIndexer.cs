namespace NFlat.Micro
{
    internal interface IIndexer
    {
        IValue GetForIndex(IValue instance, CliArguments args);
    }
}
