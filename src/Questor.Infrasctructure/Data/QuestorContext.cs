﻿using System;
using Microsoft.Data.SqlClient;
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
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(QuestorContext).Assembly);
        }
    }
}