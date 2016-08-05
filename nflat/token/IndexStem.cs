using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class IndexStem : Stem
    {
        private readonly IReadOnlyList<Stem> mArgs;

        internal IndexStem(IEnumerable<Stem> args)
        {
            mArgs = args.ToList().AsReadOnly();
        }

        internal override string Text
        {
            get {
                return "［" + String.Join("、", mArgs.Select(x => x.Text)) + "］";
            }
        }

        internal override ICommand Parse()
        {
            return new Index(mArgs.Select(arg => arg.Parse()));
        }
    }
}
