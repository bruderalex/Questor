﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp;
using Questor.Core.Auxiliary;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Engines;

namespace Questor.Core.Services.Business.Impl
{
    ///<inheritdoc cref="ISearchService"/>
    public class SearchService : ISearchService
    {
        private Dictionary<int, ISearchEngine> SearchEngines { get; }

        private readonly IAsyncRepository<SearchResult> _searchResultRepository;
        private readonly IAsyncRepository<SearchResultItem> _searchResultItemsRepository;
        private readonly ISearchResponseParser _searchResponseParser;
        private readonly IQuestorLogger<SearchService> _logger;

        public SearchService(IEnumerable<ISearchEngine> searchEngines,
            IAsyncRepository<SearchResult> searchResultRepository,
            IAsyncRepository<SearchResultItem> searchResultItemsRepository,
            ISearchResponseParser searchResponseParser,
            IQuestorLogger<SearchService> logger)
        {
            this._searchResultRepository = searchResultRepository;
            this._searchResultItemsRepository = searchResultItemsRepository;
            this._searchResponseParser = searchResponseParser;
            this._logger = logger;

            this.SearchEngines =
                searchEngines.GroupBy(engine => (int)engine.SearchEngineTypeEnum)
                    .ToDictionary(g => g.Key, g => g.FirstOrDefault());
        }

        public async Task<SearchResult> SearchOnlineAsync(string question, IEnumerable<SearchEngineTypeEnum> searchEngineTypes = null)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            try
            {
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
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{nameof(SearchOnlineAsync)} method failed");
                return null;
            }
        }

        public async Task<SearchResult> SearchOfflineAsync(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            try
            {
                // just simple search by string;
                // for SQL-server it can be implemented as full-text search
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
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"{nameof(SearchOfflineAsync)} method failed");
                return null;
            }
        }
    }
}