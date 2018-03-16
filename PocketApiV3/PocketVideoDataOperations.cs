using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public static class PocketVideoDataOperations
    {
        public static bool Copy(IPocketVideoData source, IPocketVideoData destination)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            bool changed = false;

            if (destination.Url != source.Url)
            {
                destination.Url = source.Url;
                changed = true;
            }

            if (destination.Height != source.Height)
            {
                destination.Height = source.Height;
                changed = true;
            }

            if (destination.Width != source.Width)
            {
                destination.Width = source.Width;
                changed = true;
            }

            if (destination.TypeRaw != source.TypeRaw)
            {
                destination.TypeRaw = source.TypeRaw;
                changed = true;
            }

            if (destination.ExternalId != source.ExternalId)
            {
                destination.ExternalId = source.ExternalId;
                changed = true;
            }

            return changed;
        }

        public static bool Equals(IPocketVideoData x, IPocketVideoData y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;

            return Equals(x.Url, y.Url)
                && x.Height.Equals(y.Height)
                && x.Width.Equals(y.Width)
                && Equals(x.TypeRaw, y.TypeRaw)
                && Equals(x.ExternalId, y.ExternalId);
        }

        public static int GetHashCode(IPocketVideoData obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                var hashCode = 67;
                if (obj.Url != null)
                    hashCode = (hashCode * 53) ^ obj.Url.GetHashCode();
                hashCode = (hashCode * 53) ^ obj.Height.GetHashCode();
                hashCode = (hashCode * 53) ^ obj.Width.GetHashCode();
                if (obj.TypeRaw != null)
                    hashCode = (hashCode * 53) ^ obj.TypeRaw.GetHashCode();
                if (obj.ExternalId != null)
                    hashCode = (hashCode * 53) ^ obj.ExternalId.GetHashCode();
                return hashCode;
            }
        }
    }
}
