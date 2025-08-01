﻿using System;
using XIV.Core.XIVMath;

namespace XIV.Core.DataStructures
{
    public struct Vec3 : IEquatable<Vec3>
    {
        public float x;
        public float y;
        public float z;
        public Vec3 normalized => Vec3.Normalize(this);
        public static readonly Vec3 zero = new Vec3(0.0f, 0.0f, 0.0f);
        public static readonly Vec3 one = new Vec3(1f, 1f, 1f);
        public static readonly Vec3 up = new Vec3(0.0f, 1f, 0.0f);
        public static readonly Vec3 down = new Vec3(0.0f, -1f, 0.0f);
        public static readonly Vec3 left = new Vec3(-1f, 0.0f, 0.0f);
        public static readonly Vec3 right = new Vec3(1f, 0.0f, 0.0f);
        public static readonly Vec3 forward = new Vec3(0.0f, 0.0f, 1f);
        public static readonly Vec3 back = new Vec3(0.0f, 0.0f, -1f);
        public static readonly Vec3 positiveInfinity = new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
        public static readonly Vec3 negativeInfinity = new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);

        public float sqrMagnitude => x * x + y * y + z * z;
        public float magnitude => XIVMathf.Sqrt(x * x + y * y + z * z);

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator Vec2(Vec3 v)
        {
            return new Vec2(v.x, v.y);
        }

#if UNITY_ENGINE || UNITY_EDITOR
        // Implicit conversion from Vec3 to UnityEngine.Vector3
        public static implicit operator UnityEngine.Vector3(Vec3 v)
        {
            return new UnityEngine.Vector3(v.x, v.y, v.z);
        }

        // Implicit conversion from UnityEngine.Vector3 to Vec3
        public static implicit operator Vec3(UnityEngine.Vector3 v)
        {
            return new Vec3(v.x, v.y, v.z);
        }
#endif

        public static Vec3 MoveTowards(Vec3 current, Vec3 target, float movement)
        {
            float xDiff = target.x - current.x;
            float yDiff = target.y - current.y;
            float zDiff = target.z - current.z;
            float diffSqrMagnitude = xDiff * xDiff + yDiff * yDiff + zDiff * zDiff;
            if (diffSqrMagnitude == 0f || movement >= 0f && diffSqrMagnitude <= movement * movement)
                return target;
            float diffMagnitude = XIVMathf.Sqrt(diffSqrMagnitude);
            return new Vec3(current.x + xDiff / diffMagnitude * movement,
                current.y + yDiff / diffMagnitude * movement,
                current.z + zDiff / diffMagnitude * movement);
        }

        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            t = XIVMathf.Clamp01(t);
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            return new Vec3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
        }

        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x);
        }

        public static Vec3 Normalize(Vec3 vec)
        {
            float magnitude = Magnitude(vec);
            return magnitude > 9.999999747378752E-06 ? vec / magnitude : zero;
        }

        public static float Magnitude(Vec3 vec)
        {
            return XIVMathf.Sqrt(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);
        }

        public static Vec3 operator -(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vec3 operator +(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vec3 operator /(Vec3 vec, float val)
        {
            return new Vec3(vec.x / val, vec.y / val, vec.z / val);
        }

        public static Vec3 operator *(Vec3 vec, float val)
        {
            return new Vec3(vec.x * val, vec.y * val, vec.z * val);
        }

        public static Vec3 operator *(float val, Vec3 vec)
        {
            return new Vec3(vec.x * val, vec.y * val, vec.z * val);
        }

        public static Vec3 operator *(Vec3 a, Vec3 b)
        {
            return new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        // IEquatable implementation
        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override bool Equals(object obj)
        {
            return obj is Vec3 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();
                return hash;
            }
        }

        public static float Distance(Vec3 a, Vec3 b)
        {
            float dx = a.x - b.x;
            float dy = a.y - b.y;
            float dz = a.z - b.z;
            return XIVMathf.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public static bool operator ==(Vec3 left, Vec3 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !left.Equals(right);
        }
    }
}