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
        
        public static CurveData GetCurveData(Vec3 p0, Vec3 p1, Vec3 p2, Vec3 p3, float t)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            // position
            Vec3 p = uuu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + ttt * p3;

            // derivative (tangent)
            Vec3 d1 = 3 * uu * (p1 - p0);
            Vec3 d2 = 6 * u * t * (p2 - p1);
            Vec3 d3 = 3 * tt * (p3 - p2);
            Vec3 derivative = d1 + d2 + d3;

            // guard
            Vec3 forward;
            if (derivative.sqrMagnitude <= float.Epsilon)
            {
                // fallback forward if derivative is zero: choose direction from P0->P3
                forward = (p3 - p0).normalized;
                if (forward.sqrMagnitude <= float.Epsilon) forward = Vec3.forward; // final fallback
            }
            else
            {
                forward = derivative.normalized;
            }

            // choose a stable up reference that is not parallel to forward
            Vec3 worldUp = Vec3.up;
            if (XIVMathf.Abs(Vec3.Dot(forward, worldUp)) > 0.999f)
            {
                // forward is nearly parallel to worldUp — pick another up
                worldUp = Vec3.right;
            }

            Vec3 right = Vec3.Cross(worldUp, forward);
            if (right.sqrMagnitude <= float.Epsilon)
            {
                // last-ditch fallback
                right = Vec3.Cross(forward, Vec3.forward);
                if (right.sqrMagnitude <= float.Epsilon) right = Vec3.right;
            }
            right = right.normalized;

            Vec3 normal = Vec3.Cross(forward, right).normalized;

            return new CurveData
            {
                point = p,
                normal = normal,
                right = right,
                forward = forward
            };
        }

        public static CurveData GetCurveData(XIVMemory<Vec3> curve, float t)
        {
            if (curve.Length != 4) throw new System.InvalidOperationException("Bezier curve requires exactly 4 control points.");
            return GetCurveData(curve[0], curve[1], curve[2], curve[3], t);
        }
    }
}