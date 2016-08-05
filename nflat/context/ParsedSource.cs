using System.Collections.Generic;
using System.IO;

namespace NFlat.Micro
{
    internal class ParsedSource : IParsedSource
    {
        private readonly IReadOnlyList<ICommand> mCommands;
        private readonly int mMaxIndex;

        private int mIndex;
        private int mExitIndex;

        private ParsedSource(IReadOnlyList<ICommand> commands, int lineNumber,
                             int minIndex, int maxIndex)
        {
            mCommands = commands;
            mIndex = minIndex;
            mMaxIndex = maxIndex;
            LineNumber = lineNumber;
            mExitIndex = maxIndex;
        }

        internal ParsedSource(IReadOnlyList<ICommand> commands)
            : this(commands, 0, 0, commands.Count - 1)
        {
        }

        public int LineNumber { get; private set; }

        private bool Seek()
        {
            for (; mIndex <= mMaxIndex; mIndex++) {
                if (mCommands[mIndex] is SetLine) {
                    LineNumber = (mCommands[mIndex] as SetLine).Number;
                    continue;
                }
                if (mCommands[mIndex] is SetExit) {
                    mExitIndex = (mCommands[mIndex] as SetExit). Index;
                    continue;
                }
                return true;
            }
            return false;
        }

        public ICommand Peek()
        {
            return (Seek()) ? mCommands[mIndex] : null;
        }

        public ICommand Next()
        {
            if (!Seek()) throw new NFlatBugException();
            return mCommands[mIndex++];
        }

        public IParsedSource NextBlock()
        {
            int index = mIndex;
            mIndex = mExitIndex + 1;
            return new ParsedSource(mCommands, LineNumber, index, mExitIndex);
        }

        internal void Dump(TextWriter writer)
        {
            for (int i = 0; i < mCommands.Count; i++)
                writer.WriteLine($"{i,4}: {mCommands[i]}");
        }
    }
}
