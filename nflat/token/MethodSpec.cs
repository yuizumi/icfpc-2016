namespace NFlat.Micro
{
    internal class MethodSpec : Stem, ICompileCommand, IDeclareCommand
    {
        internal MethodSpec(Identifier name, int arity)
        {
            Name = name;
            Arity = arity;
        }

        internal Identifier Name { get; }
        internal int Arity { get; }

        internal override string Text => $"{Name.Text}（{Arity}引数）";
        public string Message => $"「{Text}」";

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            WithArity(Name.Resolve(ctx)).Compile(ctx);
        }

        public void Declare(IDeclareContext ctx)
        {
            WithArity(Name.Resolve(ctx)).Declare(ctx);
        }

        private CliMethodGroup WithArity(ICommand command)
        {
            return (command as CliMethodGroup).ThrowOnNull(Error.NotMethod(Name))
                .WithArity(Arity);
        }

        public override string ToString()
            => $"MethodSpec({Name.Text}, {Arity})";
    }
}
