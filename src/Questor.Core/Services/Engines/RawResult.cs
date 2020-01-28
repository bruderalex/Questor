using System.Collections.Generic;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines
{
    public class RawResult
    {
        public RawResult(string content, SearchEngineTypeEnum engineTypeEnum)
        {
            this.Content = content;
            this.SearchEngineTypeEnum = engineTypeEnum;
        }
        
        public SearchEngineTypeEnum SearchEngineTypeEnum {get;}
        
        public string Content {get;}
    }
}