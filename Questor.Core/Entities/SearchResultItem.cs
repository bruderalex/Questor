using Questor.Core.Entities.Base;

namespace Questor.Core.Entities
{
    public class SearchResultItem : BaseEntity
    {
        public SearchResultItem()
        {
            
        }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public int SearchResultId { get; set; }

        private SearchResult _searchResult;
        
        public SearchResult SearchResult => this._searchResult;
        
        private SearchEngine _searchEngine;
        
        public SearchEngine SearchEngine => this._searchEngine;
    }
}