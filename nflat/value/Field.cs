using System;
using System.Reflection;

namespace NFlat.Micro
{
    internal sealed class Field : Member<FieldInfo>
    {
        internal Field(CSharpExpr instance, FieldInfo field)
            : base(instance, field)
        {
        }

        public override Type Type => MemberInfo.FieldType;
        public override bool IsAssignable => !MemberInfo.IsInitOnly;
    }
}
