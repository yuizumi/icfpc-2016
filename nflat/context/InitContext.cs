namespace NFlat.Micro
{
    internal class InitContext : CompileContext
    {
        internal InitContext(CompileContext parent, string name,
                             IParsedSource source)
            : base(parent, name, source)
        {
            source.Peek().ThrowOnNull(Error.MissingBlock(name));
            Output = parent.Output;
        }

        public override IExprBuilder Output { get; }

        internal override void Return(ICompileContext subctx)
        {
            throw Error.InvalidCall(this, new ExitMethod());
        }

        internal override void Break(ICompileContext subctx)
        {
            throw Error.InvalidCall(this, new Break());
        }

        internal override void Continue(ICompileContext subctx)
        {
            throw Error.InvalidCall(this, new Continue());
        }
    }
}
