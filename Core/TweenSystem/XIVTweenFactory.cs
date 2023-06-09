﻿using System;
using System.Collections.Generic;
using UnityEngine;
using XIV.Core.TweenSystem.ImageTweens;
using XIV.Core.TweenSystem.OtherTweens;
using XIV.Core.TweenSystem.RendererTweens;
using XIV.Core.TweenSystem.TransformTweens;
using XIV.Core.TweenSystem.Drivers;
using XIV.Core.Utils;
using XIV.PoolSystem;

namespace XIV.Core.TweenSystem
{
    public sealed class XIVTweenFactory
    {
        static readonly XIVTweenFactory instance = new XIVTweenFactory();
        static readonly List<TweenTimeline> timelineBuffer = new List<TweenTimeline>(4);

        Component component;
        TweenTimeline currentTimeline;
        int componentInstanceID;
        bool useCurrent;
        
        static T GetPooledTween<T>() where T : IPoolable
        {
            return XIVPoolSystem.GetItem<T>();
        }

        public static XIVTweenFactory GetTween(Component component)
        {
            instance.Initialize(component);
            return instance;
        }

        void Initialize(Component component)
        {
            this.component = component;
            this.componentInstanceID = component.gameObject.GetInstanceID();
            this.currentTimeline = TweenTimeline.GetTimeline();
            useCurrent = true;
        }

        void Reset()
        {
            component = default;
            currentTimeline = default;
            timelineBuffer.Clear();
        }

        public XIVTweenFactory AddTween(ITween tween)
        {
            if (useCurrent == false)
            {
                timelineBuffer.Add(currentTimeline);
                currentTimeline = TweenTimeline.GetTimeline(tween);
                return this;
            }

            currentTimeline.tweens.Add() = tween;
            useCurrent = false;
            return this;
        }

        XIVTweenFactory AddTween<T>(Vector3 from, Vector3 to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0) where T : TweenDriver<Vector3, Transform>
        {
            var t = GetPooledTween<T>().Set(component.transform, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        XIVTweenFactory AddTween<T>(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0) where T : TweenDriver<float, Transform>
        {
            var t = GetPooledTween<T>().Set(component.transform, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        XIVTweenFactory AddTween<T>(Quaternion from, Quaternion to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0) where T : TweenDriver<Quaternion, Transform>
        {
            var t = GetPooledTween<T>().Set(component.transform, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        public void Start()
        {
            timelineBuffer.Add(currentTimeline);
            int count = timelineBuffer.Count;
            for (int i = 0; i < count; i++)
            {
                XIVTweenSystem.AddTween(componentInstanceID, timelineBuffer[i]);
            }
            Reset();
        }

        public XIVTweenFactory And()
        {
            useCurrent = true;
            return this;
        }

        public XIVTweenFactory Wait(float duration)
        {
            var t = GetPooledTween<WaitTween>().Set(duration);
            return AddTween(t);
        }

        public XIVTweenFactory OnComplete(Action action)
        {
            var t = GetPooledTween<OnCompleteCallbackTween>().Set(action);
            return AddTween(t);
        }

        public XIVTweenFactory OnCanceled(Action action)
        {
            var t = GetPooledTween<OnCanceledCallbackTween>().Set(action);
            return AddTween(t);
        }


        public XIVTweenFactory OnComplete(Action<GameObject> action)
        {
            var t = GetPooledTween<OnCompleteCallbackTween<GameObject>>().Set(action, component.gameObject);
            return AddTween(t);
        }

        public XIVTweenFactory OnCanceled(Action<GameObject> action)
        {
            var t = GetPooledTween<OnCanceledCallbackTween<GameObject>>().Set(action, component.gameObject);
            return AddTween(t);
        }

        // Transform
        public XIVTweenFactory Scale(Vector3 from, Vector3 to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<ScaleTween>(from, to, duration, easingFunc, isPingPong, loopCount);
        }

        public XIVTweenFactory ScaleX(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<ScaleTweenX>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory ScaleY(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<ScaleTweenY>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory ScaleZ(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<ScaleTweenZ>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory Move(Vector3 from, Vector3 to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<MoveTween>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory MoveX(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<MoveTweenX>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory MoveY(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<MoveTweenY>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory MoveZ(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<MoveTweenZ>(from, to, duration, easingFunc, isPingPong, loopCount);
        }

        /// <summary>
        /// Moves GameObject in world space but calculates the position on screen space
        /// </summary>
        public XIVTweenFactory WorldToUIMove(Vector3[] screenSpacePoints, Camera cam, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            var followCurve = GetPooledTween<FollowCurveScreenSpaceToWorldSpaceTween>();
            followCurve.cam = cam;
            var t = followCurve.Set(component.transform, screenSpacePoints, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }
        
        public XIVTweenFactory Rotate(Quaternion from, Quaternion to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<RotationTween>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory RotateX(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<RotationTweenX>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory RotateY(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<RotationTweenY>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory RotateZ(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            return AddTween<RotationTweenZ>(from, to, duration, easingFunc, isPingPong, loopCount);
        }
        
        public XIVTweenFactory FollowCurve(Vector3[] points, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            var t = GetPooledTween<FollowCurveTween>().Set(component.transform, points, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        // Image
        public XIVTweenFactory ImageFill(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<UnityEngine.UI.Image>(out var image) == false) return CastError(typeof(UnityEngine.UI.Image));
            var t = GetPooledTween<ImageFillTween>().Set(image, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        public XIVTweenFactory ImageColor(Color from, Color to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<UnityEngine.UI.Image>(out var image) == false) return CastError(typeof(UnityEngine.UI.Image));
            var t = GetPooledTween<ImageColorTween>().Set(image, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        public XIVTweenFactory ImageAlpha(float from, float to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<UnityEngine.UI.Image>(out var image) == false) return CastError(typeof(UnityEngine.UI.Image));
            var t = GetPooledTween<ImageAlphaTween>().Set(image, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        public XIVTweenFactory ImageColorCurve(Color[] colors, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<UnityEngine.UI.Image>(out var image) == false) return CastError(typeof(UnityEngine.UI.Image));
            var t = GetPooledTween<ImageColorCurveTween>().Set(image, colors, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }
        
        // Renderer
        public XIVTweenFactory RendererColor(Color from, Color to, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<Renderer>(out var renderer) == false) return CastError(typeof(Renderer));
            var t = GetPooledTween<RendererColorTween>().Set(renderer, from, to, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }
        
        public XIVTweenFactory RendererColorCurve(Color[] colors, float duration, EasingFunction.Function easingFunc, bool isPingPong = false, int loopCount = 0)
        {
            if (TryGetComponent<Renderer>(out var renderer) == false) return CastError(typeof(Renderer));
            var t = GetPooledTween<RendererColorCurveTween>().Set(renderer, colors, duration, easingFunc, isPingPong, loopCount);
            return AddTween(t);
        }

        bool TryGetComponent<T>(out T comp) where T : Component
        {
            comp = component as T;
            if (comp == default) comp = component.GetComponent<T>();
            return comp != default;
        }

        XIVTweenFactory CastError(Type type)
        {
            return LogError("Cannot cast " + component.GetType() + " to " + type);
        }

        XIVTweenFactory LogError(string error)
        {
            Debug.LogError(error);
            return this;
        }
    }
}