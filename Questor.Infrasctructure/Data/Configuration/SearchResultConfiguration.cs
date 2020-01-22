using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data.Configuration
{
    public class SearchResultConfiguration : IEntityTypeConfiguration<SearchResult>
    {
        public void Configure(EntityTypeBuilder<SearchResult> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Question)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Ignore(e => e.SearchEngine);

            var navItem = builder.Metadata.FindNavigation(nameof(SearchResult.SearchResultItems));
            navItem.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}