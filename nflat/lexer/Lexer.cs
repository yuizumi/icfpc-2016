using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NFlat.Micro
{
    using static Lexing;

    internal static class Lexer
    {
        internal static void Parse(string filename, List<Token> tokens)
        {
            tokens.Add(new Token(new File(filename), Suffix.None));
            using (var reader = new StreamReader(filename))
                Parse(reader, tokens, filename);
        }

        private static void Parse(TextReader reader, List<Token> tokens,
                                  string filename)
        {
            int lineno = 0;
            try {
                for (string line; (line = reader.ReadLine()) != null; )
                    tokens.AddRange(ParseLine(line, ++lineno));
            } catch (NFlatException e) {
                throw new NFlatLineNumberedException(e, filename, lineno);
            }
        }

        private static IEnumerable<Token> ParseLine(string line, int lineno)
        {
            int indent = 0;
            while (indent < line.Length && IsIndenter(line[indent]))
                ++indent;
            if (indent == line.Length)
                yield break;
            yield return new Token(new Line(lineno, indent), Suffix.None);
            foreach (string word in SplitLine(line, indent))
                yield return Matchers.MatchToken(word);
        }

        private static IEnumerable<string> SplitLine(string line, int index)
        {
            var text = new StringBuilder(line).Append('\n');

            Bracket bracket = Bracket.None;
            int start = -1;
            for (int i = 0; i < text.Length; i++) {
                if (bracket.Normalize) text[i] = Normalize(text[i]);

                if (bracket != Bracket.None) {
                    if (Normalize(text[i]) == bracket.Close) {
                        text[i] = bracket.Close;
                        bracket = Bracket.None;
                    }
                    continue;
                }
                if (IsSeparator(text[i])) {
                    if (start >= 0) {
                        yield return text.ToString(start, i - start);
                    }
                    if (IsStartComment(text[i])) {
                        yield return text.ToString(i, text.Length - i);
                        break;
                    }
                    start = -1;
                } else {
                    bracket = Bracket.Brackets.Get(text[i], Bracket.None);
                    if (bracket.IsHeadOnly && start >= 0) throw Error.InvalidBracket(bracket);
                    if (start < 0) start = i;
                }
            }

            if (bracket != Bracket.None) throw Error.UnclosedBracket(bracket);
        }
    }
}
