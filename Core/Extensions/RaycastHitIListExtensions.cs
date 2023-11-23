using System.Collections.Generic;
using UnityEngine;

namespace XIV.Core.Extensions
{
    public static class RaycastHitIListExtensions
    {
        /// <summary>
        /// Searches the closest <see cref="RaycastHit"/> by comparing <paramref name="currentPosition"/> and <see cref="Transform"/>.<see cref="Transform.position"/>.
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition, int length, out float distance)
        {
            distance = float.MaxValue;
            if (length == 0) return default;

            RaycastHit selected = default;

            for (int i = 0; i < length; i++)
            {
                var current = searchArray[i];
                var dis = Vector3.Distance(currentPosition, current.transform.position);
                if (dis < distance)
                {
                    distance = dis;
                    selected = current;
                }
            }
            
            return selected;
        }
        
        /// <summary>
        /// <inheritdoc cref="GetClosest(System.Collections.Generic.IList{RaycastHit},UnityEngine.Vector3,int,out float)"/>
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition, out float distance)
        {
            var length = searchArray.Count;
            return GetClosest(searchArray, currentPosition, length, out distance);
        }
        
        /// <summary>
        /// <inheritdoc cref="GetClosest(System.Collections.Generic.IList{RaycastHit},UnityEngine.Vector3,int,out float)"/>
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition)
        {
            var length = searchArray.Count;
            return GetClosest(searchArray, currentPosition, length, out _);
        }
        
        /// <summary>
        /// <inheritdoc cref="GetClosest(System.Collections.Generic.IList{RaycastHit},UnityEngine.Vector3,int,out float)"/>
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition, int length)
        {
            return GetClosest(searchArray, currentPosition, length, out _);
        }

        /// <summary>
        /// <inheritdoc cref="GetClosest(System.Collections.Generic.IList{RaycastHit},UnityEngine.Vector3,int,out float)"/>
        /// Excludes the items in <paramref name="exclude"/> array.
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition, out float distance, params RaycastHit[] exclude)
        {
            var length = searchArray.Count;
            distance = float.MaxValue;
            if (length == 0) return default;

            RaycastHit selected = default;

            for (int i = 0; i < length; i++)
            {
                var current = searchArray[i];
                var dis = Vector3.Distance(currentPosition, current.transform.position);
                if (dis < distance && exclude.Contains(current) == false)
                {
                    distance = dis;
                    selected = current;
                }
            }
            
            return selected;
        }

        /// <summary>
        /// <inheritdoc cref="GetClosest(IList{RaycastHit}, Vector3, out float, RaycastHit[])"/>
        /// </summary>
        public static RaycastHit GetClosest(this IList<RaycastHit> searchArray, Vector3 currentPosition, params RaycastHit[] exclude)
        {
            return GetClosest(searchArray, currentPosition, out _, exclude);
        }
        
        public static RaycastHit GetClosestOnXZPlane(this IList<RaycastHit> searchArray, Vector3 currentPosition, out float distance)
        {
            var length = searchArray.Count;
            distance = float.MaxValue;
            if (length == 0) return default;

            RaycastHit selected = default;

            currentPosition = currentPosition.OnXZ();
            for (int i = 0; i < length; i++)
            {
                var current = searchArray[i];
                var dis = Vector3.Distance(currentPosition, current.transform.position.OnXZ());
                if (dis < distance)
                {
                    distance = dis;
                    selected = current;
                }
            }
            
            return selected;
        }
        
        public static RaycastHit GetClosestOnXZPlane<T>(this IList<RaycastHit> searchArray, Vector3 currentPosition)
        {
            return GetClosestOnXZPlane(searchArray, currentPosition, out _);
        }
    }
}