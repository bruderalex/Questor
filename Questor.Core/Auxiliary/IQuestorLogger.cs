using System;

namespace Questor.Core.Auxiliary
{
    public interface IQuestorLogger
    {
        void LogInfo(string message, params object[] args);
        
        void LogWarning(string message, params object[] args);
        
        void LogError(string message, Exception exception, params object[] args);
    }
}