using UnityEngine;

namespace Lunha.DevKit.CrossDevice
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] bool vertical;
        [SerializeField] bool horizontal;

        void OnEnable()
        {
            if (!(transform is RectTransform rectTransform)) return;

            var safeRect = Screen.safeArea;

            if (!horizontal)
            {
                safeRect.x = 0;
                safeRect.width = Screen.width;
            }

            if (!vertical)
            {
                safeRect.y = 0;
                safeRect.height = Screen.height;
            }

            var anchorMin = safeRect.position;
            var anchorMax = safeRect.position + safeRect.size;

            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;

            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}