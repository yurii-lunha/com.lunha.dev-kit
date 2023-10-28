using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lunha.DevKit.Utilities
{
    [UsedImplicitly]
    public static class PointerUtility
    {
        public static bool IsPointerOverUIObject()
        {
            if (!EventSystem.current)
            {
                return false;
            }

            var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
            {
                position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
            };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Any();
        }

        public static bool IsPointerOverGameObject()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }

            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began
                                     && EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                return true;
            }

            return false;
        }
    }
}