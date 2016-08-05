using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    using static CSharpString;

    internal class ForEach : Keyword, ICompileCommand
    {
        internal const string _Text = "各要素";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            IValue seq = ctx.Stack.Pop();

            Type seqType = seq.Type.GetInterface("IEnumerable`1")
                ?? seq.Type.GetInterface("IEnumerable");
            seqType.ThrowOnNull(Error.NotIEnumerable(seq.Type));
            Type itemType = (seqType.IsGenericType)
                ? seqType.GetGenericArguments()[0] : typeof(object);

            CSharpExpr e = ctx.Output.LoopVariable(itemType);
            var ctxLoop = ctx.LoopContext(Text);
            ctxLoop.Stack.Push(new Local(e));
            ctxLoop.Compile();
            ctxLoop.Stack.AdjustTo(ctx.Stack);

            ctx.Output.RawEmit($"foreach ({Type(itemType)} {e} in {seq.Get(seqType)}) " +
                               $"{ctxLoop.Output.Build()}");
        }
    }
}
