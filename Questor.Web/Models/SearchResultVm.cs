using System;
using System.Collections.Generic;
using Questor.Core.Data.Entities;

namespace Questor.Web.Models
{
    public class SearchResultVm
    {
        public string Question { get; set; }

        public DateTime Date { get; set; }

        public List<SearchResultItem> SearchResultItems { get; set; }
        
        public string EngineTypeName {get;set;}
    }
}