namespace NFlat.Micro
{
    internal class Assign : Keyword, ICompileCommand
    {
        internal const string _Text = "入れる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var dst = ctx.Stack.Pop();
            if (!dst.IsAssignable)
                throw Error.NotAssignable(dst);
            ctx.Stack.ForceEvaluate();
            var src = ctx.Stack.Pop();
            if (!src.Has(dst.Type))
                throw Error.NotAssignable(dst, src);

            ctx.Output.RawEmit(
                $"{dst.Get(dst.Type)} = {src.Get(dst.Type)};");
        }
    }
}
