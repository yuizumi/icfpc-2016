namespace NFlat.Micro
{
    internal class Token
    {
        internal Token(Stem stem, Suffix suffix)
        {
            Stem = stem;
            Suffix = suffix;
        }

        internal Stem Stem { get; }
        internal Suffix Suffix { get; }

        internal ICommand Parse()
        {
            return (Suffix.IsDefining && !(Stem is Directive))
                ? MakeNewEntity(Stem) : Stem.Parse();
        }

        private static NewEntity MakeNewEntity(Stem stem)
        {
            var name = (stem as Identifier).ThrowOnNull(Error.NotRedefinable(stem));
            return new NewEntity(name);
        }

        public override string ToString() => $"{Stem}{Suffix}";
    }
}
