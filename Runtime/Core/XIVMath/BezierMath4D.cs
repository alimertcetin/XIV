using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    public static class BezierMath4D
    {
        const float TOLERANCE = 0.0001f;
        const int GET_TIME_ITERATION_COUNT = 10;

        /// <summary>
        /// Returns the point at curve depending on <paramref name="t"/> time
        /// </summary>
        /// <param name="t">Time between 0 and 1</param>
        public static Vec4 GetPoint(Vec4 p0, Vec4 p1, Vec4 p2, Vec4 p3, float t)
        {
            t = XIVMathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * p0 + 3f * oneMinusT * oneMinusT * t * p1 + 3f * oneMinusT * t * t * p2 + t * t * t * p3;
        }

        /// <summary>
        /// Returns the first derivative of bezier curve
        /// </summary>
        /// <param name="t">Time between 0 and 1</param>
        public static Vec4 GetFirstDerivative(Vec4 p0, Vec4 p1, Vec4 p2, Vec4 p3, float t)
        {
            t = XIVMathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return 3f * oneMinusT * oneMinusT * (p1 - p0) + 6f * oneMinusT * t * (p2 - p1) + 3f * t * t * (p3 - p2);
        }

        public static float GetTime(Vec4 currentPosition, Vec4 p0, Vec4 p1, Vec4 p2, Vec4 p3, float tolarence = TOLERANCE, int iteration = GET_TIME_ITERATION_COUNT)
        {
            float currentGuess = 0.5f; // initial guess for t
            
            for (int i = 0; i < iteration; i++)
            {
                Vec4 pointOnCurve = GetPoint(p0, p1, p2, p3, currentGuess);
                Vec4 tangentAtPoint = GetFirstDerivative(p0, p1, p2, p3, currentGuess);
                float distanceToTarget = Vec4.Distance(currentPosition, pointOnCurve);
                float slopeOfDistance = Vec4.Dot(currentPosition - pointOnCurve, tangentAtPoint);

                if (distanceToTarget < tolarence)
                {
                    break;
                }

                currentGuess += slopeOfDistance / tangentAtPoint.sqrMagnitude;
                currentGuess = XIVMathf.Clamp01(currentGuess);
            }

            return currentGuess;
        }

        public static Vec4[] CreateCurve(Vec4 start, Vec4 end, float midPointDistance = 1f)
        {
            static Vec4 Rnd()
            {
                var rotation = XIVRandom.rotation;
                return new Vec4(rotation.x, rotation.y, rotation.z, rotation.w);
            }
            
            var mid = (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            
            return new Vec4[]
            {
                start,
                mid + (dirToStart * 0.5f) + Rnd() * midPointDistance,
                mid + (dirToStart * 0.5f) + Rnd() * midPointDistance,
                mid + (dirToEnd * 0.5f) + Rnd() * midPointDistance,
                end,
            };
        }
    }
}