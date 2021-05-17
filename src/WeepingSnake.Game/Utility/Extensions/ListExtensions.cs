using System;
using System.Collections.Generic;

namespace WeepingSnake.Game.Utility.Extensions
{
    public static class ListExtensions
    {
        public static T AddAndReturn<T>(this List<T> source, T newItem)
        {
            source.Add(newItem);
            return newItem;
        }

        public static IEnumerable<T> GetInfiniteEnumerator<T>(this List<T> source)
        {
            // Iterate over the entire list again and again
            for (var index = 0; index < source.Count; index = (index + 1) % source.Count)
            {
                yield return source[index];
            }
        }

        public static T GetOrCreate<T>(this List<T> source, int index, Func<T> createFunction)
        {
            while (source.Count <= index)
                source.Add(createFunction());

            return source[index];
        }
    }
}
