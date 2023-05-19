using UnityEngine;
using UnityEngine.UI;
using XIV.Core.XIVMath;

namespace XIV.TweenSystem
{
    // TODO : WIP ImageColorCurveTween
    // Doesn't work correct if PingPong is true, since it doesn't get any callbacks when returned to pool or when created index never be 0 if reused
    internal sealed class ImageColorCurveTween : CurveTweenDriver<Color, Image>
    {
        int index;
        
        protected override void OnUpdate(float easedTime)
        {
            int length = startValue.Length;
            int onePlus = index + 1;
            float normalizedDurationPerColor = 1f / (length - 1);
            if (easedTime > normalizedDurationPerColor * onePlus)
            {
                index++;
                onePlus++;
            }

            var currentMin = normalizedDurationPerColor * index;
            var currentMax = normalizedDurationPerColor * onePlus;

            var t = XIVMathf.Remap(easedTime, currentMin, currentMax, 0f, 1f);

            var nextIndex = onePlus % (length);
            component.color = Color.Lerp(startValue[index], startValue[nextIndex], t);
        }
    }
}