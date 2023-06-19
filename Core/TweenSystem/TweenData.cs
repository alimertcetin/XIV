using XIV.Core.Collections;
using XIV.PoolSystem;

namespace XIV.Core.TweenSystem
{
    internal class TweenData : IPoolable
    {
        public int instanceID;
        public DynamicArray<TweenTimeline> timelines = new DynamicArray<TweenTimeline>(2);

        void IPoolable.OnPoolCreate(IPool pool) { }

        void IPoolable.OnPoolReturn()
        {
            instanceID = -1;
            for (var i = 0; i < timelines.Count; i++)
            {
                XIVPoolSystem.ReturnItem(timelines[i]);
            }
            
            timelines.Clear();
        }
    }
}