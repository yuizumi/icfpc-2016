using System.IO;

namespace NFlat.Micro
{
    internal abstract class UserMember : IUserTypeMember
    {
        internal abstract UserType Owner { get; }
        internal abstract bool HasThis { get; }
        public abstract string Message { get; }

        protected string Modifiers => (HasThis) ? "public" : "public static";

        public abstract void Compile(ICompileContext ctx);
        public abstract void Write(TextWriter writer);

        public ITypeMember NonStatic()
        {
            if (!HasThis) throw Error.NotInstanceMember(this);
            return this;
        }

        public override string ToString() => $"{GetType().Name}({Message})";
    }
}
