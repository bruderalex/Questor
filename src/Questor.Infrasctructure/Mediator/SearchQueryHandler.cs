using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResult>
    {
        private ISearchService _searchService;
        
        public SearchQueryHandler(ISearchService searchService)
        {
            this._searchService = searchService;
        }
        
        public async Task<SearchResult> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var result = this._searchService.GetSearchResultByUniqueId(request.Id);
            
            return result;
        }
    }
}