namespace NFlat.Micro
{
    internal class AssignNew : Keyword, ICompileCommand
    {
        internal const string _Text = "新しい変数に入れる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        internal override bool ConsumesUndefined => true;

        public void Compile(ICompileContext ctx)
        {
            var target = ctx.Stack.Pop();

            if (!(target is Undefined)) throw GetError(target);

            var name = (target as Undefined).Name;
            ctx.Stack.ForceEvaluate();
            var src = ctx.Stack.Pop();
            var dst = new CSharpExpr(name.CSharp, src.Type);
            ctx.Output.RawEmit($"{CSharpString.Type(src.Type)} {dst.Code};");
            ctx.Output.RawEmit($"{dst.Code} = {src.Get(src.Type)};");
            ctx.Bindings.Define(name, new Variable(dst, name));
        }

        private NFlatException GetError(IValue target)
        {
            if (target is Variable)
                return Error.AlreadyDefined((target as Variable).Name);
            return Error.InvalidUsage(this);
        }
    }
}
