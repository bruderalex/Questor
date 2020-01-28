using System;
using Questor.Core.Services.Engines;

namespace Questor.Core.Exceptions
{
    public class SearchEngineException : Exception
    {
        public SearchEngineException()
        {
        }

        public SearchEngineException(string message)
            : base(message)
        {
        }

        public SearchEngineException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}