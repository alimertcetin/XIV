namespace XIV.Core.Utils
{
    public static class ArrayUtils
    {
        // https://en.wikipedia.org/wiki/Row-_and_column-major_order
        public static int Get1DIndex(int x, int y, int width)
        {
            // 2,0 (6) - 2,1 (7) - 2,2 (8)
            // 1,0 (3) - 1,1 (4) - 1,2 (5)
            // 0,0 (0) - 0,1 (1) - 0,2 (2)
            
            return x * width + y; // row major
            // return y * height + x; // column major
        }
        
        /// <summary>
        /// Returns a tuple that has x in [0] and y in [1]
        /// </summary>
        /// <param name="index"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static (int, int) Get2DIndex(int index, int width)
        {
            return (index / width, index % width);
        }
    }
}