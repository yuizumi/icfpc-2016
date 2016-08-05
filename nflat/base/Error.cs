using System;

namespace NFlat.Micro
{
    internal static class Error
    {
        private static NFlatException _(string message) => new NFlatException(message);

        internal static NFlatException AlreadyDefined(Identifier name) =>
            _($"「{name.Text}」はすでに定義されています。");

        internal static NFlatException AmbiguousOverloads(CliMember member) =>
            _($"{member.Message}のオーバーロードを解決できません。");

        internal static NFlatException ArgumentsMismatch(CliMethodGroup methodGroup) =>
            _($"{methodGroup.Message}に対する引数が一致しません。");

        internal static NFlatException DimensionMismatch(IValue instance) =>
            _($"添字の要素数が{instance.Message}の次元と一致しません。");

        internal static NFlatException IdentifierExpected(string text) =>
            _($"「{text}」は識別子ではありません。");

        internal static NFlatException ImportOutsideType() =>
            _($"型の外側でモジュールを使用することはできません。");

        internal static NFlatException ImportToExistingType() =>
            _($"既存の型でモジュールを使用することはできません。");

        internal static NFlatException IncompleteDefinition(Identifier name) =>
            _($"「{name.Text}」が正しく定義されていません。");

        internal static NFlatException IncompleteType(Type type) =>
            _($"|T:{type}| は完全な型ではありません。");

        internal static NFlatException InconsistentReturnType() =>
            _($"戻り値の型が宣言と一致しません。");

        internal static NFlatException InconsistentStack(IContext cThis, IContext cThat)
        {
            if (cThat == cThis.Parent) {
                return _($"「{cThis.Name}」の内側でスタックが変化しています。");
            } else {
                return _($"「{cThis.Name}」と「{cThat.Name}」でスタックが一致しません。");
            }
        }

        internal static NFlatException InsufficientValues() =>
            _($"値が不足しています。");

        internal static NFlatException InvalidBracket(Bracket bracket) =>
            _($"「{bracket.Open}」の使い方が正しくありません。");

        internal static NFlatException InvalidDimension(int dimension) =>
            _($"{dimension}次元の配列を作ることはできません。");

        internal static NFlatException InvalidIndexArgument(ICommand command) =>
            _($"{command.Message}を添字として使用することはできません。");

        internal static NFlatException InvalidIndexArgument(Stem stem) =>
            _($"「{stem.Text}」を添字として使用することはできません。");

        internal static NFlatException InvalidToken(string text) =>
            _($"「{text}」は正しい字句ではありません。");

        internal static NFlatException InvalidTypeArgument(IValue value) =>
            _($"{value.Message}を型引数として指定することはできません。");

        internal static NFlatException InvalidValue(IValue value) =>
            _($"{value.Message}を値として使用することはできません。");

        internal static NFlatException InvalidUsage(Keyword keyword) =>
            _($"「{keyword.Text}」の使い方が正しくありません。");

        internal static NFlatException MemberDefinedOutsideType() =>
            _($"型の外側でメンバーを定義することはできません。");

        internal static NFlatException MemberNotFound(Identifier name) =>
            _($"「{name.Text}」というメンバーは存在しません。");

        internal static NFlatException MissingBlock(string name) =>
            _($"「{name}」に続く単語が存在しません。");

        internal static NFlatException MissingBody(string name) =>
            _($"「{name}」の本体が存在しません。");

        internal static NFlatException MissingThen() =>
            _($"対応する「ならば」が存在しません。");

        internal static NFlatException MultipleConstraints() =>
            _($"引数の制約を重ねて指定することはできません。");

        internal static NFlatException MultipleMatchesImported(Identifier name) =>
            _($"複数の型で「{name.Text}」が定義されています。");

        internal static NFlatException MultipleReturnValues() =>
            _($"複数の戻り値を返すことはできません。");

        internal static NFlatException NewMemberToExistingType() =>
            _($"既存の型に新しいメンバーを追加することはできません。");

        internal static NFlatException NoOverloadsWithArity(CliMethodGroup methodGroup,
                                                            int arity) =>
            _($"{methodGroup.Message}に {arity} 引数のオーバーロードは存在しません。");

        internal static NFlatException NonStaticMemberInModule() =>
            _($"モジュールに通常のメンバーを定義することはできません。");

        internal static NFlatException NotAssignable(IValue target, IValue source) =>
            _($"{target.Message}に{source.Message}を代入することはできません。");

        internal static NFlatException NotAssignable(IValue target) =>
            _($"{target.Message}に値を代入することはできません。");

        internal static NFlatException NotAliasible(IValue value) =>
            _($"{value.Message}に別名をつけることはできません。");

        internal static NFlatException NotIEnumerable(Type type) =>
            _($"|T:{type}| は列挙可能ではありません。");

        internal static NFlatException NotInCompileContext(ICommand command) =>
            _($"{command.Message}を宣言文中に書くことはできません。");

        internal static NFlatException NotInDeclareContext(ICommand command) =>
            _($"{command.Message}を本体の中で使うことはできません。");

        internal static NFlatException NotIndexer(IValue value) =>
            _($"{value.Message}に添字を指定することはできません。");

        internal static NFlatException NotInstanceMember(ITypeMember member) =>
            _($"{member.Message}にインスタンスを通してアクセスすることはできません。");

        internal static NFlatException NotInteger(IValue value) =>
            _($"{value.Message}は整数ではありません。");

        internal static NFlatException NotMethod(Identifier name) =>
            _($"「{name.Text}」はメソッドではありません。");

        internal static NFlatException NotRedefinable(Stem stem) =>
            _($"「{stem.Text}」の再定義はできません。");

        internal static NFlatException NotThrowable(IValue value) =>
            _($"{value.Message}を投げることはできません。");

        internal static NFlatException NotTypeCommand(ICommand command) =>
            _($"{command.Message}は型ではありません。");

        internal static NFlatException NotTypeMember(ICommand command) =>
            _($"{command.Message}を型のメンバーとして定義することはできません。");

        internal static NFlatException OperatorNotApplicable(Keyword keyword, Type type) =>
            _($"|T:{type}| に対して{keyword.Message}を適用することはできません。");

        internal static NFlatException OperatorNotApplicable(Keyword keyword, Type type1,
                                                             Type type2) =>
            _($"|T:{type1}| と |T:{type2}| の組に対して" +
              $"{keyword.Message}を適用することはできません。");

        internal static NFlatException OutsideLoop(Stem stem) =>
            _($"「{stem.Text}」を繰り返しの外側で使うことはできません。");

        internal static NFlatException TypeNotConvertible(Type src, Type dst) =>
            _($"|T:{src}| を |T:{dst}| に変換することはできません。");

        internal static NFlatException TypeNotFound(string name) =>
            _($"「{name}」という名前の型は存在しません。");

        internal static NFlatException UnclosedBracket(Bracket bracket) =>
            _($"「{bracket.Open}」が正しく閉じられていません。");

        internal static NFlatException UndefinedIdentifier(Identifier name) =>
            _($"「{name.Text}」は未定義の識別子です。");

        internal static NFlatException UnsupportedMember(ITypeMember member) =>
            _($"{member.Message}をこの言語で使用することはできません。");
    }
}
