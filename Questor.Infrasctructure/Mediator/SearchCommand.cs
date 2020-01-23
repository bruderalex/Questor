using System.Collections.Generic;
using MediatR;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchCommand : IRequest<SearchResult>
    {
        public SearchCommand(string question, List<SearchEngineType> engineTypes = null)
        {
            this.Question = question;
            this.EngineTypes = engineTypes;
        }
        
        public string Question {get;set;}
        
        public List<SearchEngineType> EngineTypes {get;set;}
    }
}