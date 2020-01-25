﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Questor.Core.Data.Entities;

namespace Questor.Core.Services.Business
{
    public interface ISearchService
    {
        Task<SearchResult> SearchOnline(string question, IEnumerable<SearchEngineTypeEnum> searchEngineTypes = null);
        
        Task<SearchResult> SearchOffline(string question);
    }
}