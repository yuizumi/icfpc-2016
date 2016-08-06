using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class Index : ICompileCommand
    {
        private readonly IReadOnlyList<ICommand> mArgs;

        internal Index(IEnumerable<ICommand> args)
        {
            mArgs = args.ToList().AsReadOnly();
        }

        public string Message => "添字";

        public void Compile(ICompileContext ctx)
        {
            var value = ctx.Stack.Pop();
            var indexer = (ctx.TypePool.Get(value.Type) as IIndexer)
                .ThrowOnNull(Error.NotIndexer(value));
            var args = new FixedArguments(
                mArgs.Select(x => Resolve(ctx, x)).ToList().AsReadOnly());
            ctx.Stack.Push(indexer.GetForIndex(value, args));
        }

        private IValue Resolve(ICompileContext ctx, ICommand command)
        {
            if (command is Identifier) {
                command = (command as Identifier).Resolve(ctx);
            }
            return (command as IValue)
                .ThrowOnNull(Error.InvalidIndexArgument(command));
        }
    }
}
