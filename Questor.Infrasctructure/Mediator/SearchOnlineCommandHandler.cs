using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchOnlineCommandHandler : IRequestHandler<SearchOnlineCommand, SearchResult>
    {
        private readonly ISearchService _searchService;
        
        public SearchOnlineCommandHandler(ISearchService searchService, IAsyncRepository<SearchResult> resultsRepository)
        {
            this._searchService = searchService;
        }
        
        public async Task<SearchResult> Handle(SearchOnlineCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var result = await this._searchService.SearchOnlineAsync(request.Question, request.EngineTypes);
            
            return result;
        }
    }
}