using CodePathFinder.CodeAnalysis.Logging;
using NLog;
using System;

namespace CodePathFinder.VisualUtility.Logging
{
    public class NLogLogger : ICodePathLogger
    {
        private readonly ILogger loggerInstance;

        public NLogLogger(ILogger loggerInstance)
        {
            this.loggerInstance = loggerInstance;
        }

        public void Debug(string message, params object[] parameters)
        {
            this.loggerInstance.Debug(message, parameters);
        }

        public void Error(Exception ex, string message, params object[] parameters)
        {
            this.loggerInstance.Error(ex, message, parameters);
        }

        public void Error(string message, params object[] parameters)
        {
            this.loggerInstance.Error(message, parameters);
        }

        public void Warning(string message, params object[] parameters)
        {
            this.loggerInstance.Warn(message, parameters);
        }
    }
}
