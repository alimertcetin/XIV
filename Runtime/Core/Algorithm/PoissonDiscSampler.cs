using System;
using XIV.Core.Collections;
using XIV.Core.DataStructures;
using XIV.Core.Extensions;
using XIV.Core.Utils;
using XIV.Core.XIVMath;

namespace XIV.Core.Algorithm
{
    public class PoissonDiscSampler
    {
        DynamicArray<Vec2> activeList = new();
        DynamicArray<Vec2> points = new();

        public XIVMemory<Vec2> GeneratePoints(float radius, int sampleAttempts, Vec2 regionSize)
        {
            var cellSize = radius / XIVMathf.Sqrt(2);
            var gridWidth = XIVMathf.CeilToInt(regionSize.x / cellSize);
            var gridHeight = XIVMathf.CeilToInt(regionSize.y / cellSize);
            activeList.Clear();
            points.Clear();
            var minLength = gridWidth * gridHeight;
            if (minLength <= 0) return points.AsXIVMemory();
            
            using var temp = ArrayUtils.GetBuffer(out int[] gridIndices, minLength);
            Array.Fill(gridIndices, -1); // Mark all cells as empty

            Vec2 firstPoint = new Vec2(XIVRandom.value * regionSize.x, XIVRandom.value * regionSize.y);
            AddPoint(firstPoint, gridIndices, cellSize, gridWidth);

            while (activeList.Count > 0)
            {
                Vec2 currentPoint = activeList.XIVPickRandom(out int index);
                bool found = false;

                for (int i = 0; i < sampleAttempts; i++)
                {
                    Vec2 candidate = GenerateRandomPointAround(currentPoint, radius);

                    if (IsValid(candidate, gridIndices, radius, cellSize, regionSize, gridWidth, gridHeight))
                    {
                        AddPoint(candidate, gridIndices, cellSize, gridWidth);
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    activeList.RemoveAt(index);
                }
            }

            return points.AsXIVMemory();
        }

        void AddPoint(Vec2 point, int[] gridIndices, float cellSize, int gridWidth)
        {
            points.Add() = point;
            activeList.Add() = point;
            GetGridPosition(point, cellSize, out int gridX, out int gridY);
            int index = gridY * gridWidth + gridX;
            gridIndices[index] = points.Count - 1;
        }

        Vec2 GenerateRandomPointAround(Vec2 point, float radius)
        {
            float angle = XIVRandom.value * XIVMathf.TAU;
            float distance = radius + XIVRandom.value * radius;
            return point + new Vec2(XIVMathf.Cos(angle), XIVMathf.Sin(angle)) * distance;
        }

        bool IsValid(Vec2 candidate, int[] gridIndices, float radius, float cellSize, Vec2 regionSize, int gridWidth, int gridHeight)
        {
            if (candidate.x < 0 || candidate.x >= regionSize.x || candidate.y < 0 || candidate.y >= regionSize.y)
                return false;

            GetGridPosition(candidate, cellSize, out int cellX, out int cellY);

            int startX = XIVMathInt.Max(0, cellX - 2);
            int endX = XIVMathInt.Min(gridWidth - 1, cellX + 2);
            int startY = XIVMathInt.Max(0, cellY - 2);
            int endY = XIVMathInt.Min(gridHeight - 1, cellY + 2);

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    int index = y * gridWidth + x;
                    int pointIndex = gridIndices[index];
                    if (pointIndex != -1)
                    {
                        Vec2 neighbor = points[pointIndex];
                        if (Vec2.Distance(candidate, neighbor) < radius)
                            return false;
                    }
                }
            }

            return true;
        }

        void GetGridPosition(Vec2 point, float cellSize, out int x, out int y)
        {
            x = XIVMathf.FloorToInt(point.x / cellSize);
            y = XIVMathf.FloorToInt(point.y / cellSize);
        }
    }
}
