using System;
using System.Collections.Generic;
using Questor.Core.Entities.Base;

namespace Questor.Core.Entities
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
        }

        public string Question { get; }

        public DateTime Date { get; }

        private readonly List<SearchResultItem> _searchResultItems = new List<SearchResultItem>();

        public IReadOnlyCollection<SearchResultItem> SearchResultItems => this._searchResultItems.AsReadOnly();
    }
}