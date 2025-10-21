using System;
using System.Buffers;

namespace XIV.Core.DataStructures
{
    public struct XIVBuffer<T> : IDisposable
    {
        readonly T[] buffer;

        XIVBuffer(T[] buffer)
        {
            this.buffer = buffer;
        }
        
        internal static XIVBuffer<T> Get(int minLength, out T[] buffer)
        {
            buffer = ArrayPool<T>.Shared.Rent(minLength);
            var xivBuffer = new XIVBuffer<T>(buffer);
            return xivBuffer;
        }

        void IDisposable.Dispose()
        {
            ArrayPool<T>.Shared.Return(buffer);
        }
    }
}