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

        public SearchResult(string question, IEnumerable<SearchResultItem> searchResultItems, DateTime searchDate)
        {
            this.Question = question;
            this.Date = searchDate;
            this._searchResultItems.AddRange(searchResultItems);
        }

        public string Question { get; }

        public DateTime Date { get; }

        private readonly List<SearchResultItem> _searchResultItems = new List<SearchResultItem>();

        public IReadOnlyCollection<SearchResultItem> SearchResultItems => this._searchResultItems.AsReadOnly();
    }
}