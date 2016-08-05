namespace NFlat.Micro
{
    internal interface IParsedSource
    {
        string FileName { get; }
        int  LineNumber { get; }

        ICommand Peek();
        ICommand Next();
        IParsedSource NextBlock();
    }
}
