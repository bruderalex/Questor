using System;
using System.Collections.Generic;
using System.Linq;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Business.Impl
{
    public class SearchResultsCache : ISearchResultsCache
    {
        private readonly Dictionary<Guid, SearchResult> _internalCache = new Dictionary<Guid, SearchResult>();
        
        private const int MaxCacheCapacity = 100;

        public SearchResult GetByGuid(Guid id)
        {
            if (this._internalCache.TryGetValue(id, out var searchResult))
            {
                return searchResult;
            }
            
            return null;
        }

        public void Add(SearchResult searchResult)
        {
            if (searchResult == null)
                throw new ArgumentNullException(nameof(searchResult));
            
            if (searchResult.UniqueId.Equals(Guid.Empty))
                throw new ArgumentException(nameof(searchResult.UniqueId));
            
            this._internalCache.TryAdd(searchResult.UniqueId, searchResult);

            if (this._internalCache.Count > MaxCacheCapacity)
            {
                var removedKey = this._internalCache.Keys.First();
                this._internalCache.Remove(removedKey);
            }
        }
    }
}