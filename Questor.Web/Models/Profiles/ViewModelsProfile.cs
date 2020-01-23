using System;
using AutoMapper;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;

namespace Questor.Web.Models.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<SearchResult, SearchResultVm>()
                .ForMember(dest =>
                        dest.EngineTypeName,
                    cfg =>
                        cfg.MapFrom(src => Enum.GetName(typeof(SearchEngineType), src.SearchEngineType)));
        }
    }
}