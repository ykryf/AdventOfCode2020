using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Transforms a char collection to a string by joining the characters.
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string ToStringAlt(this ICollection<char> collection)
        {
            return string.Join("", collection);
        }

        public static ICollection<T> Clone<T>(this ICollection<T> cloneables) where T: class, ICloneable
        {
            var newList = new List<T>();
            foreach (var element in cloneables)
            {
                newList.Add(element.Clone() as T);
            }
            return newList;
        }

        public static ICollection<T> SkipAt<T>(this ICollection<T> collection, int index, int count = 1)
        {
            var newList = new List<T>();
            for (int i = 0; i < collection.Count; i++)
            {
                if (i >= index && i < index + count)
                {
                    continue;
                }
                newList.Add(collection.ElementAt(i));
            }
            return newList;
        }

        /// <summary>
        /// Returns a string composed by the common characters of a collection of strings
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string GetIntersection(this ICollection<string> collection)
        {
            char[] result = collection.First()?.ToCharArray();
            for (int i = 1; i < collection.Count; i++)
            {
                if (result == null || result.Length == 0)
                {
                    return "";
                }
                result = collection.ElementAt(i).Intersect(result).ToArray();
            }
            return result.ToStringAlt();
        }
    }
}
