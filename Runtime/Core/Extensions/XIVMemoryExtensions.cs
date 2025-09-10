using System;
using System.Collections.Generic;
using XIV.Core.DataStructures;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class XIVMemoryExtensions
    {
        /// <summary>
        /// Converts an IList{T} to a XIVMemory{T}.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The input list to be converted.</param>
        /// <returns>A new instance of XIVMemory{T} containing the same elements as the input list.</returns>
        public static XIVMemory<T> AsXIVMemory<T>(this IList<T> array)
        {
            return new XIVMemory<T>(array);
        }
        
        /// <summary>
        /// Finds the closest item to the given position based on a custom position retrieval function, and returns it.
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="arr">The <seealso cref="XIVMemory{T}"/> to look</param>
        /// <param name="arrLen">The length of the <paramref name="arr"/></param>
        /// <param name="currPos">Current position to compare distances</param>
        /// <param name="closestPoint">Closest point that is returned from <paramref name="getTPosFunc"/></param>
        /// <param name="getTPosFunc">Position retrieval function</param>
        /// </summary>
        public static T GetClosest<T>(this XIVMemory<T> arr, int arrLen, Vec3 currPos, out float distance, out Vec3 closestPoint, Func<T, Vec3> getTPosFunc)
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
        /// <inheritdoc cref="GetClosest{T}(XIVMemory{T}, int, Vec3, out float, out Vec3, Func{T, Vec3})"/>
        /// </summary>
        public static T GetClosest<T>(this XIVMemory<T> arr, int arrLen, Vec3 currPos, Func<T, Vec3> getTPosFunc)
        {
            return GetClosest(arr, arrLen, currPos, out _, out _, getTPosFunc);
        }
        
        /// <summary>
        /// Moves the items to the beginning of the array and creates a new <see cref="XIVMemory{T}"/> that has filtered items.
        /// This modifies the original array. You can use this if order doesn't matter.
        /// </summary>
        /// <returns>A new <see cref="XIVMemory{T}"/> that has filtered items</returns>
        public static XIVMemory<T> FilterBy<T>(this XIVMemory<T> array, int arrLen, Func<T, bool> func)
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
        /// Picks an item from a weighted collection using the provided weight function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The input array containing items to be picked from.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item.</param>
        /// <returns>An item chosen based on its weighted probability.</returns>
        public static T PickWeighted<T>(this XIVMemory<T> array, Func<T, int> getWeightFunc)
        {
            return PickWeighted(array, GetTotalWeight(array, getWeightFunc), getWeightFunc);
        }
        
        /// <summary>
        /// Picks an item from a weighted collection using the provided weight function and total weight.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The input array containing items to be picked from.</param>
        /// <param name="totalWeight">The sum of all weights in the collection.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item.</param>
        /// <returns>An item chosen based on its weighted probability.</returns>
        public static T PickWeighted<T>(this XIVMemory<T> array, int totalWeight, Func<T, int> getWeightFunc)
        {
            int roll = XIVRandom.Range(1, totalWeight + 1);
            int cumulative = 0;
            int arrLen = array.Length;
            for (int i = 0; i < arrLen; i++)
            {
                var item = array[i];
                cumulative += getWeightFunc(item);
                if (roll <= cumulative) return item;
            }
            throw new System.Exception();
        }
        
        /// <summary>
        /// Calculates the total weight of all items in a weighted collection using the provided weight function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the array.</typeparam>
        /// <param name="array">The input array containing items to calculate the total weight for.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item.</param>
        /// <returns>The sum of all weights in the collection.</returns>
        public static int GetTotalWeight<T>(this XIVMemory<T> array, Func<T, int> getWeightFunc)
        {
            int len = array.Length;
            int totalWeight = 0;
            for (int i = 0; i < len; i++)
            {
                var item = array[i];
                totalWeight += getWeightFunc(item);
            }

            return totalWeight;
        }
    }
}