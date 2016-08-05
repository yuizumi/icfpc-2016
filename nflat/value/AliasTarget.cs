namespace NFlat.Micro
{
    internal class AliasTarget : PseudoValue, IDeclareCommand
    {
        internal AliasTarget(ICommand target)
        {
            Target = target;
        }

        internal ICommand Target { get; }

        public override string Message => $"別名の対象（{Target.Message}）";

        public void Declare(IDeclareContext ctx)
            => ctx.Stack.Push(this);

        protected override NFlatException GetError()
            => Error.InvalidValue(this);

        public override string ToString() => $"AliasTarget({Target})";
    }
}
