namespace NFlat.Micro
{
    internal abstract class CompileContext : ICompileContext
    {
        protected CompileContext(IContext parent, string name,
                                 IParsedSource source)
        {
            Parent = parent;
            Name = name;
            Source = source;
            Bindings = new Bindings(parent.Bindings);
            TypePool = parent.TypePool;

            Stack = new CompileStack(this);
        }

        public IContext Parent { get; }
        public string Name { get; }
        public IBindings Bindings { get; }
        public TypePool TypePool { get; }
        public IParsedSource Source { get; }
        public CompileStack Stack { get; }

        public abstract IExprBuilder Output { get; }

        public virtual void Compile()
        {
            try {
                while (Source.Peek() != null)
                    Source.Next().Compile(this);
            } catch (NFlatException e) {
                throw new NFlatLineNumberedException(e, Source.LineNumber);
            }
        }

        public void Return() => Return(this);
        public void Break() => Break(this);
        public void Continue() => Continue(this);

        internal abstract void Return(ICompileContext subctx);
        internal abstract void Break(ICompileContext subctx);
        internal abstract void Continue(ICompileContext subctx);

        public ICompileContext CondContext(string name)
        {
            Stack.ForceLocalize();
            return new CondContext(this, name, Source.NextBlock());
        }

        public ICompileContext LoopContext(string name)
        {
            Stack.ForceLocalize();
            return new LoopContext(this, name, Source.NextBlock());
        }

        public override string ToString() => $"{GetType().Name}({Name})";
    }
}
