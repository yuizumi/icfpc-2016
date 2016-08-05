using System.Collections.Generic;
using System.Linq;

using BF = System.Reflection.BindingFlags;

namespace NFlat.Micro
{
    internal sealed class Suffix
    {
        private static readonly IReadOnlyList<Suffix> Suffixes;

        internal static readonly Suffix とは = new Suffix("とは");
        internal static readonly Suffix は = new Suffix("は");
        internal static readonly Suffix が = new Suffix("が");
        internal static readonly Suffix から = new Suffix("から");
        internal static readonly Suffix より = new Suffix("より");
        internal static readonly Suffix を = new Suffix("を");
        internal static readonly Suffix だけ = new Suffix("だけ");
        internal static readonly Suffix で = new Suffix("で");
        internal static readonly Suffix に = new Suffix("に");
        internal static readonly Suffix へ = new Suffix("へ");
        internal static readonly Suffix と = new Suffix("と");
        internal static readonly Suffix という = new Suffix("という");
        internal static readonly Suffix の = new Suffix("の");
        internal static readonly Suffix None = new Suffix("");

        private Suffix(string text)
        {
            Text = text;
        }

        static Suffix()
        {
            Suffixes = typeof(Suffix).GetFields(BF.NonPublic | BF.Static)
                .Select(f => f.GetValue(null)).OfType<Suffix>()
                .OrderByDescending(s => s.Text.Length).ToList();
        }

        internal bool IsDefining => (this == は) || (this == とは);
        internal string Text { get; }

        internal static Suffix FindBestMatch(string value)
        {
            return Suffixes.First(suffix => value.EndsWith(suffix.Text));
        }

        public override string ToString() => $"Suffix({Text})";
    }
}
