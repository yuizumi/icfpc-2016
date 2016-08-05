namespace NFlat.Micro
{
    internal class MakeObject : Keyword, ICompileCommand
    {
        internal static readonly Identifier CtorName =
            Identifier.Of(".ctor");

        internal const string _Text = "オブジェクトを作成";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            Pop<TypeValue>(ctx.Stack).NFType.GetMember(CtorName).Compile(ctx);
        }
    }
}
