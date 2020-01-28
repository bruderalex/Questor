using System;
using Questor.Core.Services.Engines;

namespace Questor.Core.Exceptions
{
    public class SearchEngineException : Exception
    {
        public SearchEngineException(ISearchEngine engine, Exception innerException = null)
            : base($"{engine.GetType().Name} failed", innerException)
        {
        }
    }
}