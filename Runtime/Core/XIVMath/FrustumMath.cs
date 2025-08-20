using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    // https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
    public static class FrustumMath
    {
        public static Vec3 GetFrustum(float distance, float fieldOfView, float aspect)
        {
            var frustumHeight = 2f * distance * XIVMathf.Tan(fieldOfView * 0.5f * XIVMathf.Deg2Rad);
            var frustumWidth = frustumHeight * aspect;
            return new Vec3(frustumWidth, frustumHeight, 1);
        }

        public static float GetFrustumDistance(float frustumHeight, float fieldOfView)
        {
            return frustumHeight * 0.5f / XIVMathf.Tan(fieldOfView * 0.5f * XIVMathf.Deg2Rad);
        }

        public static float GetFrustumHeight(float distance, float fieldOfView)
        {
            return 2f * distance * XIVMathf.Tan(fieldOfView * 0.5f * XIVMathf.Deg2Rad);
        }

        public static float GetFieldOfView(float frustumHeight, float distance)
        {
            return 2f * XIVMathf.Atan(frustumHeight * 0.5f / distance) * XIVMathf.Rad2Deg;
        }
    }
}