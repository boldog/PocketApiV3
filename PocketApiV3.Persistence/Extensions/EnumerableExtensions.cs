using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketApiV3.Persistence.Extensions
{
    static class EnumerableExtensions
    {

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> @this) =>
            new HashSet<T>(@this);

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> @this, IEqualityComparer<T> equalityComparer) =>
            new HashSet<T>(@this, equalityComparer);
    }
}
