using System.Reflection;

namespace NFlat.Micro
{
    internal static class MemberInfoExtension
    {
        internal static string FullName(this MemberInfo member)
        {
            return $"{member.DeclaringType}.{member.Name}";
        }

        internal static bool HasThis(this FieldInfo field)
        {
            return !field.IsStatic;
        }

        internal static bool HasThis(this MethodInfo method)
        {
            return method.CallingConvention.HasFlag(CallingConventions.HasThis);
        }

        internal static bool HasThis(this PropertyInfo property)
        {
            return HasThis(property.GetMethod ?? property.SetMethod);
        }
    }
}
