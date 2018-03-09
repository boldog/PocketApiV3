using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketImage : IIdentifiable<long>, ICanCopyFrom<PocketApiV3.PocketImage>
    {
        public long Id { get; set; }

        public long PocketItemId { get; set; }
        public virtual PocketItem PocketItem { get; set; }


        public string Url { get; set; }
        public string Credit { get; set; }
        public string Caption { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

        public void CopyFrom(PocketApiV3.PocketImage other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (Id != 0 && Id != other.Id)
                throw new ArgumentException($"The passed {nameof(PocketApiV3.PocketImage)} ID \"{other.Id}\" differs from this {nameof(PocketImage)} ID \"{Id}\".");

            if (other.Id != null)
                Id = other.Id.Value;

            if (other.ItemId != null)
                PocketItemId = other.ItemId.Value;

            Url = other.Url;
            Credit = other.Credit;
            Caption = other.Caption;
            Height = other.Height;
            Width = other.Width;
        }
    }
}
