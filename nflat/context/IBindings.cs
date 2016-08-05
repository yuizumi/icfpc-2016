namespace NFlat.Micro
{
    internal interface IBindings : IResolver
    {
        void Define(Identifier name, ICommand command);
    }
}
