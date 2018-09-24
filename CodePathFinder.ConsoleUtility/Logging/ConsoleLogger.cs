using System;
using CodePathFinder.CodeAnalysis.Logging;

namespace CodePathFinder.ConsoleUtility.Logging
{
    class ConsoleLogger : ICodePathLogger
    {
        private const string Template = "[{0}] {1}: {2}";

        public void Debug(string message, params object[] parameters)
        {
            Console.WriteLine(
                string.Format(Template, DateTime.UtcNow, "DEBUG", message), 
                parameters);
        }

        public void Error(Exception ex, string message, params object[] parameters)
        {
            Error(message, parameters);
            Console.WriteLine("--- Exception ---");
            Console.WriteLine(ex.ToString());
        }

        public void Error(string message, params object[] parameters)
        {
            Console.WriteLine(
                string.Format(Template, DateTime.UtcNow, "ERROR", message), 
                parameters);
        }

        public void Warning(string message, params object[] parameters)
        {
            Console.WriteLine(
                string.Format(Template, DateTime.UtcNow, "WARN", message), 
                parameters);
        }
    }
}
