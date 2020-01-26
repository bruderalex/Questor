using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchOfflineCommandHandler : IRequestHandler<SearchOfflineCommand, SearchResult>
    {
        private readonly ISearchService _searchService;
        
        public SearchOfflineCommandHandler(ISearchService searchService)
        {
            this._searchService = searchService;
        }
        
        public async Task<SearchResult> Handle(SearchOfflineCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            return await this._searchService.SearchOfflineAsync(request.Question);
        }
    }
}