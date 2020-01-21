using Microsoft.EntityFrameworkCore;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data
{
    public class QuestorContext : DbContext
    {
        public QuestorContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SearchEngine> SearchEngines { get; set; }

        public DbSet<SearchResult> SearchResults { get; set; }

        public DbSet<SearchResultItem> SearchResultItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestorContext).Assembly);
        }
    }
}