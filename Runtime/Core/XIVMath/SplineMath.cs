using System.Collections.Generic;
using XIV.Core.DataStructures;

namespace XIV.Core.XIVMath
{
	/// <summary>
	/// Spline Math for Cubic Bezier Curves
	/// </summary>
	public static class SplineMath
	{
		/// <summary>
		/// Returns the point at giving <paramref name="t"/> time
		/// </summary>
		/// <param name="points">Spline points</param>
		/// <param name="t">Time between 0 and 1</param>
		/// <returns>The point at giving <paramref name="t"/> time</returns>
		public static Vec3 GetPoint(IList<Vec3> points, float t)
		{
			int curveCount = (points.Count - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Count - 4;
			}
			else
			{
				t = XIVMathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return BezierMath.GetPoint(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}
		/// <summary>
		/// Returns the point at giving <paramref name="t"/> time
		/// </summary>
		/// <param name="points">Spline points</param>
		/// <param name="t">Time between 0 and 1</param>
		/// <returns>The point at giving <paramref name="t"/> time</returns>
		public static Vec3 GetPoint(XIVMemory<Vec3> points, float t)
		{
			int curveCount = (points.Length - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Length - 4;
			}
			else
			{
				t = XIVMathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return BezierMath.GetPoint(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}

		public static float GetTime(Vec3 currentPosition, IList<Vec3> points, float tolerance = 0.01f)
		{
			int curveCount = (points.Count - 1) / 3;
			float minDistance = float.MaxValue;
			float t = 0f;

			for (int i = 0; i < curveCount; i++)
			{
				int index = i * 3;
				Vec3 p0 = points[index];
				Vec3 p1 = points[index + 1];
				Vec3 p2 = points[index + 2];
				Vec3 p3 = points[index + 3];

				for (float j = 0f; j <= 1f; j += tolerance)
				{
					Vec3 curvePosition = BezierMath.GetPoint(p0, p1, p2, p3, j);
					float distance = Vec3.Distance(curvePosition, currentPosition);
					
					if (distance < minDistance)
					{
						minDistance = distance;
						t = (i + j) / curveCount; // Calculate the t value relative to the entire curve
					}
				}
			}

			return t;
		}

		public static float GetTime(Vec3 currentPosition, XIVMemory<Vec3> points, float tolerance = 0.01f)
		{
			int curveCount = (points.Length - 1) / 3;
			float minDistance = float.MaxValue;
			float t = 0f;

			for (int i = 0; i < curveCount; i++)
			{
				int index = i * 3;
				Vec3 p0 = points[index];
				Vec3 p1 = points[index + 1];
				Vec3 p2 = points[index + 2];
				Vec3 p3 = points[index + 3];

				for (float j = 0f; j <= 1f; j += tolerance)
				{
					Vec3 curvePosition = BezierMath.GetPoint(p0, p1, p2, p3, j);
					float distance = Vec3.Distance(curvePosition, currentPosition);
					
					if (distance < minDistance)
					{
						minDistance = distance;
						t = (i + j) / curveCount; // Calculate the t value relative to the entire curve
					}
				}
			}

			return t;
		}

		/// <summary>
		/// Returns the Velocity of spline at <paramref name="t"/> point
		/// </summary>
		/// <param name="points">Spline points</param>
		/// <param name="t">Time between 0 and 1</param>
		/// <returns>The Velocity of spline at <paramref name="t"/> point</returns>
		public static Vec3 GetVelocity(IList<Vec3> points, float t)
		{
			int curveCount = (points.Count - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Count - 4;
			}
			else
			{
				t = XIVMathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return BezierMath.GetFirstDerivative(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}
		
		/// <summary>
		/// <inheritdoc cref="GetVelocity(IList{Vec3}, float)"/>
		/// <param name="points">Spline points</param>
		/// <param name="t">Time between 0 and 1</param>
		/// </summary>
		public static Vec3 GetVelocity(XIVMemory<Vec3> points, float t)
		{
			int curveCount = (points.Length - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Length - 4;
			}
			else
			{
				t = XIVMathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return BezierMath.GetFirstDerivative(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}
        
		/// <summary>
		/// Returns control point index of anchor
		/// </summary>
		/// <param name="anchorIndex">Anchor index</param>
		/// <returns>Control point index of anchor</returns>
		public static int IndexOfControlPoint(int anchorIndex)
		{
			int mod = anchorIndex % 3;
			if (mod == 0) return anchorIndex;
            
			// mod == 2 previous anchor, mod == 1 next anchor
			return mod == 2 ? anchorIndex + 1 : anchorIndex - 1;
		}

		/// <summary>
		/// Returns true if index is an anchor point, false otherwise
		/// </summary>
		/// <param name="index">The index of point</param>
		/// <returns>True if index is anchor point, false otherwise</returns>
		public static bool IsAnchorPoint(int index)
		{
			return IndexOfControlPoint(index) != index;
		}

		public static float GetLength(IList<Vec3> points, int stepsPerCurve = 10)
		{
			int steps = stepsPerCurve * ((points.Count - 1) / 3);
			var p0 = GetPoint(points, 0);
			float length = 0f;
			for (int i = 1; i <= steps; i++)
			{
				float t = i / (float)steps;
				var p1 = GetPoint(points, t);
				length += (p0 - p1).magnitude;
				p0 = p1;
			}

			return length;
		}

		public static float GetLength(XIVMemory<Vec3> points, int stepsPerCurve = 10)
		{
			int steps = stepsPerCurve * ((points.Length - 1) / 3);
			var p0 = GetPoint(points, 0);
			float length = 0f;
			for (int i = 1; i <= steps; i++)
			{
				float t = i / (float)steps;
				var p1 = GetPoint(points, t);
				length += (p0 - p1).magnitude;
				p0 = p1;
			}

			return length;
		}
	}
}