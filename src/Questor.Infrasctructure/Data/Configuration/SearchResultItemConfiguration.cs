using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questor.Core.Data.Entities;
using Questor.Core.Services.Engines;

namespace Questor.Infrasctructure.Data.Configuration
{
    public class SearchResultItemConfiguration : IEntityTypeConfiguration<SearchResultItem>
    {
        public void Configure(EntityTypeBuilder<SearchResultItem> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Id);

            builder.Property(sri => sri.Url)
                .IsRequired();
            
            builder.Property(sri => sri.Title)
                .IsRequired();
            
            builder.Property(sri => sri.Content)
                .IsRequired();
            
            builder.HasOne(e => e.SearchResult)
                .WithMany(r => r.SearchResultItems);
            
            var navItem = builder.Metadata.FindNavigation(nameof(SearchResultItem.SearchResult));
            navItem.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}