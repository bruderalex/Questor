using System.Collections.Generic;
using MediatR;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Infrasctructure.Mediator
{
    public class SearchOnlineCommand : IRequest<SearchResult>
    {
        public SearchOnlineCommand(string question, IEnumerable<SearchEngineTypeEnum> engineTypes = null)
        {
            this.Question = question;
            this.EngineTypes = engineTypes;
        }
        
        public string Question {get;set;}
        
        public IEnumerable<SearchEngineTypeEnum> EngineTypes {get;set;}
    }
}