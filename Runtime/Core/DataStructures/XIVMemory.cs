using System;
using System.Collections.Generic;

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
    ///         Memory{T} mem = new Memory{T}(new T[4], 0, 4);
    ///         Memory{T} memSliced = mem.Slice(1, 2);
    ///         // var memReversed = mem.reversed; // not possible
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
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException($"Index {index} is out of range for XIVMemory of length {Length}");
                return array[GetArrayIndex(index)];
            }
            set
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException($"Index {index} is out of range for XIVMemory of length {Length}");
                array[GetArrayIndex(index)] = value;
            }
        }
        
        public XIVMemory<T> reversed => new XIVMemory<T>(array, start, length, !isReversed);
        public int Length => length;
        public bool IsReversed => isReversed;

        IList<T> array;
        int start;
        int length;
        bool isReversed;

        XIVMemory(IList<T> array, int start, int length, bool isReversed)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            this.array = array;
            this.start = start;
            this.length = length;
            this.isReversed = isReversed;
            if (length < 0 || start < 0 || start + length > array.Count)
            {
                throw new System.ArgumentOutOfRangeException(nameof(length), length, "Specified argument was out of the range of valid values.");
            }
        }

        public XIVMemory(IList<T> array, int start, int length) : this(array, start, length, false)
        {
        }

        public XIVMemory(IList<T> array) : this(array, 0, array.Count, false)
        {
        }

        public XIVMemory(T[] array) : this(array, 0, array.Length, false)
        {
        }

        public XIVMemory(T[] array, int start, int length) : this(array, start, length, false)
        {
        }

        public XIVMemory(XIVMemory<T> xivMemory) : this(xivMemory.array, xivMemory.start, xivMemory.length, xivMemory.isReversed)
        {
        }

        public XIVMemory(XIVMemory<T> xivMemory, int start, int length) : this(xivMemory.array, xivMemory.start + start, length, xivMemory.isReversed)
        {
        }

        public XIVMemory<T> Slice(int index, int length)
        {
            if (index < 0 || length < 0 || index + length > this.length) throw new ArgumentOutOfRangeException();
            int newStart = GetArrayIndex(index);
            return new XIVMemory<T>(array, newStart, length, isReversed);
        }

        public IList<T> GetUnderlyingArray() => array;

        int GetArrayIndex(int index)
        {
            return isReversed ? start + length - 1 - index : start + index;
        }
        
        public Span<T> AsSpan()
        {
            if (array is T[] arr)
            {
                return isReversed
                    ? throw new InvalidOperationException("Cannot get Span from reversed XIVMemory")
                    : new Span<T>(arr, start, length);
            }

            return new Span<T>(ToArray(), start, length);
        }
        
        public T[] ToArray()
        {
            T[] result = new T[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = this[i];
            }
            return result;
        }

        public bool Equals(XIVMemory<T> other)
        {
            return Equals(array, other.array) && start == other.start && length == other.length && isReversed == other.isReversed;
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
                hashCode = (hashCode * 397) ^ isReversed.GetHashCode();
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