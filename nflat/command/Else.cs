namespace NFlat.Micro
{
    internal class Else : ICompileCommand
    {
        internal Else(Then then)
        {
            Then = then;
        }

        internal Then Then { get; }

        public string Message => $"「{ElseKeyword._Text}」";

        public void Compile(ICompileContext ctx) { throw new NFlatBugException(); }
        public override string ToString() => $"Else(_{Then.GetHashCode():x8})";
    }
}
