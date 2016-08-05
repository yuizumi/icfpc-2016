using System.Collections.Generic;

namespace NFlat.Micro
{
    internal abstract class NFStack
    {
        protected abstract List<IValue> Values { get; }

        internal IReadOnlyList<IValue> Content
            => Values.AsReadOnly();

        internal int Count => Values.Count;

        internal void Push(IValue value)
        {
            Values.Add(value);
        }

        internal IValue Pop()
        {
            var value = Peek();
            Values.RemoveAt(Count - 1);
            return value;
        }

        internal IValue Peek()
        {
            if (Count == 0) throw Error.InsufficientValues();
            return Values[Count - 1];
        }
    }
}
