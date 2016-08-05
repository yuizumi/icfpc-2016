using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliUnknown : ITypeMember
    {
        private readonly IEnumerable<MemberInfo> mMembers;

        internal CliUnknown(IEnumerable<MemberInfo> members)
        {
            mMembers = members;
        }

        public string Message => $"「{mMembers.First().FullName()}」";

        public void Compile(ICompileContext ctx)
        {
            throw Error.UnsupportedMember(this);
        }

        public ITypeMember NonStatic()
        {
            throw Error.UnsupportedMember(this);
        }

        public override string ToString()
            => $"{GetType()}({mMembers.First().FullName()})";
    }
}
