using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class CameraExtension
    {
        /// <summary>
        /// Transforms the mouse position from screen space to world space.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 TouchToWorldPoint(this Camera camera)
        {
            var touchPoint = Input.mousePosition;
            var worldPoint = TouchToWorldPoint(camera, touchPoint);
            return worldPoint;
        }

        /// <summary>
        /// Transforms the touch position from screen space to world space
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="touchPoint">Screen space position</param>
        /// <returns></returns>
        public static Vector2 TouchToWorldPoint(this Camera camera, Vector2 touchPoint)
        {
            var worldPoint = camera.ScreenToWorldPoint(new Vector3(touchPoint.x, touchPoint.y, camera.nearClipPlane));
            return worldPoint;
        }

        /// <summary>
        /// Cast a ray using the mouse position and get the first object
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="tag">Filter by tag</param>
        /// <returns></returns>
        public static RaycastHit2D TouchToRaycastHit(this Camera camera, string tag = "")
        {
            var hit = TouchToRaycastHit(camera, Input.mousePosition, tag);
            return hit;
        }

        /// <summary>
        /// Cast a ray using the mouse position
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="tag">Filter by tag</param>
        /// <returns></returns>
        public static RaycastHit2D[] TouchToRaycastHits(this Camera camera, string tag = "")
        {
            var hits = TouchToRaycastHits(camera, Input.mousePosition, tag);
            return hits;
        }

        /// <summary>
        /// Cast a ray using the screen position and get the first object
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="touchPosition">Screen space position</param>
        /// <param name="tag">Filter by tag</param>
        /// <returns></returns>
        public static RaycastHit2D TouchToRaycastHit(this Camera camera, Vector3 touchPosition, string tag = "")
        {
            var hits = TouchToRaycastHits(camera, touchPosition, tag);
            var hit = hits.FirstOrDefault();
            return hit;
        }

        /// <summary>
        /// Cast a ray using the screen position
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="touchPosition">Screen space position</param>
        /// <param name="tag">Filter by tag</param>
        /// <returns></returns>
        public static RaycastHit2D[] TouchToRaycastHits(this Camera camera, Vector3 touchPosition, string tag = "")
        {
            var ray = camera.ScreenPointToRay(touchPosition);
            var hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (tag == string.Empty)
            {
                return hits;
            }

            var tagHits = hits
                .Where(i =>
                    i.collider.CompareTag(tag) ||
                    i.transform.CompareTag(tag)
                )
                .ToArray();

            return tagHits;
        }

        /// <summary>
        /// Cast a ray using the world position
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="worldPosition">Position in world coordinates</param>
        /// <param name="tag">Filter by tag</param>
        /// <returns></returns>
        public static RaycastHit2D WorldToRaycastHit(this Camera camera, Vector2 worldPosition, string tag = "")
        {
            var hit = TouchToRaycastHit(camera, camera.WorldToScreenPoint(worldPosition), tag);
            return hit;
        }
    }
}