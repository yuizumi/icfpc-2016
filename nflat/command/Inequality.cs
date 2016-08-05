namespace NFlat.Micro
{
    internal class Inequality : Keyword, ICompileCommand
    {
        internal const string _Text = "異なる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var y = ctx.Stack.Pop();
            var x = ctx.Stack.Pop();
            ctx.Output.Emit(new CSharpExpr($"!{Equality.GetCode(x, y)}", typeof(bool)));
        }
    }
}
