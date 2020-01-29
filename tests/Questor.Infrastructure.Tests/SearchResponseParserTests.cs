using System;
using System.Collections.Generic;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Questor.Core.Auxiliary;
using Questor.Core.Services.Business;
using Questor.Core.Services.Engines;
using Questor.Core.Services.Engines.Impl;
using Questor.Infrasctructure;
using Questor.Tests.Common;
using Xunit;

namespace Questor.Infrastructure.Tests
{
    public class SearchResponseParserTests
    {
        private IFixture _fixture;

        public SearchResponseParserTests()
        {
            this._fixture = AutoMoqDataAttribute.Fixture;

            InitializeMocks();
        }

        private void InitializeMocks()
        {
            var logger = this._fixture.Create<Mock<IQuestorLogger<GoogleSearchEngine>>>();
            
            this._fixture.Inject<ISearchEngine>(new GoogleSearchEngine(logger.Object));
        }

        [Theory, AutoMoqData]
        public async void Google_ParseRawReponse_Should_Return_Results_On_Valid_Input_HtmlContent()
        {
            var sut = this._fixture.Create<SearchResponseParser>();

            var rawResult = new RawResult(GoogleSearchTestResponse.Content, SearchEngineTypeEnum.Google);

            var result = await sut.ParseRawResponse(rawResult);

            // test content has 2 real results
            result.Should().HaveCount(2);
        }
    }
}