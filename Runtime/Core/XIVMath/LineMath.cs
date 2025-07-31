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
    }
}