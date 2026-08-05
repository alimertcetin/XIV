using XIV.Core.XIVMath;

namespace XIV.Core.DataStructures
{
    public struct XIVQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        // ------------------------------------------------------------
        // Construction
        // ------------------------------------------------------------

        /// <summary>
        /// Creates a quaternion from components.
        /// </summary>
        public XIVQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        /// <summary>
        /// Identity rotation.
        /// </summary>
        public static XIVQuaternion identity => new XIVQuaternion(0f, 0f, 0f, 1f);

        // ------------------------------------------------------------
        // Properties
        // ------------------------------------------------------------

        /// <summary>
        /// Length of the quaternion.
        /// </summary>
        public float magnitude => XIVMathf.Sqrt(x * x + y * y + z * z + w * w);

        /// <summary>
        /// Returns a normalized copy of the quaternion.
        /// </summary>
        public XIVQuaternion normalized
        {
            get
            {
                float mag = magnitude;
                if (mag > XIVMathf.Epsilon) return this / mag;
                return identity;
            }
        }

        /// <summary>
        /// Returns the rotation expressed as Euler angles in degrees.
        /// </summary>
        public Vec3 eulerAngles
        {
            get
            {
                // Roll (X axis)
                float sinRCosP = 2f * (w * x + y * z);
                float cosRCosP = 1f - 2f * (x * x + y * y);
                float roll = XIVMathf.Atan2(sinRCosP, cosRCosP);
                // Pitch (Y axis)
                float sinP = 2f * (w * y - z * x);
                float pitch;
                if (XIVMathf.Abs(sinP) >= 1f) pitch = XIVMathf.CopySign(XIVMathf.PI * 0.5f, sinP);
                else pitch = XIVMathf.Asin(sinP);

                // Yaw (Z axis)
                float sinYCosP = 2f * (w * z + x * y);
                float cosYCosP = 1f - 2f * (y * y + z * z);
                float yaw = XIVMathf.Atan2(sinYCosP, cosYCosP);

                return new Vec3(
                    roll * XIVMathf.Rad2Deg,
                    pitch * XIVMathf.Rad2Deg,
                    yaw * XIVMathf.Rad2Deg
                );
            }
        }

        // ------------------------------------------------------------
        // Operators
        // ------------------------------------------------------------

        /// <summary>
        /// Combines two rotations.
        /// </summary>
        public static XIVQuaternion operator *(XIVQuaternion a, XIVQuaternion b)
        {
            return new XIVQuaternion(
                a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
                a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
                a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
                a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
            );
        }

        /// <summary>
        /// Rotates a vector by this quaternion.
        /// </summary>
        public static Vec3 operator *(XIVQuaternion q, Vec3 v)
        {
            Vec3 u = new Vec3(q.x, q.y, q.z);
            float s = q.w;

            return
                2f * Vec3.Dot(u, v) * u +
                (s * s - Vec3.Dot(u, u)) * v +
                2f * s * Vec3.Cross(u, v);
        }

        public static XIVQuaternion operator *(XIVQuaternion q, float s) => new XIVQuaternion(q.x * s, q.y * s, q.z * s, q.w * s);

        public static XIVQuaternion operator /(XIVQuaternion q, float s) => new XIVQuaternion(q.x / s, q.y / s, q.z / s, q.w / s);

        public static bool operator ==(XIVQuaternion a, XIVQuaternion b) => Dot(a, b) > 0.999999f;

        public static bool operator !=(XIVQuaternion a, XIVQuaternion b) => !(a == b);

        // ------------------------------------------------------------
        // Core math
        // ------------------------------------------------------------

        /// <summary>
        /// Normalizes this quaternion in place.
        /// </summary>
        public void Normalize()
        {
            float mag = magnitude;
            if (mag > XIVMathf.Epsilon)
            {
                x /= mag;
                y /= mag;
                z /= mag;
                w /= mag;
            }
            else
            {
                this = identity;
            }
        }

        /// <summary>
        /// Dot product between two quaternions.
        /// </summary>
        public static float Dot(XIVQuaternion a, XIVQuaternion b) => a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;

        /// <summary>
        /// Returns the inverse rotation.
        /// </summary>
        public static XIVQuaternion Inverse(XIVQuaternion q)
        {
            float dot = Dot(q, q);
            if (dot > 0f)
            {
                float inv = 1f / dot;
                return new XIVQuaternion(
                    -q.x * inv,
                    -q.y * inv,
                    -q.z * inv,
                    q.w * inv
                );
            }
            return identity;
        }

        // ------------------------------------------------------------
        // Creation helpers
        // ------------------------------------------------------------

