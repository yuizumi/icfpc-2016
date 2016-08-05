using System;

namespace NFlat.Micro
{
    internal class DefaultCtor : ITypeMember
    {
        private readonly Type mType;

        internal DefaultCtor(Type type)
        {
            mType = type;
        }

        public string Message => "（初期化）";

        public void Compile(ICompileContext ctx)
        {
            var expr = new CSharpExpr($"new {CSharpString.Type(mType)}()", mType);
            ctx.Output.Emit(expr);
        }

        public ITypeMember NonStatic()
        {
            throw Error.NotInstanceMember(this);
        }

        public override string ToString() => $"DefaultCtor({mType})";
    }
}
