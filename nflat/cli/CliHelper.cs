using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal static class CliHelper
    {
        internal static TMember Resolve<TMember>(IEnumerable<TMember> members,
                                                 CliArguments args)
            where TMember : CliGroupMember
        {
            var matches = members.Where(m => IsApplicable(m, args)).ToList();
            if (matches.Count == 0)
                return null;
            if (matches.Any(m => m.GetArity() != matches[0].GetArity()))
                throw Error.AmbiguousOverloads(members.First());
            var argsList = args.Get(matches[0].GetArity()).ToList();
            TMember resolved = matches.SingleOrDefault(
                m1 => matches.All(m2 => (m1 == m2) || IsBetterMember(m1, m2, argsList)));
            return resolved.ThrowOnNull(Error.AmbiguousOverloads(members.First()));
        }

        private static bool IsApplicable(CliGroupMember member,
                                         CliArguments args)
        {
            int arity = member.GetArity();
            if (!args.Has(arity))
                return false;
            return member.GetParameters().Zip(args.Get(arity), (param, arg) => {
                return arg.Has(param.ParameterType);
            }).All(matched => matched);
        }

        private static bool IsBetterMember(CliGroupMember thisMember,
                                           CliGroupMember thatMember,
                                           IReadOnlyList<IValue> args)
        {
            ParameterInfo[] thisParams = thisMember.GetParameters();
            ParameterInfo[] thatParams = thatMember.GetParameters();
            bool outcome = false;
            for (int i = 0; i < args.Count; i++) {
                Type thisType = thisParams[i].ParameterType;
                Type thatType = thatParams[i].ParameterType;
                if (thisType == args[i].Type) {
                    outcome |= (thatType != args[i].Type);
                } else {
                    if (thatType == args[i].Type || IsBetterTarget(thatType, thisType)) {
                        return false;
                    }
                    outcome |= IsBetterTarget(thisType, thatType);
                }
            }
            return outcome;
        }

        private static bool IsBetterTarget(Type type1, Type type2)
        {
            if (type1.HasImplicitTo(type2) && !type2.HasImplicitTo(type1)) {
                return true;
            }

            if (type1.IsPrimitive && type2.IsPrimitive) {
                switch (Type.GetTypeCode(type1)) {
                    case TypeCode.SByte:
                        if (Type.GetTypeCode(type2) == TypeCode.Byte  ) return true;
                        goto case TypeCode.Int16;
                    case TypeCode.Int16:
                        if (Type.GetTypeCode(type2) == TypeCode.UInt16) return true;
                        goto case TypeCode.Int32;
                    case TypeCode.Int32:
                        if (Type.GetTypeCode(type2) == TypeCode.UInt32) return true;
                        goto case TypeCode.Int64;
                    case TypeCode.Int64:
                        if (Type.GetTypeCode(type2) == TypeCode.UInt64) return true;
                        break;
                }
            }

            return false;
        }

        internal static CSharpExpr GetThisObject(CliMember member, ICompileContext ctx)
        {
            Type type = member.DeclaringType;
            return (member.HasThis)
                ? ctx.Stack.Pop().Get(type) : new CSharpExpr(CSharpString.Type(type), null);
        }

        internal static string MakeArguments(CliGroupMember member, ICompileContext ctx)
        {
            var args = member.GetParameters()
                .Reverse().Select(p => ctx.Stack.Pop().Get(p.ParameterType).Code).Reverse();
            return String.Join(", ", args);
        }
    }
}
