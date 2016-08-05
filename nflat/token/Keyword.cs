namespace NFlat.Micro
{
    internal abstract class Keyword : Stem
    {
        internal virtual bool ConsumesUndefined => false;
        public virtual string Message => $"「{Text}」";
        internal string Name => Lexing.Canonicalize(Text);

        protected TValue Pop<TValue>(NFStack stack)
            where TValue : class, IValue
        {
            var value = stack.Pop();
            if (value is TValue) {
                return value as TValue;
            }
            throw (typeof(TValue) == typeof(TypeValue))
                ? Error.InvalidTypeArgument(value) : Error.InvalidUsage(this);
        }

        public override string ToString() => $"{GetType().Name}()";
    }
}
