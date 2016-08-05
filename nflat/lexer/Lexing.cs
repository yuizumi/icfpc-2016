using System;
using System.Text;

namespace NFlat.Micro
{
    internal static class Lexing
    {
        private static string NormalizeTable = BuildNormalizeTable();
        private static string CanonicalTable = BuildCanonicalTable();

        internal static string Canonicalize(string text)
        {
            var output = new StringBuilder();
            bool stripHiragana = false;
            foreach (char c in text) {
                if (IsHiragana(c) && stripHiragana) {
                    continue;
                }
                output.Append(CanonicalTable[NormalizeTable[c]]);
                stripHiragana |= !IsHiragana(c);
            }
            return output.ToString().Normalize();
        }

        internal static char Normalize(char c)
            => NormalizeTable[c];

        internal static bool IsHiragana(char c)
        {
            return (c >= '\u3041') && (c <= '\u309F');
        }

        internal static bool IsStartComment(char c) => (c == '※');

        internal static bool IsIndenter(char c)
        {
            return (c == '\t') || (c == ' ') || (c == '　');
        }

        internal static bool IsSeparator(char c)
        {
            return Char.IsWhiteSpace(c) || IsStartComment(c) || (c == ',') || (c == '、');
        }

        private static string BuildNormalizeTable()
        {
            var table = new char[Char.MaxValue + 1];
            for (int i = 0; i <= Char.MaxValue; i++)
                table[i] = Char.ToUpperInvariant((char) i);

            table['\u3000'] = ' ';  // IDEOGRAPHIC SPACE
            table['\u2212'] = '-';  // MINUS SIGN
            for (int i = '！'; i <= '～'; i++)
                table[i] = table[i - 0xFEE0];
            string source = "｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝﾞﾟ";
            string target = "。「」、・ヲァィゥェォャュョッーアイウエオカキクケコサシスセソ" +
                "タチツテトナニヌネノハヒフヘホマミムメモヤユヨラリルレロワン\u3099\u309A";
            for (int i = 0; i < source.Length; i++)
                table[source[i]] = target[i];

            return new string(table);
        }

        private static string BuildCanonicalTable()
        {
            var table = new char[Char.MaxValue + 1];
            for (int i = 0; i <= Char.MaxValue; i++)
                table[i] = (char) i;

            string source = "ぁぃぅぇぉゕゖっゃゅょゎァィゥェォヵヶッャュョヮ";
            string target = "あいうえおかけつやゆよわアイウエオカケツヤユヨワ";
            for (int i = 0; i < source.Length; i++)
                table[source[i]] = target[i];

            return new string(table);
        }
    }
}
