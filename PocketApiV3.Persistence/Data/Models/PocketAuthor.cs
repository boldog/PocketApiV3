using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence.Data.Models
{
    public class PocketAuthor
        : IPocketAuthorData, IEquatable<IPocketAuthorData>
    {
        //public long Id { get; set; }

        public PocketAuthor(PocketApiV3.PocketAuthor apiAuthor) =>
            PocketAuthorDataOperations.Copy(apiAuthor, this);

        public string Name { get; set; }

        public string Url { get; set; }

        public override bool Equals(object obj) =>
            obj is IPocketAuthorData other ? Equals(other) : base.Equals(obj);

        public bool Equals(IPocketAuthorData other) =>
            PocketAuthorDataOperations.Equals(this, other);
        public override int GetHashCode() =>
            PocketAuthorDataOperations.GetHashCode(this);

        //public static bool operator ==(PocketAuthor x, PocketAuthor y) =>
        //    Common.Equals(x, y);

        //public static bool operator !=(PocketAuthor x, PocketAuthor y) =>
        //    !Common.Equals(x, y);
    }
}
