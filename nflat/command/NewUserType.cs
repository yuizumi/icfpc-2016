namespace NFlat.Micro
{
    internal class NewUserClass : Keyword, IDeclareCommand
    {
        internal const string _Text = "クラス";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 1) { throw Error.InvalidUsage(this); }
            ctx.CompileToClass(Pop<NewEntity>(ctx.Stack).Name);
        }
    }

    internal class NewUserStruct : Keyword, IDeclareCommand
    {
        internal const string _Text = "構造体";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 1) { throw Error.InvalidUsage(this); }
            ctx.CompileToStruct(Pop<NewEntity>(ctx.Stack).Name);
        }
    }
}
