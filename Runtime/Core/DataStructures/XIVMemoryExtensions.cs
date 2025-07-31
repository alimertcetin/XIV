using System.Collections.Generic;

namespace XIV.Core.DataStructures
{
    public static class XIVMemoryExtensions
    {
        public static XIVMemory<T> AsXIVMemory<T>(this IList<T> array)
        {
            return new XIVMemory<T>(array);
        }
    }
}