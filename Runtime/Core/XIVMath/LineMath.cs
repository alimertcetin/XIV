using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
    public static class LineMath
    {
        public static bool IsPointOnTheLine(Vec3 lineStart, Vec3 lineEnd, Vec3 point, float distanceThreshold = 0.1f)
        {
            var closestPoint = GetClosestPointOnLineSegment(lineStart, lineEnd, point);
            return Vec3.Distance(closestPoint, point) < distanceThreshold;
        }
    
        public static Vec3 GetClosestPointOnLineSegment(Vec3 lineStart, Vec3 lineEnd, Vec3 point)
        {
            Vec3 lineDirection = lineEnd - lineStart;
            float lineLength = lineDirection.magnitude;
            lineDirection /= lineLength;
 
            float dotProduct = Vec3.Dot(lineDirection, point - lineStart);
            dotProduct = XIVMathf.Clamp(dotProduct, 0f, lineLength);
 
            return lineStart + lineDirection * dotProduct;
        }
        
        public static bool IsIntersect(Vec3 p0, Vec3 p1, Vec3 p3, Vec3 p4, out Vec3 intersectionPoint, float distanceThreshold = 0.01f)
        {
            intersectionPoint = Vec3.zero;
            Vec3 u = p1 - p0; // Direction vector of first segment
            Vec3 v = p4 - p3; // Direction vector of second segment
            Vec3 w = p0 - p3;

            float a = Vec3.Dot(u, u); // squared length of u
            float b = Vec3.Dot(u, v);
            float c = Vec3.Dot(v, v); // squared length of v
            float d = Vec3.Dot(u, w);
            float e = Vec3.Dot(v, w);

            float denom = a * c - b * b;

            float sc, tc;

            // If denom == 0, lines are parallel
            if (XIVMathf.Abs(denom) < XIVMathf.Epsilon)
            {
                // Lines are parallel: check if they are colinear and overlapping
                // Check if distance between line p0-p1 and p3 is small
                Vec3 closestPointOnFirst = GetClosestPointOnLineSegment(p0, p1, p3);
                if (Vec3.Distance(closestPointOnFirst, p3) < distanceThreshold)
                {
                    // They are colinear, now check if segments overlap by projecting onto the direction vector u
                    float t0 = Vec3.Dot((p3 - p0), u) / a; // parameter on first line for p3
                    float t1 = Vec3.Dot((p4 - p0), u) / a; // parameter on first line for p4

                    // Check if [t0, t1] overlaps with [0,1]
                    if ((t0 >= 0 && t0 <= 1) || (t1 >= 0 && t1 <= 1) || (t0 < 0 && t1 > 1) || (t1 < 0 && t0 > 1))
                        return true;
                }
                return false; // Parallel but not colinear or no overlap
            }

            sc = (b * e - c * d) / denom;
            tc = (a * e - b * d) / denom;

            // Clamp sc and tc to segment extents [0,1]
            sc = XIVMathf.Clamp(sc, 0f, 1f);
            tc = XIVMathf.Clamp(tc, 0f, 1f);

            Vec3 pointOnFirst = p0 + u * sc;
            Vec3 pointOnSecond = p3 + v * tc;

            // Check distance between the closest points
            if (Vec3.Distance(pointOnFirst, pointOnSecond) <= distanceThreshold)
            {
                intersectionPoint = (pointOnFirst + pointOnSecond) * 0.5f;
                return true;
            }

            return false;
        }

        public static bool IsIntersect(Vec3 p0, Vec3 p1, Vec3 p3, Vec3 p4, float distanceThreshold = 0.01f)
        {
            return IsIntersect(p0, p1, p3, p4, out _, distanceThreshold);
        }
    }
}