namespace NFlat.Micro
{
    internal abstract class Directive : Keyword
    {
        internal sealed override bool ConsumesUndefined => false;
    }
}
