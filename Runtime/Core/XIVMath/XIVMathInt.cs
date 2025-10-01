using System.Runtime.CompilerServices;

namespace XIV.Core.XIVMath
{
	public static class XIVMathInt
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Repeat(int value, int length)
		{
			return value < 0 ? (value % length) + length : value % length;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int val, int min, int max)
        {
			return val > max ? max : val < min ? min : val;
        }

        /// <summary>
        /// Calculates the next power of two of <paramref name="num"/>
        /// </summary>
        /// <param name="num">The number to calculate next power of two</param>
        /// <returns>Returns the <paramref name="num"/> if it is already power of two, otherwise returns the next power of two</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NextPowerOfTwo(int num)
        {
	        num--;
	        num |= num >> 1;
	        num |= num >> 2;
	        num |= num >> 4;
	        num |= num >> 8;
	        num |= num >> 16;
	        return num + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int x)
        {
	        return (x & (x - 1)) == 0;
        }
	}
}