using System;
using XIV.Core.XIVMath;

namespace XIV.Core.DataStructures
{
    public struct Vec2 : IEquatable<Vec2>
    {
        public float x;
        public float y;

        public static readonly Vec2 zero = new Vec2(0.0f, 0.0f);
        public static readonly Vec2 one = new Vec2(1f, 1f);
        public static readonly Vec2 up = new Vec2(0.0f, 1f);
        public static readonly Vec2 down = new Vec2(0.0f, -1f);
        public static readonly Vec2 left = new Vec2(-1f, 0.0f);
        public static readonly Vec2 right = new Vec2(1f, 0.0f);
        public static readonly Vec2 positiveInfinity = new Vec2(float.PositiveInfinity, float.PositiveInfinity);
        public static readonly Vec2 negativeInfinity = new Vec2(float.NegativeInfinity, float.NegativeInfinity);

        public float sqrMagnitude => x * x + y * y;
        public float magnitude => XIVMathf.Sqrt(x * x + y * y);

        public Vec2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static implicit operator Vec3(Vec2 v)
        {
            return new Vec3(v.x, v.y, 0);
        }

#if UNITY_ENGINE
        // Implicit conversion from Vec2 to UnityEngine.Vector2
        public static implicit operator UnityEngine.Vector2(Vec2 v)
        {
            return new UnityEngine.Vector2(v.x, v.y);
        }

        // Implicit conversion from UnityEngine.Vector2 to Vec2
        public static implicit operator Vec2(UnityEngine.Vector2 v)
        {
            return new Vec2(v.x, v.y);
        }
#endif

        public static Vec2 MoveTowards(Vec2 current, Vec2 target, float movement)
        {
            float xDiff = target.x - current.x;
            float yDiff = target.y - current.y;
            float diffSqrMagnitude = xDiff * xDiff + yDiff * yDiff;
            if (diffSqrMagnitude == 0f || (movement >= 0f && diffSqrMagnitude <= movement * movement))
                return target;
            float diffMagnitude = XIVMathf.Sqrt(diffSqrMagnitude);
            return new Vec2(current.x + xDiff / diffMagnitude * movement,
                            current.y + yDiff / diffMagnitude * movement);
        }

        public static Vec2 Lerp(Vec2 a, Vec2 b, float t)
        {
            t = XIVMathf.Clamp01(t);
            return new Vec2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static Vec2 LerpUnclamped(Vec2 a, Vec2 b, float t)
        {
            return new Vec2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        public static float Dot(Vec2 a, Vec2 b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public static Vec2 Normalize(Vec2 vec)
        {
            float magnitude = Magnitude(vec);
            return magnitude > 9.999999747378752E-06 ? vec / magnitude : zero;
        }

        public static float Magnitude(Vec2 vec)
        {
            return XIVMathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static Vec2 operator /(Vec2 vec, float val)
        {
            return new Vec2(vec.x / val, vec.y / val);
        }

        public static Vec2 operator *(Vec2 vec, float val)
        {
            return new Vec2(vec.x * val, vec.y * val);
        }

        public static Vec2 operator *(float val, Vec2 vec)
        {
            return new Vec2(vec.x * val, vec.y * val);
        }

        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x * b.x, a.y * b.y);
        }

        // IEquatable implementation
        public bool Equals(Vec2 other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vec2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Vec2 left, Vec2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec2 left, Vec2 right)
        {
            return !left.Equals(right);
        }
    }
}