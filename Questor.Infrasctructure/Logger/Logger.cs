using System;
using Microsoft.Extensions.Logging;
using Questor.Core.Auxiliary;

namespace Questor.Infrasctructure.Logger
{
    public class Logger<T> : IQuestorLogger<T>
    {
        private readonly ILogger<T> _internalLogger;

        public Logger(ILoggerFactory loggerFactory)
        {
            this._internalLogger = loggerFactory.CreateLogger<T>();
        }

        public void LogInfo(string message, params object[] args)
        {
            this._internalLogger.LogInformation(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            this._internalLogger.LogInformation(message, args);
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            this._internalLogger.LogError(exception, message, args);
        }
    }
}