namespace NFlat.Micro
{
    internal interface INamedValue : IValue
    {
        Identifier Name { get; }
    }
}
