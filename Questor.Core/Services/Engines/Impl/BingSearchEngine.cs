using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Auxiliary;
using Questor.Core.Exceptions;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines.Impl
{
    public class BingSearchEngine : ISearchEngine
    {
        private IQuestorLogger<BingSearchEngine> _logger;

        public string BaseUrl => "https://bing.com/";

        public SearchEngineTypeEnum SearchEngineTypeEnum => SearchEngineTypeEnum.Bing;

        public BingSearchEngine(IQuestorLogger<BingSearchEngine> logger)
        {
            this._logger = logger;

            this._selector = new Selector
            {
                Url = "a[href]",
                Title = "a",
                Text = "p",
                Links = "ol#b_results > li.b_algo",
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
                var baseUri = new Uri($"{BaseUrl}search?q={question}");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
                httpClient.DefaultRequestHeaders.Add("Accept", @"text/html,application/xhtml+xml,application/xml");
                httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", @"1");
                httpClient.DefaultRequestHeaders.Add("TE", @"Trailers");
                var response = await httpClient.GetAsync(baseUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException(this);

                var rawContent = await response.Content.ReadAsStringAsync();

                return new RawResult(rawContent, this.SearchEngineTypeEnum);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(BingSearchEngine)} failed");
                return new RawResult(string.Empty, this.SearchEngineTypeEnum);
            }
        }
    }
}