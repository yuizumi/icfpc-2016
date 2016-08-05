using System;
using System.Linq;

namespace NFlat.Micro
{
    using static CSharpString;

    internal class Times : Keyword, ICompileCommand
    {
        private static readonly Type[] Types = { typeof(int), typeof(uint), typeof(long),
                                                 typeof(ulong) };

        internal const string _Text = "回数だけ繰り返す";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var times = ctx.Stack.Pop();
            Type type = DecideType(times);

            CSharpExpr c = ctx.Output.LoopVariable(type);
            var ctxLoop = ctx.LoopContext(Text);
            ctxLoop.Compile();
            ctxLoop.Stack.AdjustTo(ctx.Stack);

            ctx.Output.RawEmit($"for ({Type(type)} {c} = 0; {c} < {times.Get(type)}; ++{c}) " +
                               $"{ctxLoop.Output.Build()}");
        }

        private static Type DecideType(IValue times)
        {
            return Types.FirstOrDefault(times.Has).ThrowOnNull(Error.NotInteger(times));
        }
    }
}
