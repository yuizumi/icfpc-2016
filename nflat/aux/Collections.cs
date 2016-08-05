using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NFlat.Micro
{
    internal static class Collections
    {
        internal static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        internal static TValue Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue>
                                                     dictionary,
                                                 TKey key)
        {
            return Get(dictionary, key, default(TValue));
        }

        internal static TValue Get<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue>
                                                     dictionary,
                                                 TKey key, TValue defaultValue)
        {
            TValue value;
            return (dictionary.TryGetValue(key, out value)) ? value : defaultValue;
        }
    }
}
