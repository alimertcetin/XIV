using XIV.Core.DataStructures;

namespace XIV.Core.Extensions
{
    public static class XIVColorUtility
    {
        public static string ToHtmlStringRGBA(XIVColor color)
        {
            int r = XIVMath.XIVMathInt.Clamp(XIVMath.XIVMathf.RoundToInt(color.r * 255f), 0, 255);
            int g = XIVMath.XIVMathInt.Clamp(XIVMath.XIVMathf.RoundToInt(color.g * 255f), 0, 255);
            int b = XIVMath.XIVMathInt.Clamp(XIVMath.XIVMathf.RoundToInt(color.b * 255f), 0, 255);
            int a = XIVMath.XIVMathInt.Clamp(XIVMath.XIVMathf.RoundToInt(color.a * 255f), 0, 255);
            return r.ToString("X2") + g.ToString("X2") + b.ToString("X2") + a.ToString("X2");
        }
    }
}