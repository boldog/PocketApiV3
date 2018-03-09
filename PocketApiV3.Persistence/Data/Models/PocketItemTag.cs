using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketItemTag
    {
        public long PocketItemId { get; set; }
        public PocketItem PocketItem { get; set; }


        public long PocketTagId { get; set; }
        public PocketTag PocketTag { get; set; }
    }
}
