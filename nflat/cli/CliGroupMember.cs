using System.Reflection;

namespace NFlat.Micro
{
    internal abstract class CliGroupMember : CliMember
    {
        internal abstract ParameterInfo[] GetParameters();
        internal int GetArity() => GetParameters().Length;
    }
}
