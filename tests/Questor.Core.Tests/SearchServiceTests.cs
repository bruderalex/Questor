using System;
using System.Collections;
using System.Threading;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Business;
using Questor.Core.Services.Business.Impl;
using Questor.Core.Services.Engines;
using Questor.Core.Tests.Auxiliary;
using Xunit;

namespace Questor.Core.Tests
{
    public class SearchServiceTests
    {
        private IFixture _fixture;

        public SearchServiceTests()
        {
            this._fixture = AutoMoqDataAttribute.Fixture;

            InitializeMocks();
        }

        private void InitializeMocks()
        {
            this._fixture.Inject<ISearchResultsCache>(new SearchResultsCache());
        }

        [Theory, AutoMoqData]
        public async void Search_Online_Should_Be_Not_Null_On_NonEmpty_Request(string request)
        {
            var sut = this._fixture.Create<SearchService>();

            var searchResult = await sut.SearchOnlineAsync(request);

            searchResult.Should().NotBeNull();
        }

        [Theory,
         InlineAutoMoqData(""),
         InlineAutoMoqData(" ")]
        public void Search_Online_Should_Throw_Exception_On_Empty_Request(string request)
        {
            var sut = this._fixture.Create<SearchService>();

            sut.Invoking(async s => await s.SearchOnlineAsync(request))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory, AutoMoqData]
        public async void Search_Service_Get_Should_Return_Same_As_Searched_Result(string request)
        {
            var sut = this._fixture.Create<SearchService>();

            var savedSearchResult = await sut.SearchOnlineAsync(request);

            var retreivedSearchResult = sut.GetSearchResultByUniqueId(savedSearchResult.UniqueId);

            savedSearchResult.Should().Be(retreivedSearchResult);
        }
    }
}