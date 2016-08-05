using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class Compound : Stem
    {
        private readonly IReadOnlyList<Stem> mStems;

        internal Compound(IEnumerable<Stem> stems)
        {
            mStems = stems.ToList().AsReadOnly();
        }

        internal override string Text
            => String.Join("", mStems.Select(stem => stem.Text));

        internal override ICommand Parse()
            => new Sequence(mStems.Select(stem => stem.Parse()));

        public override string ToString()
            => $"Compound({String.Join("", mStems)})";
    }
}
