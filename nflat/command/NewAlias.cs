using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class NewAlias : Keyword, IDeclareCommand
    {
        internal const string _Text = "別名";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Declare(IDeclareContext ctx)
        {
            if (ctx.Stack.Count != 2) throw Error.InvalidUsage(this);

            IValue target = ctx.Stack.Pop();

            if (target is TypeValue) {
                AddAlias(ctx, (target as TypeValue  ).NFType);
                return;
            }
            if (target is AliasTarget) {
                AddAlias(ctx, (target as AliasTarget).Target);
                return;
            }
            throw Error.NotAliasible(target);
        }

        private void AddAlias(IDeclareContext ctx, ICommand target)
        {
            ctx.Bindings.Define(Pop<NewEntity>(ctx.Stack).Name, target);
        }

        private void AddAlias(IDeclareContext ctx, NFType nftype)
        {
            ctx.Bindings.Define(Pop<NewEntity>(ctx.Stack).Name, nftype);
            (new CliTypeContext(ctx, nftype, ctx.Source.NextBlock())).Compile();
        }
    }
}
