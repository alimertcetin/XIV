using UnityEngine;
using XIV.Core.Collections;
using XIV.PoolSystem;

namespace XIV.Core.TweenSystem
{
    internal static class XIVTweenSystem
    {
        class TweenHelperMono : MonoBehaviour
        {
            void Update()
            {
                int count = tweenDatas.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    TweenData tweenData = XIVTweenSystem.tweenDatas[i];
                    TweenTimeline timeline = tweenData.timelines[0];
                    timeline.Update();
                    if (timeline.IsDone())
                    {
                        tweenData.timelines.RemoveAt(0);
                        XIVPoolSystem.ReturnItem(timeline);
                    }

                    if (tweenData.timelines.Count == 0)
                    {
                        XIVTweenSystem.tweenDatas.RemoveAt(i);
                        XIVTweenSystem.instanceIDLookup.RemoveAt(i);
                        XIVPoolSystem.ReturnItem(tweenData);
                    }
                }
            }

            void OnDestroy()
            {
                tweenHelperMono = null;
                XIVTweenSystem.Clear();
            }
        }

        static DynamicArray<TweenData> tweenDatas = new DynamicArray<TweenData>(8);
        static DynamicArray<int> instanceIDLookup = new DynamicArray<int>(8);
        static TweenHelperMono tweenHelperMono;

        internal static void AddTween(int instanceID, TweenTimeline tween)
        {
            tweenHelperMono ??= new GameObject("XIV-TweenSystem-Helper").AddComponent<TweenHelperMono>();
            GetTweenData(instanceID).timelines.Add() = tween;
        }

        internal static void CancelTween(int instanceID, bool forceComplete = true)
        {
            var index = instanceIDLookup.IndexOf(ref instanceID);
            if (index < 0) return;

            var tweenData = tweenDatas[index];
            int timelineCount = tweenData.timelines.Count;
            if (forceComplete)
            {
                for (int i = 0; i < timelineCount; i++)
                {
                    tweenData.timelines[i].Update(int.MaxValue);
                }
            }
            else
            {
                for (int i = 0; i < timelineCount; i++)
                {
                    tweenData.timelines[i].Cancel();
                }
            }

            tweenDatas.RemoveAt(index);
            instanceIDLookup.RemoveAt(index);
            XIVPoolSystem.ReturnItem(tweenData);
        }

        internal static bool HasTween(int instanceID) => instanceIDLookup.IndexOf(ref instanceID) > -1;

        static TweenData GetTweenData(int instanceID)
        {
            var index = instanceIDLookup.IndexOf(ref instanceID);
            if (index > -1) return tweenDatas[index];

            var tweenData = XIVPoolSystem.GetItem<TweenData>();
            tweenData.instanceID = instanceID;
            instanceIDLookup.Add() = instanceID;
            tweenDatas.Add() = tweenData;
            return tweenData;
        }

        static void Clear()
        {
            instanceIDLookup.Clear();
            tweenDatas.Clear();
        }
    }
}
