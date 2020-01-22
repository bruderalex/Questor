using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Questor.Core.Auxiliary;
using Questor.Core.Exceptions;
using Questor.Core.Services.Business;

namespace Questor.Core.Services.Engines.Impl
{
    public class YandexSearchEngine : ISearchEngine
    {
        private readonly IQuestorLogger<YandexSearchEngine> _logger;

        public string BaseUrl => "https://yandex.ru/";

        public SearchEngineType SearchEngineType => SearchEngineType.Yandex;

        public YandexSearchEngine(IQuestorLogger<YandexSearchEngine> logger)
        {
            this._logger = logger;

            this._selector = new Selector
            {
                Url = "a[href]",
                Title = "div.organic__url-text",
                Text = "div.organic__url-text",
                Links = "li.serp-item > div"
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

                var baseUri = new Uri($"{BaseUrl}search?text={question}");

                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(baseUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException(this);

                var rawContent = await response.Content.ReadAsStringAsync();

                this._logger.LogInfo($"{nameof(YandexSearchEngine)}: search completed in {stopwatch.ElapsedMilliseconds}");
                stopwatch.Stop();

                return new RawResult(rawContent, this);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(YandexSearchEngine)} failed");
                return new RawResult(string.Empty, this);
            }
        }
    }
}