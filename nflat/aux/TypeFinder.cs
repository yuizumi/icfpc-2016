using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal static class TypeFinder
    {
        internal static IEnumerable<TObject> CreateObjects<TObject>()
        {
            return typeof(TObject).Assembly.GetTypes()
                .Where(type => type.IsSubclassOf(typeof(TObject)) && !type.IsAbstract)
                .Select(type => (TObject) Activator.CreateInstance(type));
        }
    }
}
