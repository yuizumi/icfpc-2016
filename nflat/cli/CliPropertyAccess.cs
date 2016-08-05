using System.Reflection;

namespace NFlat.Micro
{
    internal class CliPropertyAccess : CliMember, ITypeMember
    {
        private readonly PropertyInfo mProperty;

        internal CliPropertyAccess(PropertyInfo property)
        {
            mProperty = property;
        }

        internal override MemberInfo MemberInfo => mProperty;
        internal override bool HasThis => mProperty.HasThis();

        public ITypeMember NonStatic()
        {
            if (!HasThis) throw Error.NotInstanceMember(this);
            return this;
        }

        public void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();
            var instance = CliHelper.GetThisObject(this, ctx);
            ctx.Stack.Push(new Property(instance, mProperty));
        }
    }
}
