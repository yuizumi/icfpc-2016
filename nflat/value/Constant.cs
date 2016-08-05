using System;

namespace NFlat.Micro
{
    internal class Constant<T> : Value, ICompileCommand
    {
        private readonly object mValue;

        internal Constant(T value)
        {
            mValue = value;
        }

        protected override string Code => CSharpString.Primitive(mValue);
        public override Type Type => typeof(T);

        public override string Message => $"{Type} 型の定数";

        public void Compile(ICompileContext ctx)
            => ctx.Stack.Push(this);

        public override bool Has(Type type)
        {
            if (Type.HasImplicitTo(type))
                return true;

            if (Type == typeof(int)) {
                return HasImplicitTo((int)  mValue, type);
            }
            if (Type == typeof(long)) {
                return HasImplicitTo((long) mValue, type);
            }

            return false;
        }

        private static bool HasImplicitTo(long value, Type type)
            => (type == typeof(ulong)) && (value >= 0);

        private static bool HasImplicitTo(int value, Type type)
        {
            if (!type.IsPrimitive)
                return false;

            switch (Type.GetTypeCode(type)) {
                case TypeCode.SByte:
                    return (value >= SByte.MinValue) && (value <= SByte.MaxValue);
                case TypeCode.Byte:
                    return (value >= Byte.MinValue) && (value <= Byte.MaxValue);
                case TypeCode.Int16:
                    return (value >= Int16.MinValue) && (value <= Int16.MaxValue);
                case TypeCode.UInt16:
                    return (value >= UInt16.MinValue) && (value <= UInt16.MaxValue);
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return (value >= 0);  // UInt32/64 can represent Int32.MaxValue.
            }
            return false;
        }
    }
}
