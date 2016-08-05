namespace NFlat.Micro
{
    internal class Line : Stem
    {
        internal Line(int number, int indent)
        {
            Number = number;
            Indent = indent;
        }

        internal int Number { get; }
        internal int Indent { get; }

        internal override string Text => $"〔{Number} 行目〕";

        internal override ICommand Parse()
        {
            throw new NFlatBugException();
        }

        public override string ToString()
            => $"Line({Number}, Indent={Indent})";
    }
}
