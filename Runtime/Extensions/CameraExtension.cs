using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class CameraExtension
    {
        /// <summary>
        ///     Transforms a Input.mousePosition point from screen space into world space.
        /// </summary>
        /// <param name="camera"></param>
        /// <returns></returns>
        public static Vector2 TouchToWorldPoint(this Camera camera)
        {
            Vector2 touchPoint = Input.mousePosition;

            return camera.ScreenToWorldPoint(new Vector3(touchPoint.x, touchPoint.y, camera.nearClipPlane));
        }

        public static Vector2 TouchToWorldPoint(this Camera camera, Vector2 touchPoint) =>
            camera.ScreenToWorldPoint(new Vector3(touchPoint.x, touchPoint.y, camera.nearClipPlane));

        public static RaycastHit2D TouchToRaycastHit(this Camera camera, string tag = "") =>
            TouchToRaycastHit(camera, Input.mousePosition, tag);

        public static RaycastHit2D[] TouchToRaycastHits(this Camera camera, string tag = "") =>
            TouchToRaycastHits(camera, Input.mousePosition, tag);

        public static RaycastHit2D TouchToRaycastHit(this Camera camera, Vector3 touchPosition, string tag = "")
        {
            var hits = TouchToRaycastHits(camera, touchPosition, tag);

            return tag != string.Empty ? hits.FirstOrDefault(i => i.transform.CompareTag(tag)) : hits[0];
        }

        public static RaycastHit2D[] TouchToRaycastHits(this Camera camera, Vector3 touchPosition, string tag = "")
        {
            var ray = camera.ScreenPointToRay(touchPosition);
            var hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            return tag != string.Empty ? hits.Where(i => i.transform.CompareTag(tag)).ToArray() : hits;
        }

        public static RaycastHit2D WorldToRaycastHit(this Camera camera, Vector2 worldPosition, string tag = "") =>
            TouchToRaycastHit(camera, camera.WorldToScreenPoint(worldPosition), tag);
    }
}