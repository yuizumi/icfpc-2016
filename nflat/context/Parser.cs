using System.Collections.Generic;

namespace NFlat.Micro
{
    internal class Parser
    {
        private struct Block { public int Index, Indent; }

        private List<ICommand> mCommands;
        private Stack<Block> mThenBlocks;
        private Stack<Block> mLineBlocks;

        private string mFileName;
        private int  mLineNumber;

        private int mIndent;

        internal static IParsedSource Parse(IReadOnlyList<Token> tokens)
        {
            var parser = new Parser();
            try {
                parser.DoParse(tokens);
            } catch (NFlatException e) {
                throw new NFlatLineNumberedException(e, parser.mFileName, parser.mLineNumber);
            }
            return new ParsedSource(parser.mCommands.AsReadOnly());
        }

        private void DoParse(IReadOnlyList<Token> tokens)
        {
            mCommands = new List<ICommand>();
            mThenBlocks = new Stack<Block>();
            mLineBlocks = new Stack<Block>();

            for (int i = 0; i < tokens.Count; i++) {
                if (tokens[i].Stem is File) {
                    ParseFile(tokens[i].Stem as File);
                    continue;
                }
                if (tokens[i].Stem is Line) {
                    ParseLine(tokens[i].Stem as Line, tokens[i + 1]);
                    continue;
                }
                if (tokens[i].Stem is ThenKeyword) {
                    ParseThen();
                    continue;
                }
                if (tokens[i].Stem is ElseKeyword) {
                    ParseElse();
                    continue;
                }

                ICommand command = tokens[i].Parse();

                if (command is Sequence) {
                    mCommands.AddRange((command as Sequence).Commands);
                } else {
                    mCommands.Add(command);
                }
            }

            ResolveBlocks(mThenBlocks, 0);
            ResolveBlocks(mLineBlocks, 0);
        }

        private void ParseFile(File file)
        {
            ResolveBlocks(mThenBlocks, 0);
            ResolveBlocks(mLineBlocks, 0);
            mFileName = file.Name;
            mCommands.Add(new SetFile(mFileName));
        }

        private void ParseLine(Line line, Token next)
        {
            mLineNumber = line.Number;
            mIndent = line.Indent;
            ResolveBlocks(mThenBlocks, mIndent + ((next.Stem is ElseKeyword) ? 1 : 0));
            ResolveBlocks(mLineBlocks, mIndent);
            mCommands.Add(new SetLine(mLineNumber));
            mLineBlocks.Push(new Block() { Index = mCommands.Count, Indent = mIndent });
            mCommands.Add(null);  // SetExit.
        }

        private void ParseThen()
        {
            mThenBlocks.Push(new Block() { Index = mCommands.Count, Indent = mIndent });
            mCommands.Add(null);  // SetExit
            mCommands.Add(new Then());
        }

        private void ParseElse()
        {
            if (mThenBlocks.Count == 0) throw Error.MissingThen();
            Block then = mThenBlocks.Pop();
            mCommands[then.Index] = new SetExit(mCommands.Count - 1);
            mCommands.Add(new SetLine(mLineNumber));
            mLineBlocks.Push(new Block() { Index = mCommands.Count, Indent = mIndent });
            mCommands.Add(null);  // SetExit
            mCommands.Add(new Else(mCommands[then.Index + 1] as Then));
        }

        private void ResolveBlocks(Stack<Block> blocks, int indent)
        {
            while (blocks.Count > 0) {
                Block block = blocks.Peek();
                if (block.Indent < indent) break;
                mCommands[blocks.Pop().Index] = new SetExit(mCommands.Count - 1);
            }
        }
    }
}
