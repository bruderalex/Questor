using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Business
{
    public interface ISearchService
    {
        Task<SearchResult> SearchOnline(string question, IEnumerable<EngineType> enginesTypes = null);
    }
}