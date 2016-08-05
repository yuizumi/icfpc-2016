namespace NFlat.Micro
{
    internal interface IResolver
    {
        ICommand Resolve(Identifier name);
    }
}
