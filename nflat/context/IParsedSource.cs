namespace NFlat.Micro
{
    internal interface IParsedSource
    {
        int LineNumber { get; }

        ICommand Peek();
        ICommand Next();
        IParsedSource NextBlock();
    }
}
