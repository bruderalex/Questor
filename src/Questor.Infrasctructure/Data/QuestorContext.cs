using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data
{
    public class QuestorContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public QuestorContext()
        {
            
        }
        
        public QuestorContext(DbContextOptions<QuestorContext> options,
            IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
        }

        public DbSet<SearchResult> SearchResults { get; set; }

        public DbSet<SearchResultItem> SearchResultItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("QuestorDb"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestorContext).Assembly);
        }
    }
}