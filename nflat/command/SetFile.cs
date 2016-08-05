namespace NFlat.Micro
{
    internal class SetFile : ICommand
    {
        internal SetFile(string name)
        {
            Name = name;
        }

        internal string Name { get; }
        public string Message => "内部命令";

        public override string ToString() => $"SetFile({Name})";
    }
}
