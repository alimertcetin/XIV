using UnityEngine;
using UnityEngine.UI;

namespace XIV.TweenSystem
{
    internal sealed class ImageColorTween : TweenDriver<Color, Image>
    {
        protected override void OnUpdate(float easedTime)
        {
            component.color = Color.Lerp(startValue, endValue, easedTime);
        }
    }
}