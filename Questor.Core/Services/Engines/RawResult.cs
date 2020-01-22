using System.Collections.Generic;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines
{
    public class RawResult
    {
        public RawResult(string content, SearchEngineType engineType)
        {
            this.Content = content;
            this.SearchEngineType = engineType;
        }
        
        public SearchEngineType SearchEngineType {get;}
        
        public string Content {get; }
    }
}