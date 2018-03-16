using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    public static class PocketImageDataOperations
    {
        public static bool Copy(IPocketImageData source, IPocketImageData destination)
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

            if (destination.Credit != source.Credit)
            {
                destination.Credit = source.Credit;
                changed = true;
            }

            if (destination.Caption != source.Caption)
            {
                destination.Caption = source.Caption;
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

            return changed;
        }

        public static bool Equals(IPocketImageData x, IPocketImageData y)
        {
            if (ReferenceEquals(x, y))
                return true;
            if (x is null || y is null)
                return false;

            return Equals(x.Url, y.Url)
                && Equals(x.Credit, y.Credit)
                && Equals(x.Caption, y.Caption)
                && x.Height.Equals(y.Height)
                && x.Width.Equals(y.Width);
        }

        public static int GetHashCode(IPocketImageData obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                var hashCode = 47;
                if (obj.Url != null)
                    hashCode = (hashCode * 53) ^ obj.Url.GetHashCode();
                if (obj.Credit != null)
                    hashCode = (hashCode * 53) ^ obj.Credit.GetHashCode();
                if (obj.Caption != null)
                    hashCode = (hashCode * 53) ^ obj.Caption.GetHashCode();
                hashCode = (hashCode * 53) ^ obj.Height.GetHashCode();
                hashCode = (hashCode * 53) ^ obj.Width.GetHashCode();
                return hashCode;
            }
        }
    }
}
