using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Utilities
{
    [UsedImplicitly]
    public static class PathUtility
    {
        public static Vector3[] TransformToVectorPath(Transform[] path)
        {
            var vectorPath = new Vector3[path.Length];
            for (var i = 0; i < path.Length; i++) vectorPath[i] = path[i].position;
            
            return vectorPath;
        }

        public static float CalculatePathDuration
        (
            Vector3[] path,
            Vector3 startPosition,
            float speed = 1f,
            bool run = false,
            float runSpeed = 3f,
            bool jump = false,
            float jumpSpeed = 5f
        )
        {
            var duration = 0f;

            duration += Vector2.Distance(startPosition, path[0]);
            for (var i = 1; i < path.Length; i++) duration += Vector2.Distance(path[i - 1], path[i]);

            duration *= speed;

            if (run)
            {
                duration *= runSpeed;
            }

            if (jump)
            {
                duration *= jumpSpeed;
            }

            return duration;
        }

        public static float CalculatePathDuration
        (
            Transform[] path,
            Vector3 startPosition,
            float speed = 1f,
            bool run = false,
            float runSpeed = 3f,
            bool jump = false,
            float jumpSpeed = 5f
        )
        {
            return CalculatePathDuration(TransformToVectorPath(path), startPosition, speed, run, runSpeed, jump,
                jumpSpeed);
        }
    }
}