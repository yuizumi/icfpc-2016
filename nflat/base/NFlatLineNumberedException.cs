using System;

namespace NFlat.Micro
{
    internal class NFlatLineNumberedException : Exception
    {
        internal NFlatLineNumberedException(NFlatException exception, int lineNumber)
            : base(exception.Message, exception)
        {
            LineNumber = lineNumber;
        }

        internal int LineNumber { get; }

        public override string Message => $"{LineNumber} 行目: {base.Message}";
    }
}
