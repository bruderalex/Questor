using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines
{
    public interface ISearchEngine
    {
        public SearchEngineTypeEnum SearchEngineTypeEnum {get;}
        
        string BaseUrl {get;}
        
        Selector Selector {get;}

        Task<RawResult> Search(string question, CancellationToken cancellationToken);
    }
}