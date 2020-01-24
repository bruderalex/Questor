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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<SearchResult> _searchRepo;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IMapper mapper, IAsyncRepository<SearchResult> searchRepo)
        {
            this._logger = logger;
            this._mediator = mediator;
            this._mapper = mapper;
            this._searchRepo = searchRepo;
        }

        public async Task<IActionResult> Index(string id)
        {
            var vm = new SearchResultVm();
            vm.InitializeSelectedEngines();

            if (!string.IsNullOrWhiteSpace(id))
            {
                var result = await this._searchRepo.FindAsync(int.Parse(id));
                if (result != null)
                {
                    vm = this._mapper.Map<SearchResultVm>(result);
                    vm.InitializeSelectedEngines();
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm]StartSearchDto startSearchDto)
        {
            var searchCommand = new SearchCommand(startSearchDto.Question, startSearchDto.SearchEngineTypes);

            var searchResult = await this._mediator.Send(searchCommand);

            return RedirectToAction("Index", new {id = searchResult.Id});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}