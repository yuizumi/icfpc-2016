namespace NFlat.Micro
{
    internal class ParamsContext : CompileContext
    {
        internal ParamsContext(IContext parent, string name,
                               IParsedSource source)
            : base(parent, name, source)
        {
            source.Peek().ThrowOnNull(Error.MissingBlock(name));
        }

        public override IExprBuilder Output
        {
            get { throw new NFlatBugException(); }
        }

        public override void Compile()
        {
            try {
                while (Source.Peek() != null) {
                    Resolve(Source.Next()).Compile(this);
                }
            } catch (NFlatException e) {
                throw new NFlatLineNumberedException(e, Source.FileName, Source.LineNumber);
            }
        }

        private ICommand Resolve(ICommand command)
        {
            if (command is CliTypeName) {
                return command;
            } else {
                ICommand resolved = (command as Identifier)?.Resolve(this);
                if (resolved is NFType) return resolved;
                throw Error.NotTypeCommand(command);
            }
        }

        internal override void Return(ICompileContext subctx) => Fail();
        internal override void Break(ICompileContext subctx) => Fail();
        internal override void Continue(ICompileContext subctx) => Fail();

        public override CSharpExpr GetSelf() { throw new NFlatBugException(); }

        private static void Fail() { throw new NFlatBugException(); }
    }
}
