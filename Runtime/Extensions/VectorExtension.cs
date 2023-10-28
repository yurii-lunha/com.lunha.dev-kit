using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class VectorExtension
    {
        public static Vector3 RandomRange(this Vector3 range)
        {
            return new Vector3(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y),
                Random.Range(-range.z, range.z)
            );
        }

        public static Vector2 RandomRange(this Vector2 range)
        {
            return new Vector2(
                Random.Range(-range.x, range.x),
                Random.Range(-range.y, range.y)
            );
        }
    }
}