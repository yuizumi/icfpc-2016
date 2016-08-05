using System;

namespace NFlat.Micro
{
    internal class InitialValue : Directive, ICommand
    {
        internal const string _Text = "初期値";
        internal override string Text => _Text;

        internal CSharpExpr Compile(ICompileContext ctx, Type type)
        {
            var ctxValue = ctx.InitContext(Text);
            ctxValue.Compile();
            if (ctxValue.Stack.Count != 1) throw Error.InvalidInitialValue();
            return ctxValue.Stack.Pop().Get(type);
        }

        internal override ICommand Parse() => this;
    }
}
