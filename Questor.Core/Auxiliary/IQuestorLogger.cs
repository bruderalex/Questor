using System;

namespace Questor.Core.Auxiliary
{
    public interface IQuestorLogger<T>
    {
        void LogInfo(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(Exception exception, string message, params object[] args);
    }
}