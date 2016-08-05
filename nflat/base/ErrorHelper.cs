namespace NFlat.Micro
{
    internal static class ErrorHelper
    {
        internal static void Expect(bool condition, NFlatException exception)
        {
            if (!condition) throw exception;
        }

        internal static void Ensure(bool condition)
        {
            if (!condition) throw new NFlatBugException();
        }

        internal static T ThrowOnNull<T>(this T value, NFlatException exception)
            where T : class
        {
            if (value == null) throw exception;
            return value;
        }
    }
}
