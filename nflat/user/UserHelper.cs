using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal static class UserHelper
    {
        internal static CSharpExpr GetThisObject(UserMember member, ICompileContext ctx)
        {
            Type type = member.Owner.Type;
            return (member.HasThis)
                ? ctx.Stack.Pop().Get(type) : new CSharpExpr(CSharpString.Type(type), null);
        }

        internal static string MakeArguments(IEnumerable<CSharpExpr> parameters,
                                             ICompileContext ctx)
        {
            var arguments = parameters
                .Reverse().Select(param => ctx.Stack.Pop().Get(param.Type).Code).Reverse();
            return String.Join(", ", arguments);
        }
    }
}
