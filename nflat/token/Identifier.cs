using System.Collections.Concurrent;

namespace NFlat.Micro
{
    using IdentifierMap = ConcurrentDictionary<string, Identifier>;

    internal sealed class Identifier : Stem, ICompileCommand, IDeclareCommand
    {
        private static readonly IdentifierMap Identifiers =
            new IdentifierMap();

        private Identifier(string text)
        {
            Text = text;
        }

        internal override string Text { get; }
        public string Message => $"「{Text}」";

        internal string CSharp
        {
            get {
                return (Text.EndsWith("?")) ? $"_q{Text.Remove(Text.Length - 1)}" : Text;
            }
        }

        internal static Identifier Of(string text)
        {
            return Identifiers.GetOrAdd(text, text_ => new Identifier(text_));
        }

        internal override ICommand Parse() => this;

        public void Compile(ICompileContext ctx)
            => Resolve(ctx).Compile(ctx);

        public void Declare(IDeclareContext ctx)
            => Resolve(ctx).Declare(ctx);

        internal ICommand Resolve(ICompileContext ctx)
            => Resolve(ctx, ctx.Stack);

        internal ICommand Resolve(IDeclareContext ctx)
            => Resolve(ctx, ctx.Stack);

        private ICommand Resolve(IContext ctx, NFStack stack)
        {
            if (stack.Count > 0) {
                IValue top = stack.Peek();
                if (top is TypeValue) {
                    var member = (top as TypeValue).NFType.FindMember(this);
                    if (member != null) { stack.Pop(); return member; }
                } else if (!(top is NewEntity)) {
                    var member = ctx.TypePool.Get(top.Type).FindMember(this)?.NonStatic();
                    if (member != null) return member;
                }
            }
            return ctx.Bindings.Resolve(this) ?? new Undefined(this);
        }
    }
}
