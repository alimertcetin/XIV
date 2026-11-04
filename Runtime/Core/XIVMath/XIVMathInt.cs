using System.Runtime.CompilerServices;

namespace XIV.Core.XIVMath
{
	public static class XIVMathInt
	{
		/// <summary>
		/// Loops <paramref name="value"/> within the bounds of <paramref name="length"/> (similar to modulo, but handles negative values).
		/// </summary>
		/// <param name="value">The value to wrap</param>
		/// <param name="length">The maximum length/bound (exclusive)</param>
		/// <returns>Returns the wrapped value within the range [0, <paramref name="length"/>)</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Repeat(int value, int length)
		{
			if (length <= 0) return 0;
			return ((value % length) + length) % length;
		}
		
		/// <summary>
		/// Loops <paramref name="value"/> back and forth between 0 and <paramref name="length"/> - 1 (ping-pong effect).
		/// </summary>
		/// <param name="value">The value to loop</param>
		/// <param name="length">The peak length</param>
		/// <returns>Returns the oscillated value between 0 and <paramref name="length"/> - 1</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PingPong(int value, int length)
		{
			if (length <= 1) return 0;

			int period = 2 * (length - 1);
			value = Repeat(value, period);
			return value < length ? value : period - value;
		}
		
		/// <summary>
		/// Returns the smaller of two 32-bit signed integers.
		/// </summary>
		/// <param name="a">The first number to compare</param>
		/// <param name="b">The second number to compare</param>
		/// <returns>Returns <paramref name="a"/> if it is less than <paramref name="b"/>, otherwise <paramref name="b"/></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}
		
		/// <summary>
		/// Returns the larger of two 32-bit signed integers.
		/// </summary>
		/// <param name="a">The first number to compare</param>
		/// <param name="b">The second number to compare</param>
		/// <returns>Returns <paramref name="a"/> if it is greater than <paramref name="b"/>, otherwise <paramref name="b"/></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		/// <summary>
		/// Clamps <paramref name="val"/> to a specified range defined by <paramref name="min"/> and <paramref name="max"/>.
		/// </summary>
		/// <param name="val">The value to clamp</param>
		/// <param name="min">The minimum inclusive value</param>
		/// <param name="max">The maximum inclusive value</param>
		/// <returns>Returns <paramref name="min"/> if <paramref name="val"/> is less than <paramref name="min"/>, <paramref name="max"/> if it is greater than <paramref name="max"/>, otherwise <paramref name="val"/></returns>
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

        /// <summary>
        /// Determines whether <paramref name="x"/> is a power of two.
        /// </summary>
        /// <param name="x">The number to check</param>
        /// <returns>Returns <c>true</c> if <paramref name="x"/> is a power of two, otherwise <c>false</c></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPowerOfTwo(int x)
        {
	        return (x & (x - 1)) == 0;
        }

        /// <summary>
        /// Returns the absolute value of a 32-bit signed integer.
        /// </summary>
        /// <param name="x">The number to get the absolute value for</param>
        /// <returns>Returns the absolute value of <paramref name="x"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Abs(int x)
        {
	        return (x < 0) ? -x : x;
        }
	}
}