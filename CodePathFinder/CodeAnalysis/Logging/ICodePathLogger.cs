namespace CodePathFinder.CodeAnalysis.Logging
{
    using System;

    public interface ICodePathLogger
    {
        void Error(Exception ex, string message, params object[] parameters);
        void Error(string message, params object[] parameters);
        void Warning(string message, params object[] parameters);
        void Debug(string message, params object[] parameters);
    }
}
