using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchCommandHandler : IRequestHandler<SearchCommand, SearchResult>
    {
        private readonly ISearchService _searchService;
        
        public SearchCommandHandler(ISearchService searchService)
        {
            this._searchService = searchService;
        }
        
        public async Task<SearchResult> Handle(SearchCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            return await this._searchService.SearchOnline(request.Question, request.EngineTypes);
        }
    }
}