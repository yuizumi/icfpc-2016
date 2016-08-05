namespace NFlat.Micro
{
    internal class Using : Keyword, IDeclareCommand
    {
        internal const string _Text = "使用";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 1) { throw Error.InvalidUsage(this); }
            ctx.Import(Pop<TypeValue>(ctx.Stack).NFType);
        }
    }
}
