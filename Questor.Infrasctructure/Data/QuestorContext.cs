using Microsoft.EntityFrameworkCore;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data
{
    public class QuestorContext : DbContext
    {
        public QuestorContext()
        {
            
        }
        
        public QuestorContext(DbContextOptions<QuestorContext> options)
            : base(options)
        {
        }

        public DbSet<EngineType> SearchEngines { get; set; }

        public DbSet<SearchResult> SearchResults { get; set; }

        public DbSet<SearchResultItem> SearchResultItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("QuestorDB");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestorContext).Assembly);
        }
    }
}