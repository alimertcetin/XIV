using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    public struct CurveData
    {
        public Vec3 point;
        public Vec3 normal;
        public Vec3 right;
        public Vec3 forward;
    }
    /// <summary>
    /// Cubic Bezier Math class
    /// </summary>
    public static class BezierMath
    {
        const float TOLERANCE = 0.0001f;
        const int GET_TIME_ITERATION_COUNT = 10;

        /// <summary>
        /// Returns the point at curve depending on <paramref name="t"/> time
        /// </summary>
        /// <param name="t">Time between 0 and 1</param>
        public static Vec3 GetPoint(Vec3 p0, Vec3 p1, Vec3 p2, Vec3 p3, float t)
        {
            t = XIVMathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * p0 + 3f * oneMinusT * oneMinusT * t * p1 + 3f * oneMinusT * t * t * p2 + t * t * t * p3;
        }

        /// <summary>
        /// Returns the first derivative of bezier curve
        /// </summary>
        /// <param name="t">Time between 0 and 1</param>
        public static Vec3 GetFirstDerivative(Vec3 p0, Vec3 p1, Vec3 p2, Vec3 p3, float t)
        {
            t = XIVMathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return 3f * oneMinusT * oneMinusT * (p1 - p0) + 6f * oneMinusT * t * (p2 - p1) + 3f * t * t * (p3 - p2);
        }

        public static float GetTime(Vec3 currentPosition, Vec3 p0, Vec3 p1, Vec3 p2, Vec3 p3, float tolarence = TOLERANCE, int iteration = GET_TIME_ITERATION_COUNT)
        {
            float currentGuess = 0.5f; // initial guess for t
            
            for (int i = 0; i < iteration; i++)
            {
                Vec3 pointOnCurve = GetPoint(p0, p1, p2, p3, currentGuess);
                Vec3 tangentAtPoint = GetFirstDerivative(p0, p1, p2, p3, currentGuess);
                float distanceToTarget = Vec3.Distance(currentPosition, pointOnCurve);
                float slopeOfDistance = Vec3.Dot(currentPosition - pointOnCurve, tangentAtPoint);

                if (distanceToTarget < tolarence)
                {
                    break;
                }

                currentGuess += slopeOfDistance / tangentAtPoint.sqrMagnitude;
                currentGuess = XIVMathf.Clamp01(currentGuess);
            }

            return currentGuess;
        }

        public static Vec3[] CreateCurve(Vec3 start, Vec3 end, float midPointDistance = 1f)
        {
            var mid = (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            return new Vec3[]
            {
                start,
                mid + (dirToStart * 0.5f) + XIVRandom.insideUnitSphere * midPointDistance,
                mid + (dirToEnd * 0.5f) + XIVRandom.insideUnitSphere * midPointDistance,
                end,
            };
        }

        public static Vec3[] CreateArc(Vec3 start, Vec3 end, float midPointDistance = 1f)
        {
            return CreateArc(start, end, Vec3.up, midPointDistance);
        }
        
        public static Vec3[] CreateArc(Vec3 start, Vec3 end, Vec3 up, float midPointDistance = 1f)
        {
            var mid = start + (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            return new Vec3[]
            {
                start,
                mid + (dirToStart * 0.5f) + up * midPointDistance,
                mid + (dirToEnd * 0.5f) + up * midPointDistance,
                end,
            };
        }

        /// <summary>
        /// Writes the points to the buffer starting from <paramref name="startIndex"/> and ends in <paramref name="startIndex"/> + 3
        /// </summary>
        public static void CreateCurveNonAlloc(Vec3 start, Vec3 end, XIVMemory<Vec3> buffer, int startIndex, float midPointDistance = 1f)
        {
            var mid = (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            var mid1 = mid + (dirToStart * 0.5f) + XIVRandom.insideUnitSphere * midPointDistance;
            var mid2 = mid + (dirToEnd * 0.5f) + XIVRandom.insideUnitSphere * midPointDistance;

            buffer[startIndex] = start;
            buffer[startIndex + 1] = mid1;
            buffer[startIndex + 2] = mid2;
            buffer[startIndex + 3] = end;
        }
        
        public static CurveData GetCurveData(XIVMemory<Vec3> points, float t)
        {
            if (points.Length != 4)
            {
                throw new System.InvalidOperationException("Bezier curve requires exactly 4 control points.");
            }

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vec3 p = uuu * points[0] + 3 * uu * t * points[1] + 3 * u * tt * points[2] + ttt * points[3];

            // Calculate derivative for normal, right, and forward vectors
            Vec3 p1 = 3 * uu * (points[1] - points[0]);
            Vec3 p2 = 6 * u * t * (points[2] - points[1]);
            Vec3 p3 = 3 * tt * (points[3] - points[2]);

            Vec3 derivative = p1 + p2 + p3;

            // Calculate normal, right, and forward vectors
            Vec3 normal = Vec3.Cross(derivative, Vec3.up).normalized;
            Vec3 right = Vec3.Cross(normal, derivative).normalized;
            Vec3 forward = Vec3.Cross(right, normal).normalized;

            CurveData curveData = new CurveData
            {
                point = p,
                normal = normal,
                right = right,
                forward = forward
            };

            return curveData;
        }
    }
}