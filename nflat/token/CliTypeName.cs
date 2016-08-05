using System;
using System.Collections.Concurrent;

namespace NFlat.Micro
{
    using CliTypeNameMap = ConcurrentDictionary<string, CliTypeName>;

    internal sealed class CliTypeName : Stem, ICompileCommand, IDeclareCommand
    {
        private static readonly CliTypeNameMap CliTypeNames =
            new CliTypeNameMap();

        private CliTypeName(string name)
        {
            Name = name;
        }

        internal override string Text => $"|T:{Name}|";
        internal string Name { get; }

        public string Message => Text;

        internal static CliTypeName Of(string name)
        {
            return CliTypeNames.GetOrAdd(name, name_ => new CliTypeName(name_));
        }

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(GetTypeValue(ctx));

        public void Declare(IDeclareContext ctx)
            => ctx.Stack.Push(GetTypeValue(ctx));

        private TypeValue GetTypeValue(IContext ctx)
        {
            var type = Type.GetType(Name).ThrowOnNull(Error.TypeNotFound(Name));
            return new TypeValue(ctx.TypePool.Get(type));
        }
    }
}
