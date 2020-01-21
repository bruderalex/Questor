using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Engines
{
    public interface ISearchEngine
    {
        public int EngineId {get;}
        
        string BaseUrl {get;}
        
        Selector Selector {get;}

        Task<RawResult> Search(string question, CancellationToken cancellationToken);
    }
}