using System;
using System.Collections.Concurrent;
using System.Linq;

namespace NFlat.Micro
{
    internal class TypePool
    {
        private readonly ConcurrentDictionary<Type, NFType> mTypes =
            new ConcurrentDictionary<Type, NFType>();

        internal TypePool() {}

        internal void Add(Type type, NFType nftype)
        {
            type = Normalize(type);
            if (!mTypes.TryAdd(type, nftype)) throw new NFlatBugException();
        }

        internal NFType Get(Type type)
        {
            type = Normalize(type);
            return mTypes.GetOrAdd(type, _type => new CliType(this, _type));
        }

        private static Type Normalize(Type type)
        {
            if (!type.IsConstructedGenericType)
                return type;
            if (type.GetGenericArguments().All(t => !t.IsGenericParameter))
                return type;
            if (type.GetGenericArguments().All(t =>  t.IsGenericParameter))
                return type.GetGenericTypeDefinition();
            throw new NFlatBugException();
        }
    }
}
