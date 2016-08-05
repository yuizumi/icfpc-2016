using System.Reflection;

namespace NFlat.Micro
{
    internal class CliMethod : CliMethodBase
    {
        private readonly MethodInfo mMethod;

        internal CliMethod(MethodInfo method)
        {
            mMethod = method;
        }

        internal override MemberInfo MemberInfo => mMethod;
        internal override bool HasThis => mMethod.HasThis();

        internal override ParameterInfo[] GetParameters()
            => mMethod.GetParameters();

        internal override void Compile(ICompileContext ctx)
        {
            ctx.Stack.ForceEvaluate();

            var instance = CliHelper.GetThisObject(this, ctx);
            var arguments = CliHelper.MakeArguments(this, ctx);
            var expr = new CSharpExpr(
                $"{instance.Code}.{mMethod.Name}({arguments})", mMethod.ReturnType);
            ctx.Output.Emit(expr);
        }
    }
}
