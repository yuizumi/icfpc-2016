using System;

namespace NFlat.Micro
{
    internal class NFlatException : Exception
    {
        internal NFlatException() : base()
        {
        }

        internal NFlatException(string message)
            : base(message)
        {
        }

        internal NFlatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
