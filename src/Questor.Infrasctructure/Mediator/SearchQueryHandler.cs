using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Questor.Core.Data;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchResult>
    {
        private readonly IAsyncRepository<SearchResult, int> _searchResultRepository;
        
        public SearchQueryHandler(IAsyncRepository<SearchResult, int> searchResultRepository)
        {
            this._searchResultRepository = searchResultRepository;
        }
        
        public async Task<SearchResult> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var result = await this._searchResultRepository.FindAsync(request.Id);
            
            return result;
        }
    }
}