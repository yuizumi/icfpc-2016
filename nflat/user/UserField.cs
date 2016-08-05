using System;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

using FA = System.Reflection.FieldAttributes;

namespace NFlat.Micro
{
    internal class UserField : UserMember
    {
        private readonly FieldBuilder mField;

        internal UserField(UserType owner, Identifier name, Type type,
                           bool hasThis)
        {
            Owner = owner;
            Name = name;
            mField = owner.Builder.DefineField(
                name.CSharp, type, (hasThis) ? FA.Public : (FA.Public | FA.Static));
        }

        internal override UserType Owner { get; }
        private Identifier Name { get; }

        internal override bool HasThis => mField.HasThis();

        public override string Message => Name.Text;

        public override void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();
            var instance = UserHelper.GetThisObject(this, ctx);
            ctx.Stack.Push(new Field(instance, mField));
        }

        public override void Write(TextWriter writer)
        {
            string type = CSharpString.Type(mField.FieldType);
            writer.Write($"{Modifiers} {type} {mField.Name};\n");
        }
    }
}
