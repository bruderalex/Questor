using System;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Business
{
    public interface ISearchResultsCache
    {
        SearchResult GetByGuid(Guid id);
        
        void Add(SearchResult searchResult);
    }
}