﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using Questor.Core.Data.Entities;
using Questor.Core.Exceptions;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines.Impl
{
    public class SearchResponseParser : ISearchResponseParser
    {
        private readonly IEnumerable<ISearchEngine> _searchEngines;

        public SearchResponseParser(IEnumerable<ISearchEngine> searchEngines)
        {
            this._searchEngines = searchEngines;
        }

        public async Task<IEnumerable<SearchResultItem>> ParseRawResponse(RawResult rawResult)
        {
            if (rawResult == null)
                throw new ArgumentNullException(nameof(rawResult));

            var searchEngine =
                this._searchEngines
                    .FirstOrDefault(engine => engine.SearchEngineTypeEnum == rawResult.SearchEngineTypeEnum);

            if (searchEngine == null)
                throw new UnknownSearchEngineException(Enum.GetName(typeof(SearchEngineTypeEnum), rawResult.SearchEngineTypeEnum));

            var browsingContext = BrowsingContext.New();
            var document = await browsingContext.OpenAsync(request => request.Content(rawResult.Content));
            var links = document.QuerySelectorAll(searchEngine.Selector.Links).Take(10);
            var l1 = document.QuerySelectorAll(searchEngine.Selector.Text).Take(10);
            var items = new List<SearchResultItem>();

            foreach (var link in links)
            {
                var url = link.QuerySelector(searchEngine.Selector.Url)?.GetAttribute("href");
                var title = link.QuerySelector(searchEngine.Selector.Title)?.TextContent;
                var content = link.QuerySelector(searchEngine.Selector.Text)?.TextContent;

                items.Add(new SearchResultItem(title, content, url));
            }

            return items;
        }
    }
}