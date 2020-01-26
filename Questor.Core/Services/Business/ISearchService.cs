using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Business
{
    /// <summary>
    ///     Mains seach service, responsible for an online and offline searches 
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        ///     Online search
        /// </summary>
        /// <param name="question">
        ///    Word or phrase to search
        /// </param>
        /// <param name="searchEngineTypes">
        ///    Engines, which must be used in search process;
        ///    If <code>null</code> - all engines are involved in search process
        /// </param>
        /// <returns>
        ///    Search result, containing results
        /// </returns>
        Task<SearchResult> SearchOnlineAsync(string question, IEnumerable<SearchEngineTypeEnum> searchEngineTypes = null);

        /// <summary>
        ///     Offline search
        /// </summary>
        /// <param name="question">
        ///    Word or phrase to search
        /// </param>
        /// <returns>
        ///    Search result
        /// </returns>
        Task<SearchResult> SearchOfflineAsync(string question);
    }
}