using System;
using System.Reflection;

namespace NFlat.Micro
{
    internal sealed class Property : Member<PropertyInfo>
    {
        internal Property(CSharpExpr instance, PropertyInfo property)
            : base(instance, property)
        {
        }

        public override Type Type => MemberInfo.PropertyType;
        public override bool IsAssignable => MemberInfo.CanWrite;
    }
}
