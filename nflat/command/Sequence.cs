using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class Sequence : ICommand
    {
        internal Sequence(IEnumerable<ICommand> commands)
        {
            Commands = commands;
        }

        internal IEnumerable<ICommand> Commands { get; }
        public string Message => "内部命令";

        internal static Sequence Empty()
            => new Sequence(Enumerable.Empty<ICommand>());

        public override string ToString()
            => $"Sequence({string.Join(", ", Commands)})";
    }
}
