using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketItem
    {
        public int Id { get; set; }

        public long ItemId { get; set; }

        public long ResolvedId { get; set; }

        public string NormalizedUrl { get; set; }

        public string GivenUrl { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public DateTimeOffset TimeAdded { get; set; }

        public bool IsFavorite { get; set; }
        public string Domain { get; set; }
        public string Excerpt { get; set; }

        public SubsumptionRelationship? ImageContent { get; set; }
        public SubsumptionRelationship? VideoContext { get; set; }
        public bool IsArticle { get; set; }

        public int? WordCount { get; set; }

        public string Encoding { get; set; }
        public string MimeType { get; set; }

        public DateTimeOffset TimeAddedToSyncDatabase { get; set; }

        public PocketItemStatus Status { get; set; }



    }
}
