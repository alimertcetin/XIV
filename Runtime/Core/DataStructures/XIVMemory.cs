using System;
using System.Collections;
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
    public struct XIVMemory<T> : IEquatable<XIVMemory<T>>, IEnumerable<T>
    {
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException($"Index {index} is out of range for XIVMemory of length {Length}");
                return list[GetArrayIndex(index)];
            }
            set
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException($"Index {index} is out of range for XIVMemory of length {Length}");
                list[GetArrayIndex(index)] = value;
            }
        }

        public ref T GetRef(int index)
        {
            if (index < 0 || index >= Length) throw new IndexOutOfRangeException($"Index {index} is out of range for XIVMemory of length {Length}");
            if (isArray == false) throw new InvalidOperationException($"Reference type is not an {typeof(T[])}, reference type: {typeof(IList<T>)}");
            return ref array[GetArrayIndex(index)];
        }

        public XIVMemory<T> reversed => new XIVMemory<T>(list, start, length, !isReversed);
        public int Length => length;
        public bool IsReversed => isReversed;

        IList<T> list;
        T[] array;
        int start;
        int length;
        bool isReversed;
        bool isArray;

        XIVMemory(IList<T> list, int start, int length, bool isReversed)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            this.list = list;
            this.start = start;
            this.length = length;
            this.isReversed = isReversed;
            if (length < 0 || start < 0 || start + length > list.Count)
            {
                throw new System.ArgumentOutOfRangeException(nameof(length), length, "Specified argument was out of the range of valid values.");
            }

            array = list as T[];
            isArray = array != null;
        }

        public XIVMemory(IList<T> list, int start, int length) : this(list, start, length, false)
        {
        }

        public XIVMemory(IList<T> list) : this(list, 0, list.Count, false)
        {
        }

        public XIVMemory(T[] list) : this(list, 0, list.Length, false)
        {
        }

        public XIVMemory(T[] list, int start, int length) : this(list, start, length, false)
        {
        }

        public XIVMemory(XIVMemory<T> xivMemory) : this(xivMemory.list, xivMemory.start, xivMemory.length, xivMemory.isReversed)
        {
        }

        public XIVMemory(XIVMemory<T> xivMemory, int start, int length) : this(xivMemory.list, xivMemory.start + start, length, xivMemory.isReversed)
        {
        }

        public XIVMemory<T> Slice(int index, int length)
        {
            if (index < 0 || length < 0 || index + length > this.length) throw new ArgumentOutOfRangeException();
            int newStart = GetArrayIndex(index);
            return new XIVMemory<T>(list, newStart, length, isReversed);
        }

        public IList<T> GetUnderlyingArray() => list;

        int GetArrayIndex(int index)
        {
            return isReversed ? start + length - 1 - index : start + index;
        }
        
        public Span<T> AsSpan()
        {
            if (list is T[] arr)
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
            return Equals(list, other.list) && start == other.start && length == other.length && isReversed == other.isReversed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < length; i++)
            {
                yield return list[GetArrayIndex(i)];
            }
        }

        public override bool Equals(object obj)
        {
            return obj is XIVMemory<T> other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (list != null ? list.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ start;
                hashCode = (hashCode * 397) ^ length;
                hashCode = (hashCode * 397) ^ isReversed.GetHashCode();
                return hashCode;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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