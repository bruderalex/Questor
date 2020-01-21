using Autofac;
using Questor.Core.Auxiliary;
using Questor.Core.Services.Business;
using Questor.Core.Services.Business.Impl;
using Questor.Core.Services.Engines;
using Questor.Infrasctructure.Logger;

namespace Questor.Web.Modules
{
    public class BaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ISearchEngine).Assembly)
                .Where(type => typeof(ISearchEngine).IsAssignableFrom(type))
                .AsImplementedInterfaces();

            builder.RegisterType<SearchService>()
                .As<ISearchService>();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(IQuestorLogger<>));
        }
    }
}