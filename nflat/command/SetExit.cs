namespace NFlat.Micro
{
    internal class SetExit : ICommand
    {
        internal SetExit(int index)
        {
            Index = index;
        }

        internal int Index { get; }
        public string Message => "内部命令";

        public override string ToString() => $"SetExit({Index})";
    }
}
