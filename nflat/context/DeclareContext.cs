using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using TA = System.Reflection.TypeAttributes;

namespace NFlat.Micro
{
    internal abstract class DeclareContext : IDeclareContext
    {
        private readonly List<UserType> mUserTypes = new List<UserType>();

        protected DeclareContext(IParsedSource source)
        {
            Source = source;
        }

        public abstract IContext Parent { get; }
        public abstract string Name { get; }
        public abstract IBindings Bindings { get; }
        public abstract TypePool  TypePool { get; }
        public abstract DeclareStack Stack { get; }

        public IParsedSource Source { get; }

        internal IReadOnlyList<UserType> UserTypes
            => mUserTypes.AsReadOnly();

        public void Compile()
        {
            try {
                while (Source.Peek() != null)
                    Source.Next().Declare(this);
            } catch (NFlatException e) {
                throw new NFlatLineNumberedException(e, Source.FileName, Source.LineNumber);
            }
        }

        public void CompileToClass(Identifier name)
            => CompileToType(name, TA.Public, typeof(object));

        public void CompileToStruct(Identifier name)
            => CompileToType(name, TA.Public, typeof(ValueType));

        public void CompileToModule(Identifier name)
            => CompileToType(name, TA.Public | TA.Abstract | TA.Sealed, typeof(object));

        public abstract void CompileToField(Identifier name, Type type, bool hasThis);
        public abstract void CompileToMethod(Identifier name, bool hasThis);

        protected abstract TypeBuilder GetTypeBuilder(string name, TypeAttributes flags,
                                                      Type baseType);

        private void CompileToType(Identifier name, TypeAttributes flags, Type baseType)
        {
            TypeBuilder builder = GetTypeBuilder(name.Text, flags, baseType);
            var userType = new UserType(TypePool, builder);
            Bindings.Define(name, userType);
            mUserTypes.Add(userType);
            (new UserTypeContext(this, userType, Source.NextBlock())).Compile();
        }

        public override string ToString() => $"{GetType().Name}({Name})";
    }
}
