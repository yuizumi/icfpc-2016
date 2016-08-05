namespace NFlat.Micro
{
    internal class MakeObject : KeywordPlus, ICompileCommand
    {
        internal const string _Text = "オブジェクトを作成";

        internal static readonly Identifier CtorName = Identifier.Of(".ctor");

        private readonly int mArity = -1;

        internal MakeObject(int arity)
        {
            mArity = arity;
        }

        public MakeObject() {}

        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        internal override Stem WithSpec(int value)
            => new MakeObject(value);

        public void Compile(ICompileContext ctx)
        {
            var group = Pop<TypeValue>(ctx.Stack).NFType.GetMember(CtorName);
            if (mArity >= 0) group = (group as CliMethodGroup).WithArity(mArity);
            group.Compile(ctx);
        }
    }
}
