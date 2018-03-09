using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketVideo : IIdentifiable<long>, ICanCopyFrom<PocketApiV3.PocketVideo>
    {
        public long Id { get; set; }

        public long PocketItemId { get; set; }
        public virtual PocketItem PocketItem { get; set; }

        public string Url { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public PocketVideoType Type { get; set; }

        public string TypeRaw { get; set; }

        public string ExternalId { get; set; }

        public void CopyFrom(PocketApiV3.PocketVideo other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Id != 0 && Id != other.Id)
                throw new ArgumentException($"The passed {nameof(PocketApiV3.PocketVideo)} ID \"{other.Id}\" differs from this {nameof(PocketImage)} ID \"{Id}\".");

            if (other.Id != null)
                Id = other.Id.Value;

            if (other.ItemId != null)
                PocketItemId = other.ItemId.Value;

            Url = other.Url;
            Height = other.Height;
            Width = other.Width;
            Type = other.Type;
            TypeRaw = other.TypeRaw;
            ExternalId = other.ExternalId;
        }
    }
}
