using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines
{
    public interface ISearchResponseParser
    {
        Task<IEnumerable<SearchResultItem>> ParseRawResponse(RawResult rawResult);
    }
}