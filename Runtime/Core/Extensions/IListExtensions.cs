using System.Collections;
using System.Collections.Generic;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class IListExtensions
    {
        public static object PickRandom(this IList list)
        {
            return list[XIVRandom.Range(0, list.Count)];
        }

        public static T PickRandom<T>(this IList list)
        {
            return (T)list[XIVRandom.Range(0, list.Count)];
        }

        public static T PickRandom<T>(this IList<T> list)
        {
            return list[XIVRandom.Range(0, list.Count)];
        }
    }
}