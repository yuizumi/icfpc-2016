namespace NFlat.Micro
{
    internal class Comment : Stem
    {
        internal static readonly Stem Stem = new Comment();

        private Comment() {}

        internal override string Text => "（コメント）";

        internal override ICommand Parse() => Sequence.Empty();

        public override string ToString() => "Comment()";
    }
}
