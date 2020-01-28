using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Questor.Core.Data;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;
using Questor.Infrasctructure.Data;
using Questor.Infrasctructure.Mediator;
using Questor.Web.Dto;
using Questor.Web.Models;

namespace Questor.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchController(ILogger<SearchController> logger, IMediator mediator, IMapper mapper)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new SearchResultVm();
            vm.InitializeSelectedEngines();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm] StartSearchDto startSearchDto)
        {
            SearchResult searchResult;
            
            if (startSearchDto.SearchPlace.Equals("online"))
            {
                var searchCommand = new SearchOnlineCommand(startSearchDto.Question, startSearchDto.SearchEngineTypes);
                searchResult = await this._mediator.Send(searchCommand);
            }
            else
            {
                var searchCommand = new SearchOfflineCommand(startSearchDto.Question);
                searchResult = await this._mediator.Send(searchCommand);
            }
            
            return RedirectToAction("Search", new {searchResultId = searchResult.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Search(int searchResultId)
        {
            var command = new SearchQuery(searchResultId);
            
            var searchResult = await this._mediator.Send(command);
            
            var vm = this._mapper.Map<SearchResultVm>(searchResult);
            
            return PartialView("SearchResults", vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}