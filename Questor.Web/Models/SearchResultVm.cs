using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Web.Models
{
    [Serializable]
    public class SearchResultVm
    {
        public SearchResultVm()
        {
        }

        public void InitializeSelectedEngines()
        {
            this.SelectedEngines =
                Enum.GetValues(typeof(SearchEngineTypeEnum))
                    .Cast<SearchEngineTypeEnum>()
                    .Select(engineType => new SelectedEngineVm
                    {
                        Id = (int)engineType,
                        Name = Enum.GetName(typeof(SearchEngineTypeEnum), engineType),
                        Checked = false
                    })
                    .ToList();
        }

        public string Question { get; set; }

        public DateTime Date { get; set; }

        public List<SearchResultItem> SearchResultItems { get; set; } = new List<SearchResultItem>();

        public string EngineTypeName { get; set; }

        public IList<SelectedEngineVm> SelectedEngines { get; set; } = new List<SelectedEngineVm>();
    }

    [Serializable]
    public class SelectedEngineVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Checked { get; set; }
    }
}