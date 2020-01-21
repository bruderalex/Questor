using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questor.Core.Entities;

namespace Questor.Infrasctructure.Data.Configuration
{
    public class SearchResultItemConfiguration : IEntityTypeConfiguration<SearchResultItem>
    {
        public void Configure(EntityTypeBuilder<SearchResultItem> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}