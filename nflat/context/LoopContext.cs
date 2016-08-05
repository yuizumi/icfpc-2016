namespace NFlat.Micro
{
    internal class LoopContext : CompileContext
    {
        internal LoopContext(CompileContext parent, string name,
                             IParsedSource source)
            : base(parent, name, source)
        {
            source.Peek().ThrowOnNull(Error.MissingBlock(name));
            Output = new InnerExprBuilder(this , parent.Output);
            Stack.CopyFrom(parent.Stack.Content);
        }

        public override IExprBuilder Output { get; }

        internal override void Return(ICompileContext subctx)
            => (Parent as CompileContext).Return(subctx);

        internal override void Break(ICompileContext subctx)
            => EmitCode(subctx, "break;");

        internal override void Continue(ICompileContext subctx)
            => EmitCode(subctx, "continue;");

        private void EmitCode(ICompileContext subctx, string code)
        {
            subctx.Stack.ForceLocalize();
            subctx.Stack.AdjustTo((Parent as CompileContext).Stack);
            subctx.Stack.MarkAsUnused();
            subctx.Output.RawEmit(code);
        }
    }
}
