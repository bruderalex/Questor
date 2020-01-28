using System.Runtime.CompilerServices;
using System.Threading;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Engines;

namespace Questor.Tests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        // public static readonly IFixture CustomizedFixture;

        // static AutoMoqDataAttribute()
        // {
        //     CustomizedFixture = new Fixture().Customize(new AutoMoqCustomization());
        //
        //     CustomizedFixture.Freeze<Mock<ISearchResponseParser>>()
        //         .Setup(_ => _.ParseRawResponse(It.IsNotNull<RawResult>()))
        //         .ReturnsAsync(CustomizedFixture.CreateMany<SearchResultItem>(10));
        //
        //     CustomizedFixture.Freeze<Mock<ISearchEngine>>()
        //         .Setup(_ => _.Search(It.IsNotNull<string>(), It.IsAny<CancellationToken>()))
        //         .ReturnsAsync(CustomizedFixture.Create<RawResult>());
        // }

        public AutoMoqDataAttribute()
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}