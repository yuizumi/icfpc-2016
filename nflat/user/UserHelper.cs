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
            if (!member.HasThis) {
                return new CSharpExpr(CSharpString.Type(type), null);
            }

            if (ctx.Stack.Count > 0 && ctx.Stack.Peek().Has(type)) {
                return ctx.Stack.Pop().Get(type);
            } else {
                CSharpExpr self = ctx.GetSelf();
                if (self.Code == null) throw Error.InstanceMemberFromStatic();
                ErrorHelper.Ensure(type == self.Type);
                return self;
            }
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
