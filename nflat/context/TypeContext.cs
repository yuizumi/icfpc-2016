using System;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    internal abstract class TypeContext : DeclareContext, IBindings
    {
        protected TypeContext(IDeclareContext parent, NFType nftype,
                              IParsedSource source)
            : base(source)
        {
            Parent = parent;
            NFType = nftype;
        }

        public override IContext Parent { get; }

        public override string Name => NFType.Text;
        public override IBindings Bindings => this;
        public override TypePool TypePool => NFType.Owner;

        public override DeclareStack Stack { get; } = new DeclareStack();

        internal NFType NFType { get; }

        void IBindings.Define(Identifier name, ICommand command)
        {
            if (!(command is ITypeMember)) throw Error.NotTypeMember(command);
            NFType.AddMember(name, command as ITypeMember);
        }

        ICommand IResolver.Resolve(Identifier name)
        {
            return NFType.FindMember(name) ?? Parent.Bindings.Resolve(name);
        }
    }
}
