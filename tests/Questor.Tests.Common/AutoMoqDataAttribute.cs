using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Questor.Tests.Common
{
public class AutoMoqDataAttribute : AutoDataAttribute
    {
        private static IFixture _fixture;

        public static IFixture Fixture => FixtureFactory();

        private static Func<IFixture> FixtureFactory =>
            () => { return _fixture ??= new Fixture().Customize(new AutoMoqCustomization {ConfigureMembers = true}); };

        public AutoMoqDataAttribute()
            : base(FixtureFactory)
        {
        }
    }
}