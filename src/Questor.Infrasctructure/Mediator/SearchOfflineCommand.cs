using MediatR;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchOfflineCommand : IRequest<SearchResult>
    {
        public SearchOfflineCommand(string question)
        {
            this.Question = question;
        }

        public string Question { get; set; }
    }
}