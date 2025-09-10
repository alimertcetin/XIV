using System;
using System.Collections.Generic;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class IListExtensions
    {
        /// <summary>
        /// Picks a random element from the list using a uniform distribution.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="list">The list to pick an item from.</param>
        /// <returns>A randomly selected element from the list.</returns>
        public static T PickRandom<T>(this IList<T> list)
        {
            return list[XIVRandom.Range(0, list.Count)];
        }
        
        /// <summary>
        /// Picks an item from the array based on a weighted distribution defined by getWeightFunc.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The list to pick an item from.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item in the array.</param>
        /// <returns>An item selected based on its weighted probability.</returns>
        public static T PickWeighted<T>(this IList<T> array, Func<T, int> getWeightFunc)
        {
            return PickWeighted(array, GetTotalWeight(array, getWeightFunc), getWeightFunc);
        }

        /// <summary>
        /// Picks an item from the array based on a weighted distribution defined by getWeightFunc.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The list to pick an item from.</param>
        /// <param name="totalWeight">The total weight of all items in the array, calculated by GetTotalWeight.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item in the array.</param>
        /// <returns>An item selected based on its weighted probability.</returns>
        public static T PickWeighted<T>(this IList<T> array, int totalWeight, Func<T, int> getWeightFunc)
        {
            int roll = XIVRandom.Range(1, totalWeight + 1);
            int cumulative = 0;
            int arrLen = array.Count;
            for (int i = 0; i < arrLen; i++)
            {
                var item = array[i];
                cumulative += getWeightFunc(item);
                if (roll <= cumulative) return item;
            }
            throw new System.Exception();
        }
        
        /// <summary>
        /// Calculates the total weight of all items in the list based on a given function that returns the weight of each item.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="array">The list to calculate the total weight for.</param>
        /// <param name="getWeightFunc">A function that returns the weight of each item in the array.</param>
        /// <returns>The sum of weights of all items in the list.</returns>
        public static int GetTotalWeight<T>(this IList<T> array, Func<T, int> getWeightFunc)
        {
            int len = array.Count;
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