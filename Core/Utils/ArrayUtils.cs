using UnityEngine;

namespace XIV.Core.Utils
{
    public static class ArrayUtils
    {
        public static int Merge<T>(T[] a, int aLen, T[] b, int bLen, T[] buffer)
        {
            int bufferLen = buffer.Length;
            var mergedCount = 0;
            T def = default(T);
            for (var i = 0; i < aLen && mergedCount < bufferLen; i++)
            {
                if (a[i].Equals(def)) continue;
                buffer[mergedCount++] = a[i];
            }

            for (var i = 0; i < bLen && mergedCount < bufferLen; i++)
            {
                if (b[i].Equals(def)) continue;
                buffer[mergedCount++] = b[i];
            }

            return mergedCount;
        }
        
        // https://en.wikipedia.org/wiki/Row-_and_column-major_order
        public static int Get1DIndex(int x, int y, int width)
        {
            // 2,0 (6) - 2,1 (7) - 2,2 (8)
            // 1,0 (3) - 1,1 (4) - 1,2 (5)
            // 0,0 (0) - 0,1 (1) - 0,2 (2)
            
            return x * width + y; // row major
            // return y * height + x; // column major
        }
        
        public static Vector2Int Get2DIndex(int index, int width)
        {
            return new Vector2Int(index / width, index % width);
            // return new Vector2Int(index / height, index % height);
        }
    }
}