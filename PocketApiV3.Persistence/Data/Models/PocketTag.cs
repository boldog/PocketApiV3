using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketTag
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public ICollection<PocketItemTag> Items { get; set; } = new List<PocketItemTag>();
    }
}
