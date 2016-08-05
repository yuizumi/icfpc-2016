namespace NFlat.Micro
{
    internal abstract class Matcher
    {
        internal virtual int Priority => 0;
        internal abstract Token MatchToken(string text);
        internal abstract Stem MatchStem(string text);
    }
}
