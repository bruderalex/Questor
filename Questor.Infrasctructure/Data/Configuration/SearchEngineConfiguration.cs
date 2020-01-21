using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data.Configuration
{
    public class SearchEngineConfiguration : IEntityTypeConfiguration<EngineType>
    {
        public void Configure(EntityTypeBuilder<EngineType> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}