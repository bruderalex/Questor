using System.Collections.Generic;

namespace Questor.Core.Services.Engines
{
    public class RawResult
    {
        public RawResult(string content, ISearchEngine searchEngine)
        {
            this.Content = content;
            this.SearchEngine = searchEngine;
        }
        
        public ISearchEngine SearchEngine {get;}
        
        public string Content {get; }
    }
}