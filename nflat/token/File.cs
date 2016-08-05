namespace NFlat.Micro
{
    internal class File : Stem
    {
        internal File(string name)
        {
            Name = name;
        }

        internal string Name { get; }

        internal override string Text => $"ファイル：{Name}";

        internal override ICommand Parse()
        {
            throw new NFlatBugException();
        }

        public override string ToString() => $"File({Name})";
    }
}
