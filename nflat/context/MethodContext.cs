using System;
using System.Collections.Generic;
using System.Linq;

namespace NFlat.Micro
{
    internal class MethodContext : CompileContext
    {
        internal MethodContext(TypeContext parent, Identifier name,
                               IParsedSource source, bool hasThis)
            : base(parent, name.Text, source)
        {
            source.Peek().ThrowOnNull(Error.MissingBody(name.Text));

            Parameters = ParseParameters(parent, source);
            ReturnType = ParseReturnType(parent, source);
            Identifier = name;
            HasThis = hasThis;

            Output = new OuterExprBuilder(this);

            foreach (var e in Parameters) Stack.Push(new Parameter(e));
        }

        public override IExprBuilder Output { get; }

        internal IReadOnlyList<CSharpExpr> Parameters { get; }
        internal Type ReturnType { get; }

        internal Identifier Identifier { get; }
        internal bool HasThis { get; }

        internal override void Break(ICompileContext subctx)
        {
            throw Error.OutsideLoop(new Break());
        }

        internal override void Continue(ICompileContext subctx)
        {
            throw Error.OutsideLoop(new Continue());
        }

        internal override void Return(ICompileContext subctx)
        {
            Type returnType = typeof(void);

            switch (subctx.Stack.Count) {
                case 0:
                    subctx.Output.RawEmit("return;");
                    break;
                case 1:
                    var result = subctx.Stack.Peek();
                    returnType = result.Type;
                    subctx.Output.RawEmit($"return {result.Get(result.Type)};");
                    break;
                default:
                    throw Error.MultipleReturnValues();
            }

            if (!returnType.HasImplicitTo(ReturnType))
                throw Error.InconsistentReturnType();
            subctx.Stack.MarkAsUnused();
        }

        public override CSharpExpr GetSelf()
        {
            if (HasThis) {
                return new CSharpExpr("this", (Parent as TypeContext).NFType.Type);
            }
            return default(CSharpExpr);
        }

        private static IReadOnlyList<CSharpExpr> ParseParameters(
            IContext parent, IParsedSource source)
        {
            if (!(source.Peek() is DeclareParams)) {
                return (new List<CSharpExpr>()).AsReadOnly();
            }
            source.Next();
            var ctxParams = new ParamsContext(parent, DeclareParams._Text,
                                              source.NextBlock());
            ctxParams.Compile();

            return ctxParams.Stack.Content.Select((param, index) => {
                return new CSharpExpr($"_a{index}", (param as TypeValue).NFType.Type);
            }).ToList().AsReadOnly();
        }

        private static Type ParseReturnType(IContext parent, IParsedSource source)
        {
            if (!(source.Peek() is DeclareReturn)) {
                return typeof(void);
            }
            source.Next();
            var ctxParams = new ParamsContext(parent, DeclareReturn._Text,
                                              source.NextBlock());
            ctxParams.Compile();

            switch (ctxParams.Stack.Count) {
                case 0:
                    return typeof(void);
                case 1:
                    return (ctxParams.Stack.Pop() as TypeValue).NFType.Type;
                default:
                    throw Error.MultipleReturnValues();
            }
        }
    }
}
