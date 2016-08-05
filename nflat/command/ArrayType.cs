namespace NFlat.Micro
{
    internal class ArrayType : ArrayKeyword, ICompileCommand, IDeclareCommand
    {
        internal const string _Text = "配列";

        private readonly int mDimension;

        private ArrayType(int dimension)
        {
            mDimension = dimension;
        }

        public ArrayType() : this(1)
        {
        }

        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
            => Process(ctx, ctx.Stack);

        public void Declare(IDeclareContext ctx)
            => Process(ctx, ctx.Stack);

        internal override Stem WithDimension(int dimension)
            => new ArrayType(dimension);

        private void Process(IContext ctx, NFStack stack)
        {
            var arrayType = Pop<TypeValue>(stack).NFType.Type.MakeArrayType(mDimension);
            stack.Push(new TypeValue(ctx.TypePool.Get(arrayType)));
        }
    }
}
