using Questor.Core.Data.Entities.Base;

namespace Questor.Core.Data.Entities
{
    public class SearchResultItem : BaseEntity
    {
        public SearchResultItem()
        {
            
        }

        public SearchResultItem(EngineType engineType, string title, string content, string url)
        {
            this._engineType = engineType;
            this.Title = title;
            this.Content = content;
            this.Url = url;
        }
        
        public string Title { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public int SearchResultId { get; set; }

        private SearchResult _searchResult;
        
        public SearchResult SearchResult => this._searchResult;
        
        private EngineType _engineType;
        
        public EngineType EngineType => this._engineType;
    }
}