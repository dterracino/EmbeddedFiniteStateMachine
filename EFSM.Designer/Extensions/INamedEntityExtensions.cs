using EFSM.Designer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFSM.Designer.Extensions
{
    public static class INamedEntityExtensions
    {
        public static string CreateUniqueName(this IEnumerable<string> existingNames, string format, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            int index = 1;

            string currentName = string.Format(format, index);

            string[] materialized = existingNames.ToArray();

            while (materialized.Any(n => string.Compare(n, currentName, stringComparison) == 0))
            {
                index++;

                currentName = string.Format(format, index);
            }

            return currentName;
        }

        public static string CreateUniqueName<T>(this IEnumerable<T> namedItems, string format, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
            where T : INamedItem
        {
            var existingNames = namedItems.Select(n => n.Name);

            return CreateUniqueName(existingNames, format, stringComparison);
        }
    }
}
