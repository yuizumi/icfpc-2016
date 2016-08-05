using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class Bindings : IBindings
    {
        private readonly Dictionary<Identifier, ICommand> mBindings
            = new Dictionary<Identifier, ICommand>();

        internal Bindings(IBindings parent)
        {
            Parent = parent;
        }

        internal IBindings Parent { get; }

        public void Define(Identifier name, ICommand command)
        {
            if (mBindings.ContainsKey(name)) throw Error.AlreadyDefined(name);
            mBindings.Add(name, command);
        }

        public ICommand Resolve(Identifier name)
        {
            return mBindings.Get(name) ?? Parent?.Resolve(name);
        }
    }
}
