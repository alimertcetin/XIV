using System;
using System.Buffers;

namespace XIV.Core.DataStructures
{
    public struct XIVBuffer<T> : IDisposable
    {
        T[] pooledBuffer;
        public ref T this[int index] => ref pooledBuffer[index];
        public int Length => pooledBuffer.Length;
        
        bool isDisposed;

        public XIVBuffer(T[] pooledBuffer)
        {
            this.pooledBuffer = pooledBuffer;
            isDisposed = false;
        }
        
        internal static XIVBuffer<T> Get(int minLength)
        {
            var buffer = ArrayPool<T>.Shared.Rent(minLength);
            var xivBuffer = new XIVBuffer<T>(buffer);
            return xivBuffer;
        }
        
        public static implicit operator T[](XIVBuffer<T> xivBuffer)
        {
            return xivBuffer.pooledBuffer;
        }

        void IDisposable.Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;
            ArrayPool<T>.Shared.Return(pooledBuffer);
            pooledBuffer = null;
        }
    }
}