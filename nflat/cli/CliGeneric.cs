using System;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliGeneric : ITypeMember
    {
        private readonly MemberInfo mMember;

        internal CliGeneric(MemberInfo member)
        {
            mMember = member;
            Name = Identifier.Of(mMember.Name);
        }

        public string Message => $"「{mMember.FullName()}」";
        internal Identifier Name { get; }

        public void Compile(ICompileContext ctx)
        {
            throw Error.IncompleteType(mMember.DeclaringType);
        }

        public ITypeMember NonStatic()
        {
            throw Error.IncompleteType(mMember.DeclaringType);
        }

        public override string ToString()
            => $"CliGeneric({mMember.FullName()})";
    }
}
