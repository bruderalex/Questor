using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Auxiliary;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Exceptions;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines.Impl
{
    public class DuckduckGoSearchEngine : ISearchEngine
    {
        private readonly IQuestorLogger<DuckduckGoSearchEngine> _logger;

        public string BaseUrl => "https://duckduckgo.com/html/";

        public SearchEngineTypeEnum SearchEngineTypeEnum => SearchEngineTypeEnum.DuckDuckGo;

        public DuckduckGoSearchEngine(IQuestorLogger<DuckduckGoSearchEngine> logger)
        {
            this._logger = logger;

            this._selector = new Selector
            {
                Url = "a.result__a",
                Title = "h2.result__title a",
                Text = "a.result__snippet",
                Links = "div.results div.result.results_links.results_links_deep.web-result"
            };
        }

        private readonly Selector _selector;

        public Selector Selector => this._selector;

        public async Task<RawResult> Search(string question, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            try
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                
                var baseUri = new Uri(BaseUrl);

                var requestParams = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("q", question),
                    new KeyValuePair<string, string>("b", ""),
                    new KeyValuePair<string, string>("kl", "us-en"),
                });

                using var httpClient = new HttpClient();
                var response = await httpClient.PostAsync(baseUri, requestParams, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException($"{nameof(DuckduckGoSearchEngine)} didn't return Ok 200");

                var rawContent = await response.Content.ReadAsStringAsync();

                this._logger.LogInfo($"{nameof(DuckduckGoSearchEngine)} search completed in {stopwatch.ElapsedMilliseconds}");
                stopwatch.Stop();
                
                return new RawResult(rawContent, this.SearchEngineTypeEnum);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(DuckduckGoSearchEngine)} failed");
                return new RawResult(string.Empty, this.SearchEngineTypeEnum);
            }
        }
    }
}