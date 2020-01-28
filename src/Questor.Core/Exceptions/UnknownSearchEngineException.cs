using System;

namespace Questor.Core.Exceptions
{
    public class UnknownSearchEngineException : Exception
    {
        public UnknownSearchEngineException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }
}