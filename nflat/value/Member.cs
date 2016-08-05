using System;
using System.Reflection;

namespace NFlat.Micro
{
    internal abstract class Member<TMemberInfo> : Value
        where TMemberInfo : MemberInfo
    {
        internal Member(CSharpExpr instance, TMemberInfo member)
        {
            Instance = instance;
            MemberInfo = member;
        }

        protected CSharpExpr Instance { get; }
        protected TMemberInfo MemberInfo { get; }

        protected override string Code => $"{Instance.Code}.{MemberInfo.Name}";

        public override bool IsStable => DeclaringType.IsValueType;
        public override string Message => MemberInfo.FullName();

        private Type DeclaringType => MemberInfo.DeclaringType;
    }
}
