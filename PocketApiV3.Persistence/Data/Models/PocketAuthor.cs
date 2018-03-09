using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiAuthor = PocketApiV3.PocketAuthor;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketAuthor : IIdentifiable<long>, ICanCopyFrom<ApiAuthor>
    {

        public long Id { get; set; }

        public long PocketItemId { get; set; }
        public virtual PocketItem PocketItem { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public void CopyFrom(PocketApiV3.PocketAuthor other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Id != 0 && Id != other.Id)
                throw new ArgumentException($"The passed {nameof(PocketApiV3.PocketAuthor)} ID \"{other.Id}\" differs from this {nameof(PocketAuthor)} ID \"{Id}\".");

            if (other.Id != null)
                Id = other.Id.Value;

            if (other.ItemId != null)
                PocketItemId = other.ItemId.Value;

            Name = other.Name;
            Url = other.Url;
        }
    }
}
