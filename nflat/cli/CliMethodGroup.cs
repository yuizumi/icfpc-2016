using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliMethodGroup : ITypeMember
    {
        protected CliMethodGroup(IEnumerable<CliMethodBase> methods)
        {
            Methods = methods;
        }

        protected IEnumerable<CliMethodBase> Methods { get; }
        public virtual string Message => Methods.First().Message;

        internal static CliMethodGroup Create(
            IEnumerable<ConstructorInfo> members)
        {
            return new CliMethodGroup(
                members.Select(c => new CliConstructor(c)).ToList().AsReadOnly());
        }

        internal static CliMethodGroup Create(
            IEnumerable<MethodInfo> members)
        {
            return new CliMethodGroup(
                members.Select(m => new CliMethod(m)).ToList().AsReadOnly());
        }

        internal virtual CliMethodGroup WithArity(int arity)
            => new ConstrainedArity(this, arity);

        public ITypeMember NonStatic()
        {
            var methods = Methods.Where(m => m.HasThis);
            if (!methods.Any()) throw Error.NotInstanceMember(this);
            return new CliMethodGroup(methods);
        }

        public void Compile(ICompileContext ctx)
        {
            CliMethodBase method = Resolve(m => m.HasThis, new StackArguments(ctx.Stack, 1))
                ?? Resolve(m => !m.HasThis, new StackArguments(ctx.Stack, 0));
            method.ThrowOnNull(Error.ArgumentsMismatch(this)).Compile(ctx);
        }

        private CliMethodBase Resolve(Func<CliMethodBase, bool> filter,
                                      StackArguments arguments)
        {
            return CliHelper.Resolve(Methods.Where(filter), arguments);
        }

        private class Constrained : CliMethodGroup
        {
            internal Constrained(IEnumerable<CliMethodBase> members) : base(members)
            {
            }

            internal override CliMethodGroup WithArity(int arity)
            {
                throw Error.MultipleConstraints();
            }
        }

        private class ConstrainedArity : Constrained
        {
            internal ConstrainedArity(CliMethodGroup baseGroup, int arity)
                : base(baseGroup.Methods.Where(m => m.GetArity() == arity))
            {
                if (!Methods.Any()) throw Error.NoOverloadsWithArity(baseGroup, arity);
                Arity = arity;
            }

            internal int Arity { get; }

            public override string Message
                => $"「{Methods.First().MemberInfo.FullName()}（{Arity} 引数）」";
        }

        public override string ToString()
            => $"CliMethodGroup({Methods.First().MemberInfo.FullName()})";
    }
}

