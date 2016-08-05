using System;
using System.Linq;

namespace NFlat.Micro
{
    using static CSharpString;

    internal class ForRange : Keyword, ICompileCommand
    {
        private static readonly Type[] Types = { typeof(int), typeof(uint), typeof(long),
                                                 typeof(ulong) };

        internal const string _Text = "範囲の各数値";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            IValue max = ctx.Stack.Pop();
            IValue min = ctx.Stack.Pop();
            Type type = DecideType(min, max);

            CSharpExpr c = ctx.Output.LoopVariable(type);
            var ctxLoop = ctx.LoopContext(Text);
            ctxLoop.Stack.Push(new Local(c));
            ctxLoop.Compile();
            ctxLoop.Stack.AdjustTo(ctx.Stack);

            ctx.Output.RawEmit($"for ({Type(type)} {c} = {min.Get(type)}; " +
                               $"{c} <= {max.Get(type)}; ++{c}) " +
                               $"{ctxLoop.Output.Build()}");
        }

        private static Type DecideType(IValue min, IValue max)
        {
            return Types.FirstOrDefault(type => min.Has(type) && max.Has(type)).ThrowOnNull(
                Error.NotInteger(Types.Any(min.Has) ? min : max));
        }
    }
}
