using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public static class PocketAuthorDataOperations
    {
        public static bool Copy(IPocketAuthorData source, IPocketAuthorData destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            bool changed = false;

            if (destination.Name != source.Name)
            {
                destination.Name = source.Name;
                changed = true;
            }

            if (destination.Url != source.Url)
            {
                destination.Url = source.Url;
                changed = true;
            }

            return changed;
        }

        public static bool Equals(IPocketAuthorData x, IPocketAuthorData y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;

            return Equals(x.Name, y.Name)
                && Equals(x.Url, y.Url);
        }

        public static int GetHashCode(IPocketAuthorData obj)
        {
            if (obj is null)
                return 0;

            unchecked
            {
                var hashCode = 61;
                if (obj.Name != null)
                    hashCode = (hashCode * 53) ^ obj.Name.GetHashCode();
                if (obj.Url != null)
                    hashCode = (hashCode * 53) ^ obj.Url.GetHashCode();
                return hashCode;
            }
        }

    }
}
