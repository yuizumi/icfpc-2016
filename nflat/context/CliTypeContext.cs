using System;
using System.Reflection;
using System.Reflection.Emit;

namespace NFlat.Micro
{
    internal class CliTypeContext : TypeContext
    {
        internal CliTypeContext(IDeclareContext parent, NFType nftype,
                                IParsedSource source)
            : base(parent, nftype, source)
        {
        }

        public override void CompileToField(Identifier name, Type type, bool hasThis)
        {
            throw Error.NewMemberToExistingType();
        }

        public override void CompileToMethod(Identifier name, bool hasThis)
        {
            throw Error.NewMemberToExistingType();
        }

        protected override TypeBuilder GetTypeBuilder(string name, TypeAttributes flags,
                                                      Type baseType)
        {
            throw Error.NewMemberToExistingType();
        }
    }
}
