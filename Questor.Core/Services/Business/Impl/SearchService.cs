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
        private Dictionary<int, ISearchEngine> _searchEngines { get; }

        private readonly IAsyncRepository<EngineType> _engineTypeRepository;

        public SearchService(IEnumerable<ISearchEngine> searchEngines, IAsyncRepository<EngineType> engineTypeRepository)
        {
            this._engineTypeRepository = engineTypeRepository;

            this._searchEngines =
                searchEngines.GroupBy(engine => engine.EngineId)
                    .ToDictionary(g => g.Key, g => g.FirstOrDefault());
        }

        public async Task<SearchResult> SearchOnline(string question, IEnumerable<EngineType> enginesTypes = null)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            var engines = enginesTypes == null
                ? this._searchEngines.Values.ToList()
                : enginesTypes.Select(engineType => this._searchEngines[engineType.Id]).ToList();

            var engineSearchTasks = new List<Task<RawResult>>();

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            foreach (var engine in engines)
            {
                engineSearchTasks.Add(engine.Search(question, token));
            }

            var completedSearchTask = await Task.WhenAny(engineSearchTasks);
            var rawResult = await completedSearchTask;
            tokenSource.Cancel();

            return await ParseRawResult(rawResult, question);
        }

        private async Task<SearchResult> ParseRawResult(RawResult rawResult, string question)
        {
            if (rawResult == null)
                throw new ArgumentNullException(nameof(rawResult));

            if (rawResult.SearchEngine == null)
                throw new ArgumentNullException(nameof(rawResult.SearchEngine));

            var engineType = await this._engineTypeRepository.FindAsync(rawResult.SearchEngine.EngineId);
            var browsingContext = BrowsingContext.New();
            var document = await browsingContext.OpenAsync(request => request.Content(rawResult.Content));
            var links = document.QuerySelectorAll(rawResult.SearchEngine.Selector.Url).Take(10);
            var items = new List<SearchResultItem>();

            foreach (var link in links)
            {
                var url = link.QuerySelector(rawResult.SearchEngine.Selector.Url)?.GetAttribute("[href]");
                var title = link.QuerySelector(rawResult.SearchEngine.Selector.Title)?.TextContent;
                var content = link.QuerySelector(rawResult.SearchEngine.Selector.Text)?.TextContent;

                items.Add(new SearchResultItem(engineType, title, content, url));
            }

            var searchResult = new SearchResult(question, items, DateTime.Now);
            
            return searchResult;
        }
    }
}