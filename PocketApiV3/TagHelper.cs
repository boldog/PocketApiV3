using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketApiV3
{
    static class TagHelper
    {
        const string Delimiter = ",";
        static readonly string[] DelimiterArray = new string[] { Delimiter };

        public static string Join(IEnumerable<string> tags)
        {
            if (tags == null)
                return null;

            var cleaned = tags.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim());
            var result = string.Join(Delimiter, cleaned);
            return result;
        }

        public static string[] Split(string tags)
        {
            if (tags == null)
                return null;

            var parts = tags.Split(DelimiterArray, StringSplitOptions.RemoveEmptyEntries);
            var result = parts.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
            return result;
        }
    }
}
