using System;
using System.Collections.Generic;
using UnityEngine;
using XIV.Core.Collections;
using XIV.PoolSystem;
using Object = UnityEngine.Object;

namespace XIV.Core.TweenSystem
{
    internal static class XIVTweenSystem
    {
        class TweenHelperMono : MonoBehaviour
        {
            void Update()
            {
                int count = XIVTweenSystem.tweenTimelines.Count;
                for (int i = count - 1; i >= 0; i--)
                {
                    List<TweenTimeline> timelines = XIVTweenSystem.tweenTimelines[i];
                    TweenTimeline timeline = timelines[0];
                    timeline.Update();
                    if (timeline.IsDone())
                    {
                        timelines.RemoveAt(0);
                        timeline.Clear();
                        XIVTweenSystem.ReleaseItem(timeline);
                    }

                    if (timelines.Count == 0)
                    {
                        XIVTweenSystem.Remove(instanceIDs[i], i);
                        timelines.Clear();
                        XIVTweenSystem.ReleaseItem(timelines);
                    }
                }
            }

            void OnDestroy()
            {
                XIVTweenSystem.tweenHelperMono = null;
                XIVTweenSystem.Clear();
            }
        }

        static readonly List<List<TweenTimeline>> tweenTimelines = new(8);
        static readonly List<int> instanceIDs = new(8);
        /// <summary>
        /// (int, int) = (instanceID, index in <see cref="instanceIDs"/>)
        /// Note that indexer in both InstanceIDs and <see cref="tweenTimelines"/> are targeting to the same object.
        /// </summary>
        static readonly Dictionary<int, int> instanceIDLookup = new(8);
        static TweenHelperMono tweenHelperMono;

        internal static T GetTween<T>() where T : ITween => GetItem<T>();
        internal static TweenTimeline GetTimeline()
        {
            var timeLine = GetItem<TweenTimeline>();
            timeLine.SetDeltaTimeFunc(TweenTimeline.defaulDeltaTimeFunc);
            return timeLine;
        }

        internal static TweenTimeline GetTimeline(ITween tween)
        {
            var timeline = GetTimeline();
            timeline.AddTween(tween);
            return timeline;
        }

        internal static TweenTimeline GetTimeline(ITween[] tweens)
        {
            var timeline = GetTimeline();
            int length = tweens.Length;
            for (int i = 0; i < length; i++)
            {
                timeline.AddTween(tweens[i]);
            }

            return timeline;
        }

        internal static void ReleaseTween(ITween tween)
        {
            ReleaseItem(tween);
        }

        internal static void AddTween(int instanceID, TweenTimeline timeline)
        {
            if (tweenHelperMono == false)
            {
                tweenHelperMono = new GameObject("XIV-TweenSystem-Helper").AddComponent<TweenHelperMono>();
                Object.DontDestroyOnLoad(tweenHelperMono);
            }

            GetTweenTimelines(instanceID).Add(timeline);
        }

        internal static void CancelTween(int instanceID, bool forceComplete = true)
        {
            if (instanceIDLookup.TryGetValue(instanceID, out int index) == false) return;

            var timelines = tweenTimelines[index];
            int timelineCount = timelines.Count;
            for (int i = 0; i < timelineCount; i++)
            {
                TweenTimeline timeline = timelines[i];
                if (forceComplete) timeline.ForceComplete();
                else timeline.Cancel();
                
                timeline.Clear();
            }
            
            timelines.Clear();
            ReleaseItem(timelines);
            Remove(instanceID, index);
        }

        internal static bool HasTween(int instanceID)
        {
            return instanceIDLookup.ContainsKey(instanceID);
        }

        static List<TweenTimeline> GetTweenTimelines(int instanceID)
        {
            if (instanceIDLookup.TryGetValue(instanceID, out int index))
            {
                return tweenTimelines[index];
            }

            var timelines = GetItem<List<TweenTimeline>>();
            Add(instanceID, timelines);
            return timelines;
        }

        static T GetItem<T>()
        {
            return XIVPoolSystem.GetItem<T>();
        }

        static void ReleaseItem<T>(T item)
        {
            XIVPoolSystem.ReleaseItem(item);
        }

        static void Add(int instanceID, List<TweenTimeline> timelines)
        {
            instanceIDLookup.Add(instanceID, instanceIDs.Count);
            instanceIDs.Add(instanceID);
            tweenTimelines.Add(timelines);
        }

        static void Remove(int instanceID, int index)
        {
            instanceIDLookup.Remove(instanceID);
            tweenTimelines.RemoveAt(index);
            instanceIDs.RemoveAt(index);
        }

        static void Clear()
        {
            instanceIDs.Clear();
            instanceIDLookup.Clear();
            tweenTimelines.Clear();
        }
    }
}
