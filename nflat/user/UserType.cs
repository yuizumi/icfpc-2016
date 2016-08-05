using System;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

using FA = System.Reflection.FieldAttributes;
using TA = System.Reflection.TypeAttributes;

namespace NFlat.Micro
{
    internal class UserType : NFType, IUserTypeMember
    {
        internal UserType(TypePool owner, TypeBuilder builder)
        {
            owner.Add(builder, this);
            Owner = owner;
            Builder = builder;
            BaseType = owner.Get(builder.BaseType);
        }

        internal override TypePool Owner { get; }
        internal override Type Type => Builder;
        internal override NFType BaseType { get; }

        internal override string Text => Builder.Name;

        internal TypeBuilder Builder { get; }

        private string Keyword
        {
            get {
                if (Type.IsEnum) return "enum";
                if (Type.IsClass) return "class";
                if (Type.IsInterface) return "interface";
                if (Type.IsValueType) return "struct";

                throw new NFlatBugException();
            }
        }

        public override ITypeMember FindMember(Identifier name)
        {
            if (name == MakeObject.CtorName)
                return Members.Get(name) ?? (new DefaultCtor(Type));
            return base.FindMember(name);
        }

        internal TypeBuilder GetTypeBuilder(string name, Type baseType)
            => Builder.DefineNestedType(name, TA.Public, baseType);

        public void Write(TextWriter writer)
        {
            writer.Write($"public {Keyword} {Builder.Name} {{\n");
            foreach (var member in Members.Values.OfType<IUserTypeMember>()) {
                member.Write(writer);
            }
            writer.Write($"}} // {Builder.Name}\n");
        }

        public override string ToString() => $"UserType({Builder})";
    }
}
