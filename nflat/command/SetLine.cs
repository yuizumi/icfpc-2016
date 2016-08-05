namespace NFlat.Micro
{
    internal class SetLine : ICommand
    {
        internal SetLine(int number)
        {
            Number = number;
        }

        internal int Number { get; }
        public string Message => "内部命令";

        public override string ToString() => $"SetLine({Number})";
    }
}
