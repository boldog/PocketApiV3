using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3.Persistence
{
    static class Common
    {
        public static bool Equals<T>(T x, T y)
            where T : class, IEquatable<T>
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null)
                return false;
            return x.Equals(y);
        }

    }
}
