using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal sealed class Keywords
    {
        private static readonly IReadOnlyDictionary<string, Keyword> KeywordMap =
            TypeFinder.CreateObjects<Keyword>()
            .ToDictionary(k => k.Name).AsReadOnly();

        internal static Keyword Get(string name) => KeywordMap.Get(name);
    }
}