        /// <summary>
        /// Creates a rotation from Euler angles in degrees.
        /// </summary>
        public static XIVQuaternion Euler(float x, float y, float z)
        {
            float hx = x * XIVMathf.Deg2Rad * 0.5f;
            float hy = y * XIVMathf.Deg2Rad * 0.5f;
            float hz = z * XIVMathf.Deg2Rad * 0.5f;

            float sx = XIVMathf.Sin(hx);
            float cx = XIVMathf.Cos(hx);
            float sy = XIVMathf.Sin(hy);
            float cy = XIVMathf.Cos(hy);
            float sz = XIVMathf.Sin(hz);
            float cz = XIVMathf.Cos(hz);

            return new XIVQuaternion(
                sx * cy * cz + cx * sy * sz,
                cx * sy * cz - sx * cy * sz,
                cx * cy * sz + sx * sy * cz,
                cx * cy * cz - sx * sy * sz
            );
        }

        /// <summary>
        /// Creates a rotation around an axis by an angle in degrees.
        /// </summary>
        public static XIVQuaternion AngleAxis(float angleDeg, Vec3 axis)
        {
            axis = axis.normalized;
            float halfRad = angleDeg * XIVMathf.Deg2Rad * 0.5f;
            float sin = XIVMathf.Sin(halfRad);

            return new XIVQuaternion(
                axis.x * sin,
                axis.y * sin,
                axis.z * sin,
                XIVMathf.Cos(halfRad)
            );
        }

        /// <summary>
        /// Creates a rotation looking in the given forward direction.
        /// </summary>
        public static XIVQuaternion LookRotation(Vec3 forward, Vec3 up)
        {
            forward = forward.normalized;
            Vec3 right = Vec3.Cross(up, forward).normalized;
            up = Vec3.Cross(forward, right);

            // Rotation matrix elements (row-major)
            float m00 = right.x; // row 0, column 0
            float m01 = right.y; // row 0, column 1
            float m02 = right.z; // row 0, column 2

            float m10 = up.x; // row 1, column 0
            float m11 = up.y; // row 1, column 1
            float m12 = up.z; // row 1, column 2

            float m20 = forward.x; // row 2, column 0
            float m21 = forward.y; // row 2, column 1
            float m22 = forward.z; // row 2, column 2

            float trace = m00 + m11 + m22;
            if (trace > 0f)
            {
                float s = XIVMathf.Sqrt(trace + 1f) * 2f;
                return new XIVQuaternion(
                    (m12 - m21) / s,
                    (m20 - m02) / s,
                    (m01 - m10) / s,
                    0.25f * s
                );
            }

            if (m00 > m11 && m00 > m22)
            {
                float s = XIVMathf.Sqrt(1f + m00 - m11 - m22) * 2f;
                return new XIVQuaternion(
                    0.25f * s,
                    (m01 + m10) / s,
                    (m02 + m20) / s,
                    (m12 - m21) / s
                );
            }

            if (m11 > m22)
            {
                float s = XIVMathf.Sqrt(1f + m11 - m00 - m22) * 2f;
                return new XIVQuaternion(
                    (m10 + m01) / s,
                    0.25f * s,
                    (m21 + m12) / s,
                    (m20 - m02) / s
                );
            }

            float s2 = XIVMathf.Sqrt(1f + m22 - m00 - m11) * 2f;
            return new XIVQuaternion(
                (m20 + m02) / s2,
                (m21 + m12) / s2,
                0.25f * s2,
                (m01 - m10) / s2
            );
        }

        // ------------------------------------------------------------
        // Interpolation
        // ------------------------------------------------------------

        /// <summary>
        /// Linearly interpolates between two rotations.
        /// </summary>
        public static XIVQuaternion Lerp(XIVQuaternion a, XIVQuaternion b, float t)
        {
            t = XIVMathf.Clamp(t, 0f, 1f);
            XIVQuaternion q = a * (1f - t) + b * t;
            return q.normalized;
        }

        /// <summary>
        /// Fast linear interpolation.
        /// Assumptions:
        /// - a and b are normalized
        /// - t is in [0, 1]
        /// </summary>
        public static XIVQuaternion LerpFast(in XIVQuaternion a, in XIVQuaternion b, float t)
        {
            XIVQuaternion q = new XIVQuaternion(
                a.x + (b.x - a.x) * t,
                a.y + (b.y - a.y) * t,
                a.z + (b.z - a.z) * t,
                a.w + (b.w - a.w) * t
            );

            return q.normalized;
        }

        /// <summary>
        /// Spherically interpolates between two rotations.
        /// </summary>
        public static XIVQuaternion Slerp(XIVQuaternion a, XIVQuaternion b, float t)
        {
            t = XIVMathf.Clamp(t, 0f, 1f);

            float dot = Dot(a, b);
            if (dot < 0f)
            {
                b = b * -1f;
                dot = -dot;
            }

            if (dot > 0.9995f)
                return Lerp(a, b, t);

            float theta0 = XIVMathf.Acos(dot);
            float theta = theta0 * t;

            XIVQuaternion q = (b - a * dot).normalized;
            return a * XIVMathf.Cos(theta) + q * XIVMathf.Sin(theta);
        }

