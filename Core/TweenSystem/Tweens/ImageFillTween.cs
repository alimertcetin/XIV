using UnityEngine;
using UnityEngine.UI;

namespace XIV.TweenSystem
{
    internal sealed class ImageFillTween : TweenDriver<float, Image>
    {
        protected override void OnUpdate(float easedTime)
        {
            component.fillAmount = Mathf.Lerp(startValue, endValue, easedTime);
        }
    }
}