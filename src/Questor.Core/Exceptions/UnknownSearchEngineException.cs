using System;

namespace Questor.Core.Exceptions
{
    public class UnknownSearchEngineException : Exception
    {
        public UnknownSearchEngineException()
        {
        }

        public UnknownSearchEngineException(string message)
            : base(message)
        {
        }

        public UnknownSearchEngineException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}