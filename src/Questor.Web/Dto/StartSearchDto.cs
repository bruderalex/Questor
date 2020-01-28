using System;
using System.Collections.Generic;
using System.Linq;
using Questor.Core.Services.Business;
using Questor.Web.Models;

namespace Questor.Web.Dto
{
    public class StartSearchDto
    {
        public string Question { get; set; }

        public IEnumerable<SelectedEngineVm> SelectedEngines {get;set;}
        
        public string SearchPlace {get;set;}
        
        public IEnumerable<SearchEngineTypeEnum> SearchEngineTypes => 
            this.SelectedEngines.Where(searchEngineVm => searchEngineVm.Checked)
                .Select(engineTypeEnum => (SearchEngineTypeEnum)engineTypeEnum.Id);
    }
}