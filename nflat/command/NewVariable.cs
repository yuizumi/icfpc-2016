namespace NFlat.Micro
{
    internal class NewVariable : Keyword, ICompileCommand
    {
        internal const string _Text = "変数";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var type = Pop<TypeValue>(ctx.Stack).NFType.Type;
            var name = Pop<NewEntity>(ctx.Stack).Name;
            var expr = new CSharpExpr(name.CSharp, type);
            string code = $"{CSharpString.Type(expr.Type)} {expr.Code}";
            if (ctx.Source.Peek() is InitialValue) {
                var value = (ctx.Source.Next() as InitialValue).Compile(ctx, type);
                ctx.Output.RawEmit($"{code} = {value};");
            } else {
                ctx.Output.RawEmit($"{code};");
            }
            ctx.Bindings.Define(name, new Variable(expr, name));
        }
    }
}
