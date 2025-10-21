using System;
using XIV.Core.Utils;

namespace XIV.Core.Algorithm
{
    public class PerlinNoise2d
    {
        const int PERMUTATION_ARRAY_LENGTH = 256;
        readonly int[] permutations;

        public PerlinNoise2d(int seed = 0)
        {
            var rnd = (seed == 0) ? new Random() : new Random(seed);
            permutations = new int[PERMUTATION_ARRAY_LENGTH * 2];

            // Create base permutation (0–PERMUTATION_ARRAY_LENGTH)
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

        static double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        static double Lerp(double a, double b, double t)
        {
            return a + t * (b - a);
        }

        // 2D gradient directions from hash
        static double Gradient(int hash, double x, double y)
        {
            // Pick one of 8 possible directions
            int h = hash & 7;
            double u = h < 4 ? x : y;
            double v = h < 4 ? y : x;
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }

        /// <summary>
        /// Compute 2D Perlin noise at coordinate (x, y).
        /// Returns value between 0 and 1.
        /// </summary>
        public double Noise(double x, double y)
        {
            // Identify grid cell coordinates
            int xi = (int)Math.Floor(x) & (PERMUTATION_ARRAY_LENGTH - 1);
            int yi = (int)Math.Floor(y) & (PERMUTATION_ARRAY_LENGTH - 1);

            // Relative position within the cell
            double xf = x - Math.Floor(x);
            double yf = y - Math.Floor(y);

            // Hash corner coordinates to get gradient indices
            int aa = permutations[permutations[xi] + yi];
            int ab = permutations[permutations[xi] + yi + 1];
            int ba = permutations[permutations[xi + 1] + yi];
            int bb = permutations[permutations[xi + 1] + yi + 1];

            // Compute gradient dot products for each corner
            double gradAA = Gradient(aa, xf, yf);
            double gradBA = Gradient(ba, xf - 1, yf);
            double gradAB = Gradient(ab, xf, yf - 1);
            double gradBB = Gradient(bb, xf - 1, yf - 1);

            // Smooth interpolation weights
            double u = Fade(xf);
            double v = Fade(yf);

            // Interpolate along x, then y
            double lerpX1 = Lerp(gradAA, gradBA, u);
            double lerpX2 = Lerp(gradAB, gradBB, u);
            double result = Lerp(lerpX1, lerpX2, v);

            // Normalize [-1,1] to [0,1]
            return (result + 1.0) / 2.0;
        }

        public double OctaveNoise(double x, double y, double frequency = 0.25, int octaves = 2, double persistence = 0.5, double lacunarity = 2.0)
        {
            double total = 0;
            double amplitude = 1;
            double maxValue = 0;

            for (int i = 0; i < octaves; i++)
            {
                double freq = frequency * Math.Pow(lacunarity, i);
                total += Noise(x * freq, y * freq) * amplitude;
                maxValue += amplitude;

                amplitude *= persistence;
            }

            return total / maxValue;
        }
    }
}