namespace NFlat.Micro
{
    internal class NameIt : Keyword, ICompileCommand
    {
        internal const string _Text = "名付ける";
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
            var dst = ctx.Output.MakeVariable(src.Type);
            ctx.Output.RawEmit($"{dst.Code} = {src.Get(src.Type)};");
            ctx.Bindings.Define(name, new NamedLocal(dst, name));
        }

        private NFlatException GetError(IValue target)
        {
            if (target is INamedValue)
                return Error.AlreadyDefined((target as INamedValue).Name);
            return Error.InvalidUsage(this);
        }
    }
}
