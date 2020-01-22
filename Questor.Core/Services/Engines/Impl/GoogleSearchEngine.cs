using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Auxiliary;
using Questor.Core.Exceptions;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines.Impl
{
    public class GoogleSearchEngine : ISearchEngine
    {
        private readonly IQuestorLogger<GoogleSearchEngine> _logger;

        public string BaseUrl => "https://google.com/";

        public SearchEngineType SearchEngineType => SearchEngineType.Google;

        private readonly Selector _selector;

        public Selector Selector => this._selector;

        public GoogleSearchEngine(IQuestorLogger<GoogleSearchEngine> logger)
        {
            this._logger = logger;

            this._selector = new Selector
            {
                Url = "a[href]",
                Title = "div.r h3",
                Text = "div.s span.st",
                Links = "div.g  div.rc"
            };
        }

        public async Task<RawResult> Search(string question, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(question))
                throw new ArgumentNullException(nameof(question));

            try
            {
                var baseUri = new Uri($"{BaseUrl}search?q={question}&&num=20");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
                var response = await this._logger.LogTimeAsync(async () => await httpClient.GetAsync(baseUri, cancellationToken));

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException(this);

                var rawContent = await response.Content.ReadAsStringAsync();

                return new RawResult(rawContent, this.SearchEngineType);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(GoogleSearchEngine)} failed");
                return new RawResult(string.Empty, this.SearchEngineType);
            }
        }
    }
}