using System;
using XIV.Core.DataStructures;

namespace XIV.Core.Extensions
{
    public static class ArrayExtensions
    {
        /// <summary>
        /// Moves the items to the beginning of the array and creates a new <see cref="XIVMemory{T}"/> that has filtered items.
        /// This modifies the original array. You can use this if order doesn't matter.
        /// </summary>
        /// <returns>A new <see cref="XIVMemory{T}"/> that has filtered items</returns>
        public static XIVMemory<T> FilterBy<T>(this T[] array, int arrLen, Func<T, bool> func)
        {
            int left = 0; // Start of the array
            int right = arrLen - 1; // End of the array
            int count = 0;

            while (left <= right)
            {
                while (left < arrLen && func(array[left]))
                {
                    left++;
                    count++;
                }

                while (right >= 0 && func(array[right]) == false)
                {
                    right--;
                }

                // Swap if there are valid positions to swap
                if (left < right)
                {
                    (array[left], array[right]) = (array[right], array[left]);
                }
            }
            return new XIVMemory<T>(array, 0, count);
        }
        
        /// <summary>
        /// <inheritdoc cref="FilterBy{T}(T[], int, Func{T, bool})"/>
        /// </summary>
        /// <returns>A new <see cref="XIVMemory{T}"/> that has filtered items</returns>
        public static XIVMemory<T> FilterBy<T>(this T[] array, Func<T, bool> func) => FilterBy(array, array.Length, func);


        /// <summary>
        /// Finds the closest item to the given position based on a custom position retrieval function, and returns it.
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="arr">The array to look</param>
        /// <param name="arrLen">The length of the <paramref name="arr"/></param>
        /// <param name="currPos">Current position to compare distances</param>
        /// <param name="closestPoint">Closest point that is returned from <paramref name="getTPosFunc"/></param>
        /// <param name="getTPosFunc">Position retrieval function</param>
        /// </summary>
        public static T GetClosest<T>(this T[] arr, int arrLen, Vec3 currPos, out float distance, out Vec3 closestPoint, Func<T, Vec3> getTPosFunc)
        {
            var closest = default(T);
            closestPoint = default(Vec3);
            distance = float.MaxValue;
            for (int i = 0; i < arrLen; i++)
            {
                var pos = getTPosFunc(arr[i]);
                var dist = (currPos - pos).sqrMagnitude;
                if (dist < distance)
                {
                    closest = arr[i];
                    closestPoint = pos;
                    distance = dist;
                }
            }
            return closest;
        }
        
        /// <summary>
        /// <inheritdoc cref="GetClosest{T}(T[], int, Vec3, out float, out Vec3, Func{T, Vec3})"/>
        /// </summary>
        public static T GetClosest<T>(this T[] arr, int arrLen, Vec3 currPos, Func<T, Vec3> getTPosFunc)
        {
            return GetClosest(arr, arrLen, currPos, out _, out _, getTPosFunc);
        }
        
        public static bool Contains<T>(this T[] array, int arrLen, T item, out int index)
        {
            index = -1;
            for (int i = 0; i < arrLen; i++)
            {
                if (item.Equals(array[i]))
                {
                    index = i;
                    return true;
                }
            }

            return false;
        }

        public static bool Contains<T>(this T[] array, int arrLen, T item)
        {
            return Contains(array, arrLen, item, out _);
        }

        public static T[] Split<T>(this T[] array, int arrLen, Func<T, bool> condition)
        {
            T[] arr = new T[arrLen];

            for (int i = 0, j = 0; i < arrLen; i++)
            {
                if (condition.Invoke(array[i]))
                {
                    arr[j++] = array[i];
                }
            }
            return arr;
        }

        public static int Count<T>(this T[] array, int arrLen, Func<T, bool> condition)
        {
            int count = 0;
            for (int i = 0; i < arrLen; i++)
            {
                if (condition.Invoke(array[i])) count++;
            }
            return count;
        }

        public static T[] RemoveAt<T>(this T[] arr, int arrLen, int index)
        {
            if (index < 0 || index > arrLen) return arr;
            for (int i = index; i < arrLen - 1; i++)
            {
                arr[i] = arr[i + 1];
            }
            if (arrLen - 1 >= 0) Array.Resize(ref arr, arrLen - 1);
            return arr;
        }

        public static T[] RemoveIf<T>(this T[] arr, Func<T, bool> condition)
        {
            return RemoveIf(arr, arr.Length, condition);
        }
        
        public static T[] RemoveIf<T>(this T[] arr, int arrLen, Func<T, bool> condition)
        {
            for (int i = arrLen - 1; i >= 0; i--)
            {
                if (condition.Invoke(arr[i])) arr = RemoveAt(arr, arrLen, i);
            }

            return arr;
        }
    }
}