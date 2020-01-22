using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;
using Questor.Web.Models;

namespace Questor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISearchService _searchService;

        public HomeController(ILogger<HomeController> logger, ISearchService searchService)
        {
            this._logger = logger;
            this._searchService = searchService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await this._searchService.SearchOnline("123", new List<SearchEngineType> {SearchEngineType.Yandex, SearchEngineType.DuckDuckGo});

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}