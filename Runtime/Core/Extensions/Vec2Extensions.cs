using XIV.Core.DataStructures;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class Vec2Extensions
    {
        public static Vec2 SetX(this Vec2 vec2, float value)
        {
            return new Vec2(value, vec2.y);
        }
        
        public static Vec2 SetY(this Vec2 vec2, float value)
        {
            return new Vec2(vec2.x, value);
        }

        public static bool IsSameDirection(this Vec2 vec2, Vec2 other, float threshold = 0f)
        {
            return Vec2.Dot(vec2, other) > threshold;
        }

        public static Vec2 Abs(this Vec2 vec2)
        {
            return new Vec2(XIVMathf.Abs(vec2.x), XIVMathf.Abs(vec2.y));
        }

        public static Vec2 ClampMagnitude(this Vec2 vec2, float min, float max)
        {
            var magnitude = vec2.magnitude;
            if (magnitude < min) magnitude = min;
            if (magnitude > max) magnitude = max;
            return vec2.normalized * magnitude;
        }

        public static bool IsNaN(this Vec2 vec2)
        {
            return float.IsNaN(vec2.x) || float.IsNaN(vec2.y);
        }
    }
}