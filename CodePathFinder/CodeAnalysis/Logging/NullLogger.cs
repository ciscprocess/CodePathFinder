using System;

namespace CodePathFinder.CodeAnalysis.Logging
{
    internal class NullLogger : ICodePathLogger
    {
        public void Debug(string message, params object[] parameters)
        {
        }

        public void Error(Exception ex, string message, params object[] parameters)
        {
        }

        public void Error(string message, params object[] parameters)
        {
        }

        public void Warning(string message, params object[] parameters)
        {
        }
    }
}
