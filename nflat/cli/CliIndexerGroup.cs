using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NFlat.Micro
{
    internal class CliIndexerGroup : ITypeMember, IIndexer
    {
        private readonly IEnumerable<CliIndexer> mIndexers;

        private CliIndexerGroup(IEnumerable<CliIndexer> indexers)
        {
            mIndexers = indexers;
        }

        public string Message => mIndexers.First().Message;

        internal static CliIndexerGroup Create(IEnumerable<PropertyInfo> members)
        {
            var indexers = members.Where(p => p.GetIndexParameters().Length != 0)
                .Select(p => new CliIndexer(p));
            return new CliIndexerGroup(indexers.ToList().AsReadOnly());
        }

        public void Compile(ICompileContext ctx)
        {
            throw Error.UnsupportedMember(this);
        }

        public ITypeMember NonStatic()
        {
            var indexers = mIndexers.Where(m => m.HasThis);
            if (!indexers.Any()) throw Error.NotInstanceMember(this);
            return new CliIndexerGroup(indexers);
        }


        public IValue GetForIndex(IValue instance, CliArguments args)
        {
            return CliHelper.Resolve(mIndexers, args).GetForIndex(instance, args);
        }
    }
}
