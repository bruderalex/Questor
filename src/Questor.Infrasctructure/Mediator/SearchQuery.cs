using System;
using MediatR;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchQuery : IRequest<SearchResult>
    {
        public SearchQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}