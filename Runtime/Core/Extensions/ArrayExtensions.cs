using System;

namespace XIV.Core.Extensions
{
    public static class ArrayExtensions
    {
        
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