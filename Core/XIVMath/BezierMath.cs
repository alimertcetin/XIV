﻿using UnityEngine;
using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    public struct CurveData
    {
        public Vector3 point;
        public Vector3 normal;
        public Vector3 right;
        public Vector3 forward;
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
        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * oneMinusT * p0 + 3f * oneMinusT * oneMinusT * t * p1 + 3f * oneMinusT * t * t * p2 + t * t * t * p3;
        }

        /// <summary>
        /// Returns the first derivative of bezier curve
        /// </summary>
        /// <param name="t">Time between 0 and 1</param>
        public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return 3f * oneMinusT * oneMinusT * (p1 - p0) + 6f * oneMinusT * t * (p2 - p1) + 3f * t * t * (p3 - p2);
        }

        public static float GetTime(Vector3 currentPosition, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float tolarence = TOLERANCE, int iteration = GET_TIME_ITERATION_COUNT)
        {
            float currentGuess = 0.5f; // initial guess for t
            
            for (int i = 0; i < iteration; i++)
            {
                Vector3 pointOnCurve = GetPoint(p0, p1, p2, p3, currentGuess);
                Vector3 tangentAtPoint = GetFirstDerivative(p0, p1, p2, p3, currentGuess);
                float distanceToTarget = Vector3.Distance(currentPosition, pointOnCurve);
                float slopeOfDistance = Vector3.Dot(currentPosition - pointOnCurve, tangentAtPoint);

                if (distanceToTarget < tolarence)
                {
                    break;
                }

                currentGuess += slopeOfDistance / tangentAtPoint.sqrMagnitude;
                currentGuess = Mathf.Clamp01(currentGuess);
            }

            return currentGuess;
        }

        public static Vector3[] CreateCurve(Vector3 start, Vector3 end, float midPointDistance = 1f)
        {
            var mid = (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            return new Vector3[]
            {
                start,
                mid + (dirToStart * 0.5f) + Random.insideUnitSphere * midPointDistance,
                mid + (dirToEnd * 0.5f) + Random.insideUnitSphere * midPointDistance,
                end,
            };
        }

        /// <summary>
        /// Writes the points to the buffer starting from <paramref name="startIndex"/> and ends in <paramref name="startIndex"/> + 3
        /// </summary>
        public static void CreateCurveNonAlloc(Vector3 start, Vector3 end, Vector3[] buffer, int startIndex, float midPointDistance = 1f)
        {
            var mid = (end - start) * 0.5f;
            var dirToStart = start - mid;
            var dirToEnd = end - mid;
            var mid1 = mid + (dirToStart * 0.5f) + Random.insideUnitSphere * midPointDistance;
            var mid2 = mid + (dirToEnd * 0.5f) + Random.insideUnitSphere * midPointDistance;

            buffer[startIndex] = start;
            buffer[startIndex + 1] = mid1;
            buffer[startIndex + 2] = mid2;
            buffer[startIndex + 3] = end;
        }
        
        public static CurveData GetCurveData(XIVMemory<Vector3> points, float t)
        {
            if (points.Length != 4)
            {
                Debug.LogError("Bezier curve requires exactly 4 control points.");
                return new CurveData();
            }

            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * points[0] + 3 * uu * t * points[1] + 3 * u * tt * points[2] + ttt * points[3];

            // Calculate derivative for normal, right, and forward vectors
            Vector3 p1 = 3 * uu * (points[1] - points[0]);
            Vector3 p2 = 6 * u * t * (points[2] - points[1]);
            Vector3 p3 = 3 * tt * (points[3] - points[2]);

            Vector3 derivative = p1 + p2 + p3;

            // Calculate normal, right, and forward vectors
            Vector3 normal = Vector3.Cross(derivative, Vector3.up).normalized;
            Vector3 right = Vector3.Cross(normal, derivative).normalized;
            Vector3 forward = Vector3.Cross(right, normal).normalized;

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