namespace NFlat.Micro
{
    internal interface ICompileCommand : ICommand
    {
        void Compile(ICompileContext context);
    }
}
