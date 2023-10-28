using JetBrains.Annotations;
using UnityEngine;

namespace Lunha.DevKit.Utilities
{
    [UsedImplicitly]
    public static class ColorUtility
    {
        public static string ToHtmlStringRgba(Color color)
        {
            return $"#{UnityEngine.ColorUtility.ToHtmlStringRGBA(color)}";
        }
    }
}