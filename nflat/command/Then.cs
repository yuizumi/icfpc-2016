namespace NFlat.Micro
{
    internal class Then : ICompileCommand
    {
        public string Message => $"「{ThenKeyword._Text}」";

        public void Compile(ICompileContext ctx)
        {
            CSharpExpr condition = ctx.Stack.Pop().Get(typeof(bool));

            var ctxThen = ctx.CondContext(ThenKeyword._Text);
            ctxThen.Compile();
            var else_ = ctx.Source.Peek() as Else;
            if (else_ != null && else_.Then == this) {
                ctxThen.Stack.ForceLocalize();
                ctx.Source.Next();
                var ctxElse = ctx.CondContext(ElseKeyword._Text);
                ctxElse.Compile();
                ctxElse.Stack.AdjustTo(ctxThen.Stack);
                var codeThen = ctxThen.Output.Build();
                var codeElse = ctxElse.Output.Build();
                ctx.Output.RawEmit($"if ({condition.Code}) {codeThen} else {codeElse}");
                ctx.Stack.CopyFrom(ctxThen.Stack.Content);
            } else {
                ctxThen.Stack.AdjustTo(ctx.Stack);
                var codeThen = ctxThen.Output.Build();
                ctx.Output.RawEmit($"if ({condition.Code}) {codeThen}");
            }
        }

        public override string ToString() => $"Then(_{GetHashCode():x8})";
    }
}
