namespace NFlat.Micro
{
    internal class CondContext : CompileContext
    {
        internal CondContext(CompileContext parent, string name,
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
            => (Parent as CompileContext).Break(subctx);

        internal override void Continue(ICompileContext subctx)
            => (Parent as CompileContext).Continue(subctx);
    }
}
