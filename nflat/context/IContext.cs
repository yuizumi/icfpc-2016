namespace NFlat.Micro
{
    internal interface IContext
    {
        IContext Parent { get; }
        string Name { get; }
        IBindings Bindings { get; }
        TypePool TypePool { get; }
    }
}
