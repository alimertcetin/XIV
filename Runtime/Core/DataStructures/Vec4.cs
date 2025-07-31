using System;
using XIV.Core.XIVMath;

namespace XIV.Core.DataStructures
{
    public struct Vec4 : IEquatable<Vec4>
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public static readonly Vec4 zero = new Vec4(0f, 0f, 0f, 0f);
        public static readonly Vec4 one = new Vec4(1f, 1f, 1f, 1f);
        public static readonly Vec4 positiveInfinity = new Vec4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        public static readonly Vec4 negativeInfinity = new Vec4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public float sqrMagnitude => x * x + y * y + z * z + w * w;
        public float magnitude => XIVMathf.Sqrt(sqrMagnitude);
        public Vec4 normalized => Normalize(this);

        public Vec4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

#if UNITY_ENGINE
        // Implicit conversion from Vec4 to UnityEngine.Vector4
        public static implicit operator UnityEngine.Vector4(Vec4 v)
        {
            return new UnityEngine.Vector4(v.x, v.y, v.z, v.w);
        }

        // Implicit conversion from UnityEngine.Vector4 to Vec4
        public static implicit operator Vec4(UnityEngine.Vector4 v)
        {
            return new Vec4(v.x, v.y, v.z, v.w);
        }
#endif

        public static Vec4 Lerp(Vec4 a, Vec4 b, float t)
        {
            t = XIVMathf.Clamp01(t);
            return new Vec4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        public static Vec4 LerpUnclamped(Vec4 a, Vec4 b, float t)
        {
            return new Vec4(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );
        }

        public static float Dot(Vec4 a, Vec4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vec4 Normalize(Vec4 v)
        {
            float mag = v.magnitude;
            return mag > 9.999999747378752E-06f ? v / mag : zero;
        }

        public static float Distance(Vec4 a, Vec4 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            float dw = a.w - b.w;
            return XIVMathf.Sqrt(dx * dx + dy * dy + dz * dz + dw * dw);
        }

        public static Vec4 operator +(Vec4 a, Vec4 b)
        {
            return new Vec4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vec4 operator -(Vec4 a, Vec4 b)
        {
            return new Vec4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vec4 operator *(Vec4 v, float scalar)
        {
            return new Vec4(v.x * scalar, v.y * scalar, v.z * scalar, v.w * scalar);
        }

        public static Vec4 operator *(float scalar, Vec4 v)
        {
            return new Vec4(v.x * scalar, v.y * scalar, v.z * scalar, v.w * scalar);
        }

        public static Vec4 operator /(Vec4 v, float scalar)
        {
            return new Vec4(v.x / scalar, v.y / scalar, v.z / scalar, v.w / scalar);
        }

        public static Vec4 operator *(Vec4 a, Vec4 b)
        {
            return new Vec4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        // IEquatable implementation
        public bool Equals(Vec4 other)
        {
            return x == other.x && y == other.y && z == other.z && w == other.w;
        }

        public override bool Equals(object obj)
        {
            return obj is Vec4 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();
                hash = hash * 23 + w.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Vec4 left, Vec4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec4 left, Vec4 right)
        {
            return !left.Equals(right);
        }
    }
}