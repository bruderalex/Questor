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

        public SearchEngineType SearchEngineType => SearchEngineType.Bing;

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
                var response = await httpClient.GetAsync(baseUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException(this);

                var rawContent = await response.Content.ReadAsStringAsync();

                return new RawResult(rawContent, this.SearchEngineType);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(BingSearchEngine)} failed");
                return new RawResult(string.Empty, this.SearchEngineType);
            }
        }
    }
}