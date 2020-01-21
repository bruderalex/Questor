using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questor.Core.Entities;

namespace Questor.Infrasctructure.Data.Configuration
{
    public class SearchEngineConfiguration : IEntityTypeConfiguration<SearchEngine>
    {
        public void Configure(EntityTypeBuilder<SearchEngine> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}