namespace NFlat.Micro
{
    internal class Self : Keyword, ICompileCommand
    {
        internal const string _Text = "自分";
        internal override string Text => _Text;
        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var expr = ctx.GetSelf();
            if (expr.Code == null) throw Error.SelfNotDefined();
            ctx.Stack.Push(new Local(expr));
        }
    }
}
