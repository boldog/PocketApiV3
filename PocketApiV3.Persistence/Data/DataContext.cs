using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiteDB;
using PocketApiV3.Persistence.Data.Models;

namespace PocketApiV3.Persistence.Data
{
    public class DataContext : IDisposable
    {

        public DataContext()
        {
            var bsonMapper = new BsonMapper();

            _db = new LiteDatabase("PocketApiV3.db", bsonMapper);
            LiteCollection<PocketItem> c = _db.GetCollection<PocketItem>();

        }

        public void Dispose() => Dispose(true);


        LiteDatabase _db;

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                Interlocked.Exchange(ref _db, null)?.Dispose();
            }
        }
    }
}
