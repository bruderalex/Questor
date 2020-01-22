using System;
using System.Threading.Tasks;

namespace Questor.Core.Auxiliary
{
    public interface IQuestorLogger<T>
    {
        Task<TV> LogTimeAsync<TV>(Func<Task<TV>> func);
        
        void LogInfo(string message, params object[] args);

        void LogWarning(string message, params object[] args);

        void LogError(Exception exception, string message, params object[] args);
    }
}