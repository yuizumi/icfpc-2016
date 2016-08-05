namespace NFlat.Micro
{
    internal interface ITypeMember : ICompileCommand
    {
        ITypeMember NonStatic();
    }
}
