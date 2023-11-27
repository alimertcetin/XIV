using System;

namespace XIV.Core.DataStructures
{
    /// <summary>
    /// Alternative structure to the <see cref="System.Memory{T}"/>.
    /// And also can be used as replacement of <see cref="System.Span{T}"/>.
    /// You should use Memory and Span whenever it is possible.
    /// This struct is useful when you want constraints.
    /// <example>
    /// <code>
    /// public class MemExampleBase{T} { }
    /// public class MemExample{T} : MemExampleBase{XIVMemory{T}}
    ///     // public class XIVMemExample{T} : MemExampleBase{Span{T}} // not possible
    ///     // public class XIVMemExample{T} : MemExampleBase{Memory{T}} // possible
    /// {
    ///     void Example()
    ///     {
    ///         XIVMemory{T} xivMem = new XIVMemory{T}(new T[4], 0, 4);
    ///         XIVMemory{T} xivSliced = xivMem.Slice(1, 2);
    ///         XIVMemory{T} xivReversed = xivMem.reversed;
    ///         xivMem.Reverse();
    ///         Memory{T} mem = new Memory{T}(new T[4], 0, 4);
    ///         Memory{T} memSliced = mem.Slice(1, 2);
    ///         // var memReversed = mem.reversed; // not possible
    ///         // mem.Reverse(); // not possible
    ///     }
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public struct XIVMemory<T> : IEquatable<XIVMemory<T>>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0) throw new System.IndexOutOfRangeException();
                if (index >= Length) throw new System.IndexOutOfRangeException();
                return array[GetArrayIndex(index)];
            }
        }
        
        public XIVMemory<T> reversed => new XIVMemory<T>(array, start, length, !isReversed);
        public int Length => length;
        public bool IsReversed => isReversed;

        T[] array;
        int start;
        int length;
        bool isReversed;

        XIVMemory(T[] array, int start, int length, bool isReversed)
        {
            this.array = array;
            this.start = start;
            this.length = length;
            this.isReversed = isReversed;
            if (length <= 0 || start + length - 1 >= array.Length)
            {
                throw new System.ArgumentOutOfRangeException(nameof(length), length, "Specified argument was out of the range of valid values.");
            }
        }

        public XIVMemory(T[] array, int start, int length) : this(array, start, length, false)
        {
        }

        public XIVMemory(XIVMemory<T> xivMemory) : this(xivMemory.array, xivMemory.start, xivMemory.length, xivMemory.isReversed)
        {
        }

        public void Reverse() => isReversed = !isReversed;

        public XIVMemory<T> Slice(int index, int length)
        {
            int arrIndexStart = GetArrayIndex(index);
            int arrIndexEnd = GetArrayIndex(index + length - 1);
            int diff = arrIndexEnd - arrIndexStart;
            return diff < 0 ? 
                new XIVMemory<T>(array, arrIndexEnd, length, isReversed) : 
                new XIVMemory<T>(array, arrIndexStart, length, isReversed);
        }

        public T[] GetUnderlyingArray() => array;

        int GetArrayIndex(int index)
        {
            return isReversed ? start + length - 1 - index : start + index;
        }

        public bool Equals(XIVMemory<T> other)
        {
            return Equals(array, other.array) && start == other.start && length == other.length;
        }

        public override bool Equals(object obj)
        {
            return obj is XIVMemory<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (array != null ? array.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ start;
                hashCode = (hashCode * 397) ^ length;
                return hashCode;
            }
        }
        
        public static implicit operator XIVMemory<T>(T[] array)
        {
            return new XIVMemory<T>(array, 0, array.Length);
        }

        public static bool operator ==(XIVMemory<T> left, XIVMemory<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(XIVMemory<T> left, XIVMemory<T> right)
        {
            return !left.Equals(right);
        }
    }
}