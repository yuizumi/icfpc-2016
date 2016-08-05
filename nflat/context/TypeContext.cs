using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    internal abstract class TypeContext : DeclareContext, IBindings
    {
        private readonly List<NFType> mImports = new List<NFType>();

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

        public override void Import(NFType nftype)
        {
            mImports.Add(nftype);
        }

        void IBindings.Define(Identifier name, ICommand command)
        {
            if (!(command is ITypeMember)) throw Error.NotTypeMember(command);
            NFType.AddMember(name, command as ITypeMember);
        }

        ICommand IResolver.Resolve(Identifier name)
        {
            return NFType.FindMember(name) ?? Parent.Bindings.Resolve(name) ??
                ResolveFromImports(name);
        }

        private ICommand ResolveFromImports(Identifier name)
        {
            var resolved = mImports.Select(nftype => nftype.FindMember(name))
                .Where(m => m != null).ToList();
            switch (resolved.Count) {
                case 0:
                    return null;
                case 1:
                    return resolved[0];
                default:
                    throw Error.MultipleMatchesImported(name);
            }
        }
    }
}
