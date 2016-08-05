using System;

namespace NFlat.Micro
{
    internal class Throw : Keyword, ICompileCommand
    {
        internal const string _Text = "投げる";
        internal override string Text => _Text;

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
        {
            var value = ctx.Stack.Pop();
            if (!value.Has(typeof(Exception)))
                throw Error.NotThrowable(value);
            ctx.Output.RawEmit($"throw {value.Get(typeof(Exception))};");
            ctx.Stack.MarkAsUnused();
        }
    }

    internal class ThrowNew : KeywordPlus
    {
        internal const string _Text = "例外を投げる";

        private readonly int mArity = -1;

        internal ThrowNew(int arity)
        {
            mArity = arity;
        }

        public ThrowNew() {}

        internal override string Text => _Text;

        internal override Stem WithSpec(int value)
            => new ThrowNew(value);

        internal override ICommand Parse()
            => new Sequence(new MakeObject(mArity), new Throw());
    }
}
