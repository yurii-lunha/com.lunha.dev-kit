using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Extensions
{
    [UsedImplicitly]
    public static class RectExtensions
    {
        public static Vector3 WorldPosition(this RectTransform rectTransform)
        {
            var wInCorners = new Vector3[4];
            rectTransform.GetWorldCorners(wInCorners);

            var x = wInCorners[2].x + (wInCorners[0].x - wInCorners[2].x) / 2f;
            var y = wInCorners[2].y + (wInCorners[0].y - wInCorners[2].y) / 2f;
            return new Vector3(x, y);
        }

        /// <summary>
        ///     Converts the anchoredPosition of the first RectTransform to the second RectTransform,
        ///     taking into consideration offset, anchors and pivot, and returns the new anchoredPosition
        /// </summary>
        public static Vector2 RelativeToRectTransform(this RectTransform from, RectTransform to)
        {
            var rectFrom = from.rect;
            var fromPivotDerivedOffset = new Vector2(rectFrom.width * 0.5f + rectFrom.xMin,
                rectFrom.height * 0.5f + rectFrom.yMin);

            var screenP = RectTransformUtility.WorldToScreenPoint(null, from.position);
            screenP += fromPivotDerivedOffset;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(to, screenP, null, out var localPoint);

            var rectTo = to.rect;
            var pivotDerivedOffset = new Vector2(
                rectTo.width * 0.5f + rectTo.xMin,
                rectTo.height * 0.5f + rectTo.yMin);
            
            return to.anchoredPosition + localPoint - pivotDerivedOffset;
        }
    }
}