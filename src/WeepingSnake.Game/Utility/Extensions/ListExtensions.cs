using System.Collections.Generic;

namespace WeepingSnake.Game.Utility.Extensions
{
    public static class ListExtensions
    {
        public static T AddAndReturn<T>(this List<T> list, T newItem)
        {
            list.Add(newItem);
            return newItem;
        }
    }
}
