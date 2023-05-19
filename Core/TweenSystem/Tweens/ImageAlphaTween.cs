using UnityEngine;
using UnityEngine.UI;
using XIV.Core.Extensions;

namespace XIV.TweenSystem
{
    internal sealed class ImageAlphaTween : TweenDriver<float, Image>
    {
        protected override void OnUpdate(float easedTime)
        {
            component.color = component.color.SetA(Mathf.Lerp(startValue, endValue, easedTime));
        }
    }
}