using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Lunha.DevKit.Editor.Utilities
{
    [UsedImplicitly]
    public static class RaycastUtility
    {
        public static RaycastHit2D UIToWorldRaycastHit(Vector2 mousePosition, string tag = "")
        {
            var worldRay = HandleUtility.GUIPointToWorldRay(mousePosition);
            var results = new RaycastHit2D[] { };
            var size = Physics2D.RaycastNonAlloc(worldRay.origin, worldRay.direction, results);

            return tag != string.Empty ? results.FirstOrDefault(i => i.transform.CompareTag(tag)) : results[0];
        }

        public static RaycastHit2D UIToWorldRaycastHit(string tag = "") =>
            UIToWorldRaycastHit(Event.current.mousePosition, tag);
    }
}