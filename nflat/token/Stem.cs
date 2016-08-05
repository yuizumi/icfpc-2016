namespace NFlat.Micro
{
    internal abstract class Stem
    {
        internal abstract string Text { get; }

        internal abstract ICommand Parse();
        public override string ToString() => $"{GetType().Name}({Text})";
    }
}
