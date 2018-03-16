using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketVideo
        : IPocketVideoData, IEquatable<IPocketVideoData>
    {
        public string Url { get; set; }

        public int? Height { get; set; }

        public int? Width { get; set; }

        public PocketVideoType Type { get; set; }

        public string TypeRaw { get; set; }

        public string ExternalId { get; set; }

        public override bool Equals(object obj) =>
            obj is IPocketVideoData other ? Equals(other) : base.Equals(obj);

        public bool Equals(IPocketVideoData other) =>
            PocketVideoDataOperations.Equals(this, other);

        public override int GetHashCode() =>
            PocketVideoDataOperations.GetHashCode(this);
    }
}
