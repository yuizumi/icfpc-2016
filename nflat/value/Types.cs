using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using BF = System.Reflection.BindingFlags;

namespace NFlat.Micro
{
    internal static class Types
    {
        private static readonly IReadOnlyDictionary<Type, IReadOnlyCollection<Type>>
            NumericTable = BuildNumericTable().AsReadOnly();

        private static Dictionary<Type, IReadOnlyCollection<Type>> BuildNumericTable()
        {
            Type _sbyte = typeof(sbyte);
            Type _byte = typeof(byte);
            Type _short = typeof(short);
            Type _ushort = typeof(ushort);
            Type _int = typeof(int);
            Type _uint = typeof(uint);
            Type _long = typeof(long);
            Type _ulong = typeof(ulong);
            Type _char = typeof(char);
            Type _float = typeof(float);
            Type _double = typeof(double);
            Type _decimal = typeof(decimal);

            return new Dictionary<Type, IReadOnlyCollection<Type>>() {
                [_sbyte  ] = new [] {_short, _int, _long, _float, _double, _decimal},
                [_byte   ] = new [] {_short, _ushort, _int, _uint, _long, _ulong,
                                     _float, _double, _decimal},
                [_short  ] = new [] {_int, _long, _float, _double, _decimal},
                [_ushort ] = new [] {_int, _uint, _long, _ulong, _float, _double, _decimal},
                [_int    ] = new [] {_long, _float, _double, _decimal},
                [_uint   ] = new [] {_long, _ulong, _float, _double, _decimal},
                [_long   ] = new [] {_float, _double, _decimal},
                [_ulong  ] = new [] {_float, _double, _decimal},
                [_char   ] = new [] {_ushort, _int, _uint, _long, _ulong, _float, _double,
                                     _decimal},
                [_float  ] = new [] {_double},
            };
        }

        internal static bool HasImplicitTo(this Type source, Type target)
        {
            if (HasStandardTo(source, target)) {
                return true;
            }
            Type source0 = (source.IsGenericOf(typeof(Nullable<>)))
                ? source.GenericTypeArguments[0] : source;
            Type target0 = (target.IsGenericOf(typeof(Nullable<>)))
                ? target.GenericTypeArguments[0] : target;
            if (target != target0 && source.HasImplicitTo(target0)) {
                return true;
            }
            if (target != target0 && source0.HasImplicitTo(target0)) {
                return true;
            }
            return FindUserDefined(source0).Concat(FindUserDefined(target0)).Any(op => {
                Type opSource = op.GetParameters()[0].ParameterType;
                Type opTarget = op.ReturnType;
                return HasStandardTo(source, opSource) && HasStandardTo(opTarget, target);
            });
        }

        private static IEnumerable<MethodInfo> FindUserDefined(Type type)
        {
            return type.GetMethods(BF.Public | BF.Static | BF.FlattenHierarchy)
                .Where(method => method.Name == "op_Implicit");
        }

        private static bool HasStandardTo(Type source, Type target)
        {
            return target.IsAssignableFrom(source)
                || NumericTable.Get(source, Type.EmptyTypes).Contains(target);
        }

        internal static bool IsGenericOf(this Type type, Type genericType)
        {
            return type.IsGenericType && (type.GetGenericTypeDefinition() == genericType);
        }
    }
}
