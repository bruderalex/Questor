﻿using System;
using System.Collections.Generic;
using Questor.Core.Data.Entities.Base;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;

namespace Questor.Core.Data.Entities
{
    public class SearchResult : BaseEntity<int>
    {
        public SearchResult()
        {
        }

        public SearchResult(string question, IEnumerable<SearchResultItem> searchResultItems, DateTime searchDate, SearchEngineTypeEnum engineTypeEnum)
        {
            this.Question = question;
            this.Date = searchDate;
            this._searchResultItems.AddRange(searchResultItems);
            this.EngineTypeId = (int)engineTypeEnum;
            this.UniqueId = Guid.NewGuid();
        }

        public string Question { get; }

        public int EngineTypeId {get;}
        
        public SearchEngineTypeEnum EngineTypeEnum => (SearchEngineTypeEnum)EngineTypeId;

        public DateTime Date { get; }
        
        public Guid UniqueId {get;}

        private readonly List<SearchResultItem> _searchResultItems = new List<SearchResultItem>();

        public IReadOnlyCollection<SearchResultItem> SearchResultItems => this._searchResultItems.AsReadOnly();
    }
}