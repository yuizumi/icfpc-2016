namespace NFlat.Micro
{
    internal class NewUserField : Keyword, IDeclareCommand
    {
        internal const string _Text = "フィールド";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 2) { throw Error.InvalidUsage(this); }
            var type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var name = Pop<NewEntity>(ctx.Stack).Name;
            ctx.CompileToField(name, type, true);
        }
    }

    internal class NewUserStaticField : Keyword, IDeclareCommand
    {
        internal const string _Text = "クラスフィールド";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 2) { throw Error.InvalidUsage(this); }
            var type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var name = Pop<NewEntity>(ctx.Stack).Name;
            ctx.CompileToField(name, type, false);
        }
    }

    internal class NewUserMethod : Keyword, IDeclareCommand
    {
        internal const string _Text = "メソッド";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 1) { throw Error.InvalidUsage(this); }
            ctx.CompileToMethod(Pop<NewEntity>(ctx.Stack).Name, true);
        }
    }

    internal class NewUserMethodStatic : Keyword, IDeclareCommand
    {
        internal const string _Text = "クラスメソッド";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 1) { throw Error.InvalidUsage(this); }
            ctx.CompileToMethod(Pop<NewEntity>(ctx.Stack).Name, false);
        }
    }
}
