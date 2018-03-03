using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PocketApiV3.Persistence.Data
{
    public class SyncDbContext : DbContext
    {
        public DbSet<Models.PocketItem> Items { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlite("Data Source=presto.db");
        }
    }
}
