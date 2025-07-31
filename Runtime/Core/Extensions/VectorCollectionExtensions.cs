using System.Collections.Generic;
using XIV.Core.DataStructures;
using XIV.Core.XIVMath;

namespace XIV.Core.Extensions
{
    public static class Vec3CollectionExtensions
	{
		/*
		 * Created using OpenAI Assistant
		 */
		public static Vec2[] ToVec2<T>(this T Vec3Collection) where T : IList<Vec3>
		{
			Vec2[] Vec2Array = new Vec2[Vec3Collection.Count];
			for (int i = 0; i < Vec3Collection.Count; i++)
			{
				Vec2Array[i] = new Vec2(Vec3Collection[i].x, Vec3Collection[i].y);
			}
			return Vec2Array;
		}
		
		/*
		 * Created using OpenAI Assistant
		 */
		public static void ToVec2NonAlloc<T>(this T Vec3Collection, Vec2[] Vec2Array) where T : IList<Vec3>
		{
			int count = XIVMathInt.Min(Vec2Array.Length, Vec3Collection.Count);
			for (int i = 0; i < count; i++)
			{
				Vec2Array[i] = Vec3Collection[i];
			}
		}

		/*
		 * Created using OpenAI Assistant
		 */
		public static Vec3[] ToVec3<T>(this T Vec2Collection) where T : IList<Vec2>
		{
			Vec3[] Vec3Array = new Vec3[Vec2Collection.Count];
			for (int i = 0; i < Vec2Collection.Count; i++)
			{
				Vec3Array[i] = new Vec3(Vec2Collection[i].x, Vec2Collection[i].y, 0);
			}
			return Vec3Array;
		}
		
		/*
		 * Created using OpenAI Assistant
		 */
		public static void ToVec3NonAlloc<T>(this T Vec2Collection, Vec3[] Vec3Array) where T : IList<Vec2>
		{
			int count = XIVMathInt.Min(Vec3Array.Length, Vec2Collection.Count);
			for (int i = 0; i < count; i++)
			{
				Vec3Array[i] = Vec2Collection[i];
			}
		}
		
		public static Vec3[] ToVec3XZPlane<T>(this T Vec2Collection) where T : IList<Vec2>
		{
			Vec3[] Vec3Array = new Vec3[Vec2Collection.Count];
			for (int i = 0; i < Vec2Collection.Count; i++)
			{
				Vec3Array[i] = new Vec3(Vec2Collection[i].x, 0f, Vec2Collection[i].y);
			}
			return Vec3Array;
		}
		
		public static void ToVec3XZPlaneNonAlloc<T>(this T Vec2Collection, Vec3[] Vec3Array) where T : IList<Vec2>
		{
			int count = XIVMathInt.Min(Vec3Array.Length, Vec2Collection.Count);
			
			for (int i = 0; i < count; i++)
			{
				Vec3Array[i] = new Vec3(Vec2Collection[i].x, 0f, Vec2Collection[i].y);
			}
		}
	}
}