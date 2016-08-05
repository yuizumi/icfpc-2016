using System.Reflection;

namespace NFlat.Micro
{
    internal class CliConstructor : CliMethodBase
    {
        private readonly ConstructorInfo mConstructor;

        internal CliConstructor(ConstructorInfo ctor)
        {
            mConstructor = ctor;
        }

        internal override MemberInfo MemberInfo => mConstructor;
        internal override bool HasThis => false;

        internal override ParameterInfo[] GetParameters()
            => mConstructor.GetParameters();

        internal override void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();

            var arguments = CliHelper.MakeArguments(this, ctx);
            var expr = new CSharpExpr(
                $"new {CSharpString.Type(DeclaringType)}({arguments})", DeclaringType);
            ctx.Output.Emit(expr);
        }
    }
}
