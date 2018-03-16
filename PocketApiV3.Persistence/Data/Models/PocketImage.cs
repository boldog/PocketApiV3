using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketImage
        : IPocketImageData, IEquatable<IPocketImageData>
    {
        public string Url { get; set; }
        public string Credit { get; set; }
        public string Caption { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

        public override bool Equals(object obj) =>
            obj is IPocketImageData other ? Equals(other) : base.Equals(obj);

        public bool Equals(IPocketImageData other) =>
            PocketImageDataOperations.Equals(this, other);

        public override int GetHashCode() =>
            PocketImageDataOperations.GetHashCode(this);

        //public static PocketImage CreateCopyFrom(PocketApiV3.PocketImage other)
        //{
        //    if (other == null)
        //        return null;

        //    var result = new PocketImage();
        //    result.CopyFrom(other);
        //    return result;
        //}

        //public static bool operator ==(PocketImage x, PocketImage y) =>
        //    Common.Equals(x, y);

        //public static bool operator !=(PocketImage x, PocketImage y) =>
        //    !Common.Equals(x, y);
    }
}
