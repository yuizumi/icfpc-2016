using System;
using System.Reflection.Emit;

using TA = System.Reflection.TypeAttributes;

namespace NFlat.Micro
{
    internal class RootContext : DeclareContext
    {
        private readonly ModuleBuilder mModule;

        internal RootContext(IParsedSource source, ModuleBuilder module)
            : base(source)
        {
            mModule = module;
        }

        public override IContext Parent => null;
        public override string Name => mModule.Name;

        public override IBindings Bindings { get; } = new Bindings(null);
        public override TypePool  TypePool { get; } = new TypePool();
        public override DeclareStack Stack { get; } = new DeclareStack();

        public override void CompileToField(Identifier name, Type type,
                                            bool hasThis)
        {
            throw Error.MemberDefinedOutsideType();
        }

        public override void CompileToMethod(Identifier name, bool hasThis)
        {
            throw Error.MemberDefinedOutsideType();
        }

        protected override TypeBuilder GetTypeBuilder(string name, Type baseType)
            => mModule.DefineType(name, TA.Public, baseType);
    }
}
