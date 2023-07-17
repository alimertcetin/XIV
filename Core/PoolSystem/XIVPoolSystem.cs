using System;
using System.Collections.Generic;

namespace XIV.PoolSystem
{
    public static class XIVPoolSystem
    {
        static Dictionary<Type, IPool> pools = new Dictionary<Type, IPool>();

        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            pools.Clear();
        }
        
        static T AddPool<T>(T pool) where T : IPool
        {
            pools.Add(pool.StoredType, pool);
            return pool;
        }

        public static T GetItem<T>() where T : IPoolable
        {
            return HasPool(typeof(T)) == false ? AddPool(new XIVPool<T>(Activator.CreateInstance<T>)).GetItem() : GetPool<T>().GetItem();
        }

        public static void ReturnItem<T>(T item) where T : IPoolable
        {
#if UNITY_EDITOR
            if (HasPool(typeof(T)) == false) throw new NullReferenceException("Pool is null. You should add a pool for " + typeof(T) + " to get items from it");
#endif
            GetPool<T>().Return(item);
        }

        public static bool HasPool<T>() => HasPool(typeof(T));

        public static bool HasPool(Type type) => pools.ContainsKey(type);

        static IPool<T> GetPool<T>() where T : IPoolable => (IPool<T>)pools[typeof(T)];
    }
}