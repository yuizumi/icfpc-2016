namespace NFlat.Micro
{
    internal class InfiniteLoop : Keyword, ICompileCommand
    {
        internal const string _Text = "繰り返す";
        internal override string Text => _Text;

        public void Compile(ICompileContext ctx)
        {
            var ctxLoop = ctx.LoopContext(Text);
            ctxLoop.Compile();
            ctxLoop.Stack.AdjustTo(ctx.Stack);
            ctx.Output.RawEmit($"while (true) {ctxLoop.Output.Build()}");
        }

        internal override ICommand Parse() => this;
    }
}
