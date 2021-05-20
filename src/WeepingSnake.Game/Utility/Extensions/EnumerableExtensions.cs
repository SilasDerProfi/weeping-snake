using System;
using System.Collections.Generic;
using System.Linq;

namespace WeepingSnake.Game.Utility.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> source)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var index = random.Next(0, source.Count());

            return source.ElementAt(index);
        }
    }
}
