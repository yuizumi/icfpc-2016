using System;
using System.IO;
using System.Linq;

namespace NFlat.Micro
{
    internal class UserMethod : UserMember
    {
        internal UserMethod(MethodContext context, bool hasThis)
        {
            Context = context;
            HasThis = hasThis;
        }

        private MethodContext Context { get; }

        internal override UserType Owner
        {
            get {
                return (Context.Parent as TypeContext).NFType as UserType;
            }
        }

        internal override bool HasThis { get; }

        public override string Message => Context.Name;

        private string CSharpName() => Context.Identifier.CSharp;

        public override void Compile(ICompileContext ctxCallee)
        {
            var instance = UserHelper.GetThisObject(this, ctxCallee);
            var arguments = UserHelper.MakeArguments(Context.Parameters, ctxCallee);
            var expr = new CSharpExpr(
                $"{instance.Code}.{CSharpName()}({arguments})", Context.ReturnType);
            ctxCallee.Output.Emit(expr);
        }

        public override void Write(TextWriter writer)
        {
            string returnType = CSharpString.Type(Context.ReturnType);
            var parameters = Context.Parameters.Select(
                p => $"{CSharpString.Type(p.Type)} {p.Code}");
            writer.Write($"{Modifiers} {returnType} {CSharpName()}(");
            writer.Write(String.Join(", ", parameters) + ") ");
            writer.Write(Context.Output.Build());
            writer.Write($" // {CSharpName()}\n");
        }
    }
}
