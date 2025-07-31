using XIV.Core.DataStructures;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class Vec3Extensions
    {
        public static Vec3 OnXZ(this Vec3 vec3)
        {
            return new Vec3(vec3.x, 0f, vec3.z);
        }
        
        public static Vec3 SetX(this Vec3 vec3, float value)
        {
            return new Vec3(value, vec3.y, vec3.z);
        }
        
        public static Vec3 SetY(this Vec3 vec3, float value)
        {
            return new Vec3(vec3.x, value, vec3.z);
        }
        
        public static Vec3 SetZ(this Vec3 vec3, float value)
        {
            return new Vec3(vec3.x, vec3.y, value);
        }

        public static bool IsSameDirection(this Vec3 vec3, Vec3 other, float threshold = 0f)
        {
            return Vec3.Dot(vec3, other) > threshold;
        }

        public static Vec3 Abs(this Vec3 vec3)
        {
            return new Vec3(XIVMathf.Abs(vec3.x), XIVMathf.Abs(vec3.y), XIVMathf.Abs(vec3.z));
        }

        public static Vec3 ClampMagnitude(this Vec3 vec3, float min, float max)
        {
            var magnitude = vec3.magnitude;
            if (magnitude < min) magnitude = min;
            if (magnitude > max) magnitude = max;
            return vec3.normalized * magnitude;
        }

        public static bool IsNaN(this Vec3 vec3)
        {
            return float.IsNaN(vec3.x) || float.IsNaN(vec3.y) || float.IsNaN(vec3.z);
        }

    }
}