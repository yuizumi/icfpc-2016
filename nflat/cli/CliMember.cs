using System;
using System.Reflection;

namespace NFlat.Micro
{
    internal abstract class CliMember
    {
        internal abstract MemberInfo MemberInfo { get; }
        internal abstract bool HasThis { get; }

        internal Type DeclaringType => MemberInfo.DeclaringType;
        internal string Name => MemberInfo.Name;

        public string Message => $"「{MemberInfo.FullName()}」";

        public override string ToString()
            => $"{GetType()}({MemberInfo.FullName()})";
    }
}
