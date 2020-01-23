using System;
using System.Collections.Generic;
using Questor.Core.Data.Entities.Base;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;

namespace Questor.Core.Data.Entities
{
    public class SearchResult : BaseEntity
    {
        public SearchResult()
        {
        }

        public SearchResult(string question, IEnumerable<SearchResultItem> searchResultItems, DateTime searchDate, SearchEngineTypeEnum searchEngineTypeEnum)
        {
            this.Question = question;
            this.Date = searchDate;
            this.SearchEngineTypeEnum = searchEngineTypeEnum;
            this._searchResultItems.AddRange(searchResultItems);
        }

        public string Question { get; }

        public DateTime Date { get; }

        private readonly List<SearchResultItem> _searchResultItems = new List<SearchResultItem>();

        public SearchEngineTypeEnum SearchEngineTypeEnum { get; private set; }

        public IReadOnlyCollection<SearchResultItem> SearchResultItems => this._searchResultItems.AsReadOnly();
    }
}