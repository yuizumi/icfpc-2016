using System.IO;

namespace NFlat.Micro
{
    internal interface IUserTypeMember : ITypeMember
    {
        void Write(TextWriter writer);
    }
}
