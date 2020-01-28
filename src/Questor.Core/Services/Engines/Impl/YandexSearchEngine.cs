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

        public SearchEngineTypeEnum SearchEngineTypeEnum => SearchEngineTypeEnum.Yandex;

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
                httpClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:72.0) Gecko/20100101 Firefox/72.0");
                var response = await httpClient.GetAsync(baseUri, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    throw new SearchEngineException($"{nameof(YandexSearchEngine)} didn't return Ok 200");

                var rawContent = await response.Content.ReadAsStringAsync();

                this._logger.LogInfo($"{nameof(YandexSearchEngine)}: search completed in {stopwatch.ElapsedMilliseconds}");
                stopwatch.Stop();

                
                
                return new RawResult(rawContent, this.SearchEngineTypeEnum);
            }
            catch (TaskCanceledException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"response to {nameof(YandexSearchEngine)} failed");
                return new RawResult(string.Empty, this.SearchEngineTypeEnum);
            }
        }
    }
}