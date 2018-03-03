using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PocketApiV3.Persistence.Data;

namespace PocketApiV3.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Beginning migration...");
            using (var db = new SyncDbContext())
            {
                await db.Database.MigrateAsync();
            }
            Console.WriteLine("Done.");
        }
    }
}
