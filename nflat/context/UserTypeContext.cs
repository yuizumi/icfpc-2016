using System;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    internal class UserTypeContext : TypeContext
    {
        internal UserTypeContext(IDeclareContext parent, UserType userType,
                                 IParsedSource source)
            : base(parent, userType, source)
        {
        }

        private UserType UserType => NFType as UserType;

        public override void CompileToField(Identifier name, Type type,
                                            bool hasThis)
        {
            NFType.AddMember(name, new UserField(UserType, name, type, hasThis));
        }

        public override void CompileToMethod(Identifier name, bool hasThis)
        {
            var ctxMethod = new MethodContext(this, name, Source.NextBlock());
            ctxMethod.Compile();
            ctxMethod.Return();
            NFType.AddMember(name, new UserMethod(ctxMethod, hasThis));
        }

        protected override TypeBuilder GetTypeBuilder(string name, Type baseType)
            => UserType.GetTypeBuilder(name, baseType);
    }
}
