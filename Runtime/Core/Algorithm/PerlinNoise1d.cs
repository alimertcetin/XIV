using System;
using XIV.Core.Utils;

namespace XIV.Core.Algorithm
{
    public class PerlinNoise1d
    {
        const int PERMUTATION_ARRAY_LENGTH = 256;
        readonly int[] permutations;

        public PerlinNoise1d(int seed)
        {
            var rnd = (seed == 0) ? new Random() : new Random(seed);
            permutations = new int[PERMUTATION_ARRAY_LENGTH * 2];

            // Create base permutation (0–255)
            using var temp = ArrayUtils.GetBuffer(out int[] basePermutations, PERMUTATION_ARRAY_LENGTH);
            for (int i = 0; i < PERMUTATION_ARRAY_LENGTH; i++)
            {
                basePermutations[i] = i;
            }

            // Shuffle using Fisher-Yates
            for (int i = PERMUTATION_ARRAY_LENGTH - 1; i > 0; i--)
            {
                int j = rnd.Next(i + 1);
                (basePermutations[i], basePermutations[j]) = (basePermutations[j], basePermutations[i]);
            }

            // Duplicate array for overflow handling
            for (int i = 0; i < PERMUTATION_ARRAY_LENGTH * 2; i++)
            {
                permutations[i] = basePermutations[i & (PERMUTATION_ARRAY_LENGTH - 1)];
            }
        }

        // Fade function smooths the interpolation curve
        static double Fade(double t)
        {
            // 6t^5 - 15t^4 + 10t^3
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        // Linear interpolation
        static double Lerp(double a, double b, double t)
        {
            return a + t * (b - a);
        }

        // Gradient: generates direction (-1 or +1) based on hash
        static double Gradient(int hash, double distance)
        {
            // Bitwise trick: even → +distance, odd → -distance
            return ((hash & 1) == 0) ? distance : -distance;
        }

        /// <summary>
        /// Compute 1D Perlin noise at coordinate x.
        /// </summary>
        /// <returns>Value between 0 and 1</returns>
        public double Noise(double x)
        {
            // Integer part of x — the left grid point
            int leftGrid = (int)Math.Floor(x) & (PERMUTATION_ARRAY_LENGTH - 1);

            // Fractional part of x — position within the cell
            double localX = x - Math.Floor(x);

            // Right grid point (next cell)
            int rightGrid = (leftGrid + 1) & (PERMUTATION_ARRAY_LENGTH - 1);

            // Fetch random hash values for gradients at left/right
            int leftHash = permutations[leftGrid];
            int rightHash = permutations[rightGrid];

            // Compute gradient contributions
            double leftGradient = Gradient(leftHash, localX);
            double rightGradient = Gradient(rightHash, localX - 1.0);

            // Smooth interpolation between two gradients
            double u = Fade(localX);
            double result = Lerp(leftGradient, rightGradient, u);

            // Normalize from [-1, 1] → [0, 1]
            return (result + 1.0) / 2.0;
        }

        /// <summary>
        /// Adds multiple noise layers (octaves) for richer detail.
        /// </summary>
        public double OctaveNoise(double x, double frequency = 0.25, int octaves = 2, double persistence = 0.5, double lacunarity = 2.0)
        {
            double total = 0;
            double amplitude = 1;
            double maxValue = 0;

            for (int i = 0; i < octaves; i++)
            {
                double freq = frequency * Math.Pow(lacunarity, i);
                total += Noise(x * freq) * amplitude;
                maxValue += amplitude;

                amplitude *= persistence;
            }

            return total / maxValue;
        }
    }
}