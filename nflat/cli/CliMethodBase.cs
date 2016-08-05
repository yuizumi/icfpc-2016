namespace NFlat.Micro
{
    internal abstract class CliMethodBase : CliGroupMember
    {
        internal abstract void Compile(ICompileContext ctx);
    }
}
