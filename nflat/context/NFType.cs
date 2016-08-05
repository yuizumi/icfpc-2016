using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal abstract class NFType : ITypeMember, IDeclareCommand
    {
        internal abstract TypePool Owner { get; }
        internal abstract Type Type { get; }
        internal abstract NFType BaseType { get; }

        protected Dictionary<Identifier, ITypeMember> Members { get; }
            = new Dictionary<Identifier, ITypeMember>();

        internal abstract string Text { get; }
        public string Message => Text;

        public void Compile(ICompileContext ctx)
            => Process(ctx, ctx.Stack);

        public void Declare(IDeclareContext ctx)
            => Process(ctx, ctx.Stack);

        private void Process(IContext ctx, NFStack stack)
        {
            NFType nftype = this;

            if (Type.IsGenericTypeDefinition) {
                int arity = Type.GetGenericArguments().Length;
                var types = new Type[arity];
                for (int i = arity - 1; i >= 0; i--) {
                    IValue value = stack.Pop();
                    types[i] = (value as TypeValue)
                        .ThrowOnNull(Error.InvalidTypeArgument(value)).NFType.Type;
                }
                nftype = ctx.TypePool.Get(Type.MakeGenericType(types));
            }

            stack.Push(new TypeValue(nftype));
        }

        public ITypeMember NonStatic()
        {
            throw Error.NotInstanceMember(this);
        }

        public void AddMember(Identifier name, ITypeMember member)
        {
            if (Members.ContainsKey(name)) throw Error.AlreadyDefined(name);
            Members.Add(name, member);
        }

        public ITypeMember GetMember(Identifier name)
        {
            return FindMember(name).ThrowOnNull(Error.MemberNotFound(name));
        }

        public ITypeMember GetMember(MethodSpec spec)
        {
            CliMethodGroup methods = (GetMember(spec.Name) as CliMethodGroup)
                .ThrowOnNull(Error.NotMethod(spec.Name));
            return methods.WithArity(spec.Arity);
        }

        public virtual ITypeMember FindMember(Identifier name)
        {
            ITypeMember member = null;

            if (Members.TryGetValue(name, out member)) {
                return member;
            }
            if (Type.IsConstructedGenericType) {
                var genType = Owner.Get(Type.GetGenericTypeDefinition());
                var genMember = genType.Members.Get(name) as CliGeneric;
                if (genMember != null) return FindMember(genMember.Name);
            }
            var interfaces = Type.GetInterfaces();
            member = interfaces.Except(interfaces.SelectMany(i => i.GetInterfaces()))
                .Select(i => Owner.Get(i).FindMember(name))
                .FirstOrDefault(found => found != null);
            return member ?? BaseType?.FindMember(name);
        }
    }
}
