using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;
using Questor.Infrasctructure.Mediator;
using Questor.Web.Dto;
using Questor.Web.Models;

namespace Questor.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(new SearchResultVm());
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm]StartSearchDto startSearchDto)
        {
            var searchCommand = new SearchCommand(startSearchDto.Question, startSearchDto.SearchEngineTypes);

            var searchResult = await this._mediator.Send(searchCommand);

            var searchResultsVm = this._mapper.Map<SearchResultVm>(searchResult);
            
            return View("Index", searchResultsVm);
        }

        [HttpPost]
        public async Task<IActionResult> Mark([FromForm] string question)
        {
            await Task.Delay(5000);
            return View("Index", new SearchResultVm());
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