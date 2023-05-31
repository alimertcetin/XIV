﻿using UnityEngine;
using UnityEngine.UI;
using XIV.Core.XIVMath;

namespace XIV.TweenSystem
{
    internal sealed class ImageColorCurveTween : CurveTweenDriver<Color, Image>
    {
        protected override void OnUpdate(float easedTime)
        {
            var color = BezierMath4D.GetPoint(startValue[0], startValue[1], startValue[2], startValue[3], easedTime);
            component.color = color;
        }
    }
}