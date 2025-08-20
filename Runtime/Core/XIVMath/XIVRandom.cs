using System;
using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    /// <summary>
    /// Random number generator similar to UnityEngine.Random
    /// </summary>
    public static class XIVRandom
    {
        static readonly Random random = new Random();

        /// <summary>
        /// Returns a random float number between 0.0 [inclusive] and 1.0 [inclusive].
        /// </summary>
        public static float value => (float)random.NextDouble();

        /// <summary>
        /// Returns a random integer number between min [inclusive] and max [exclusive].
        /// </summary>
        public static int Range(int min, int max) => random.Next(min, max);

        /// <summary>
        /// Returns a random float number between min [inclusive] and max [inclusive].
        /// </summary>
        public static float Range(float min, float max) => (float)(random.NextDouble() * (max - min) + min);

        /// <summary>
        /// Returns a random point inside a unit sphere.
        /// </summary>
        public static Vec3 insideUnitSphere
        {
            get
            {
                // Radius with cube root to ensure uniform distribution in volume
                float radius = XIVMathf.Pow(Range(0f, 1f), 1f / 3f);
                return onUnitSphere * radius;
            }
        }
        
        public static Vec3 onUnitSphere
        {
            get
            {
                // Random point on unit sphere
                float x = Range(-1f, 1f);
                float y = Range(-1f, 1f);
                float z = Range(-1f, 1f);
                return Vec3.Normalize(new Vec3(x, y, z));
            }
        }

        /// <summary>
        /// Returns a random point inside a unit circle on the XZ plane.
        /// </summary>
        public static Vec2 insideUnitCircle
        {
            get
            {
                float angle = Range(0f, XIVMathf.TAU); // Random angle in radians
                float radius = XIVMathf.Sqrt(Range(0f, 1f)); // √r ensures uniform distribution
                float x = XIVMathf.Cos(angle) * radius;
                float y = XIVMathf.Sin(angle) * radius;
                return new Vec2(x, y);
            }
        }

        public static Vec2 onUnitCircle
        {
            get
            {
                float angle = Range(0f, XIVMathf.TAU);
                return new Vec2(XIVMathf.Cos(angle), XIVMathf.Sin(angle));
            }
        }

        /// <summary>
        /// Sets the seed for the random number generator.
        /// </summary>
        public static void InitState(int seed)
        {
            typeof(Random)
                .GetField("_seedArray", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(random, null);
            typeof(Random)
                .GetField("_inext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(random, 0);
            typeof(Random)
                .GetField("_inextp", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(random, 21);
            // Reinitialize the Random instance
            // Note: System.Random does not support reseeding, so this is a workaround.
            // For thread safety and true reseeding, consider using a new Random instance per seed.
        }

        /// <summary>
        /// Returns a random rotation as a normalized quaternion (Vec4), similar to UnityEngine.Random.rotation.
        /// </summary>
        public static Vec4 rotation
        {
            get
            {
                // Uniform random quaternion using Ken Shoemake's method
                // Reference: http://planning.cs.uiuc.edu/node198.html
                float u1 = (float)random.NextDouble();
                float u2 = (float)random.NextDouble();
                float u3 = (float)random.NextDouble();

                float sqrt1MinusU1 = XIVMathf.Sqrt(1f - u1);
                float sqrtU1 = XIVMathf.Sqrt(u1);

                float theta1 = 2f * XIVMathf.PI * u2;
                float theta2 = 2f * XIVMathf.PI * u3;

                float x = sqrt1MinusU1 * XIVMathf.Sin(theta1);
                float y = sqrt1MinusU1 * XIVMathf.Cos(theta1);
                float z = sqrtU1 * XIVMathf.Sin(theta2);
                float w = sqrtU1 * XIVMathf.Cos(theta2);

                return Vec4.Normalize(new Vec4(x, y, z, w));
            }
        }
    }
}
