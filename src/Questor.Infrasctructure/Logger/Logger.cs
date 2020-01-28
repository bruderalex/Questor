using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Questor.Core.Auxiliary;

namespace Questor.Infrasctructure.Logger
{
    public class Logger<T> : IQuestorLogger<T>
    {
        private readonly Stopwatch _stopwatch;

        private readonly ILogger<T> _internalLogger;

        public Logger(ILoggerFactory loggerFactory)
        {
            this._internalLogger = loggerFactory.CreateLogger<T>();
            this._stopwatch = new Stopwatch();
        }

        public async Task<TV> LogTimeAsync<TV>(Func<Task<TV>> func)
        {
            this._stopwatch.Restart();

            var result = await func();

            this._stopwatch.Stop();

            this._internalLogger.LogInformation($"{func.Method.Name} completed in {this._stopwatch.ElapsedMilliseconds} ms");
            
            return result;
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