        /// <summary>
        /// Fast spherical interpolation.
        /// Assumptions:
        /// - a and b are normalized
        /// - dot(a, b) >= 0
        /// - t is in [0, 1]
        /// - quaternions are not nearly identical
        /// </summary>
        public static XIVQuaternion SlerpFast(in XIVQuaternion a, in XIVQuaternion b, float t)
        {
            float dot = Dot(a, b);
            float theta = XIVMathf.Acos(dot) * t;

            XIVQuaternion q = new XIVQuaternion(
                b.x - a.x * dot,
                b.y - a.y * dot,
                b.z - a.z * dot,
                b.w - a.w * dot
            ).normalized;

            return a * XIVMathf.Cos(theta) + q * XIVMathf.Sin(theta);
        }

        /// <summary>
        /// Rotates from one rotation towards another by a maximum angle.
        /// </summary>
        public static XIVQuaternion RotateTowards(XIVQuaternion from, XIVQuaternion to, float maxDegrees)
        {
            float angle = Angle(from, to);
            if (angle <= XIVMathf.Epsilon)
                return to;

            float t = XIVMathf.Min(1f, maxDegrees / angle);
            return Slerp(from, to, t);
        }

        /// <summary>
        /// Fast rotation towards another rotation.
        /// Assumptions:
        /// - from and to are normalized
        /// - maxDegrees is non-negative
        /// </summary>
        public static XIVQuaternion RotateTowardsFast(in XIVQuaternion from, in XIVQuaternion to, float maxDegrees)
        {
            float dot = Dot(from, to);
            float angle = XIVMathf.Acos(dot) * 2f * XIVMathf.Rad2Deg;

            if (angle <= maxDegrees)
                return to;

            float t = maxDegrees / angle;
            return SlerpFast(in from, in to, t);
        }

        // ------------------------------------------------------------
        // Angle utilities
        // ------------------------------------------------------------

        /// <summary>
        /// Returns the angular difference in degrees.
        /// </summary>
        public static float Angle(XIVQuaternion a, XIVQuaternion b)
        {
            float dot = XIVMathf.Min(XIVMathf.Abs(Dot(a, b)), 1f);
            return XIVMathf.Acos(dot) * 2f * XIVMathf.Rad2Deg;
        }

        /// <summary>
        /// Converts the rotation to angle-axis representation.
        /// </summary>
        public void ToAngleAxis(out float angleDeg, out Vec3 axis)
        {
            XIVQuaternion q = normalized;
            angleDeg = 2f * XIVMathf.Acos(q.w) * XIVMathf.Rad2Deg;

            float s = XIVMathf.Sqrt(1f - q.w * q.w);
            if (s < XIVMathf.Epsilon) axis = new Vec3(1f, 0f, 0f);
            else axis = new Vec3(q.x / s, q.y / s, q.z / s);
        }

        // ------------------------------------------------------------
        // Overrides
        // ------------------------------------------------------------

        public override bool Equals(object obj)
        {
            return obj is XIVQuaternion q && this == q;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
        }

        public override string ToString()
        {
            return $"({x:F3}, {y:F3}, {z:F3}, {w:F3})";
        }

        // ------------------------------------------------------------
        // Internal helpers
        // ------------------------------------------------------------

        public static XIVQuaternion operator +(XIVQuaternion a, XIVQuaternion b)
        {
            return new XIVQuaternion(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static XIVQuaternion operator -(XIVQuaternion a, XIVQuaternion b)
        {
            return new XIVQuaternion(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }
        
        /// <summary>
        /// Creates a rotation that rotates from one direction to another.
        /// </summary>
        public static XIVQuaternion FromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            Vec3 from = fromDirection.normalized;
            Vec3 to = toDirection.normalized;

            float dot = Vec3.Dot(from, to);

            // Directions are the same
            if (dot > 1f - XIVMathf.Epsilon)
                return identity;

            // Directions are opposite
            if (dot < -1f + XIVMathf.Epsilon)
            {
                // Find an arbitrary perpendicular axis
                Vec3 axis = Vec3.Cross(from, new Vec3(1f, 0f, 0f));
                if (axis.sqrMagnitude < XIVMathf.Epsilon)
                    axis = Vec3.Cross(from, new Vec3(0f, 1f, 0f));

                return AngleAxis(180f, axis.normalized);
            }

            // General case
            Vec3 cross = Vec3.Cross(from, to);

            return new XIVQuaternion(
                cross.x,
                cross.y,
                cross.z,
                1f + dot
            ).normalized;
        }
    }
}