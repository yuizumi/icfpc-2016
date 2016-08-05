using System.Reflection;

namespace NFlat.Micro
{
    internal class CliFieldAccess : CliMember, ITypeMember
    {
        private readonly FieldInfo mField;

        internal CliFieldAccess(FieldInfo field)
        {
            mField = field;
        }

        internal override MemberInfo MemberInfo => mField;
        internal override bool HasThis => mField.HasThis();

        public ITypeMember NonStatic()
        {
            if (!HasThis) throw Error.NotInstanceMember(this);
            return this;
        }

        public void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();
            var instance = CliHelper.GetThisObject(this, ctx);
            ctx.Stack.Push(new Field(instance, mField));
        }
    }
}
