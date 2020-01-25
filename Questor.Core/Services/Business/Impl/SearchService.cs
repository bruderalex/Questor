using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Engines;

namespace Questor.Core.Services.Business.Impl
{
    public class SearchService : ISearchService
    {
        private Dictionary<int, ISearchEngine> SearchEngines { get; }

        private readonly IAsyncRepository<SearchResult> _searchResultRepository;
        private readonly IAsyncRepository<SearchResultItem> _searchResultItemsRepository;
        
        private readonly ISearchResponseParser _searchResponseParser;

        public SearchService(IEnumerable<ISearchEngine> searchEngines, 
            IAsyncRepository<SearchResult> searchResultRepository,
            IAsyncRepository<SearchResultItem> searchResultItemsRepository,
            ISearchResponseParser searchResponseParser)
        {
            this._searchResultRepository = searchResultRepository;
            this._searchResultItemsRepository = searchResultItemsRepository;
            this._searchResponseParser = searchResponseParser;
            
            this.SearchEngines =
                searchEngines.GroupBy(engine => (int)engine.SearchEngineTypeEnum)
                    .ToDictionary(g => g.Key, g => g.FirstOrDefault());
        }

        public async Task<SearchResult> SearchOnline(string question, IEnumerable<SearchEngineTypeEnum> searchEngineTypes = null)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            var searchEngines = searchEngineTypes == null
                ? this.SearchEngines
                : this.SearchEngines
                    .Where(engine => 
                        searchEngineTypes
                            .Any(engineType => 
                                (int)engineType == engine.Key));

            var engineSearchTasks = new List<Task<RawResult>>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            foreach (var pair in searchEngines)
            {
                engineSearchTasks.Add(pair.Value.Search(question, token));
            }

            var completedSearchTask = await Task.WhenAny(engineSearchTasks);
            var rawResult = await completedSearchTask;
            tokenSource.Cancel();

            var parsedItems = 
                await this._searchResponseParser
                .ParseRawResponse(rawResult);
            
            var searchResult = new SearchResult(question, parsedItems, DateTime.Now, rawResult.SearchEngineTypeEnum);
            
            await this._searchResultRepository.AddAsync(searchResult);
            
            return searchResult;
        }

        public async Task<SearchResult> SearchOffline(string question)
        {
            var items = 
                await this._searchResultItemsRepository
                    .GetListAsync(r => 
                        r.Content.IndexOf(question, StringComparison.Ordinal) != -1
                        || r.Title.IndexOf(question, StringComparison.Ordinal) != -1
                        || r.Url.IndexOf(question, StringComparison.Ordinal) != -1); 
            
            var searchResult = new SearchResult(question, items, DateTime.Now, SearchEngineTypeEnum.Offline);
            
            await this._searchResultRepository.AddAsync(searchResult);
            
            return searchResult;
        }
    }
}