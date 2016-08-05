namespace NFlat.Micro
{
    internal interface ICommand
    {
        string Message { get; }
    }

    internal static class CommandExtension
    {
        internal static void Compile(this ICommand command, ICompileContext context)
        {
            if (command is ICompileCommand) {
                (command as ICompileCommand).Compile(context);
                return;
            }
            throw Error.NotInDeclareContext(command);
        }

        internal static void Declare(this ICommand command, IDeclareContext context)
        {
            if (command is IDeclareCommand) {
                (command as IDeclareCommand).Declare(context);
                return;
            }
            if (context.Source.Peek() is NewAlias) {
                context.Stack.Push(new AliasTarget (command));
                return;
            }
            throw Error.NotInCompileContext(command);
        }
    }
}
