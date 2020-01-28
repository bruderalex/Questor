using MediatR;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchQuery : IRequest<SearchResult>
    {
        public SearchQuery(int id)
        {
            this.Id = id;
        }

        public int Id { get; set; }
    }
}