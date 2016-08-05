namespace NFlat.Micro
{
    internal interface IDeclareCommand : ICommand
    {
        void Declare(IDeclareContext context);
    }
}
