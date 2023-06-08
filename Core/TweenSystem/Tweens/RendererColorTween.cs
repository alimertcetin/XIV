using UnityEngine;

namespace XIV.TweenSystem
{
    internal sealed class RendererColorTween : TweenDriver<Color, Renderer>
    {
        protected override void OnUpdate(float easedTime)
        {
            component.material.color = Color.Lerp(startValue, endValue, easedTime);
        }
    }
}