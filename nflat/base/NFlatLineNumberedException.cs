using System;

namespace NFlat.Micro
{
    internal class NFlatLineNumberedException : Exception
    {
        internal NFlatLineNumberedException(NFlatException exception,
                                            string filename,
                                            int lineNumber)
            : base(exception.Message, exception)
        {
            FileName = filename;
            LineNumber = lineNumber;
        }

        internal string FileName { get; }
        internal int  LineNumber { get; }

        public override string Message
            => $"{FileName}: {LineNumber} 行目: {base.Message}";
    }
}
