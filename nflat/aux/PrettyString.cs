namespace NFlat.Micro
{
    internal static class PrettyString
    {
        internal static string ToPrettyString(this object obj)
        {
            string str = obj?.ToString() ?? "null";
            return (obj is string) ? CSharpString.Primitive(str) : str;
        }
    }
}
