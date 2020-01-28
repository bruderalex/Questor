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
                .ForMember(src => src.EngineTypeName,
                    dst =>
                        dst.MapFrom(src => Enum.GetName(typeof(SearchEngineTypeEnum), src.EngineTypeEnum)));
        }
    }
}