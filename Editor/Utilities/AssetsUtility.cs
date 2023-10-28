using System.Linq;
using JetBrains.Annotations;
using UnityEditor;

namespace Lunha.DevKit.Editor.Utilities
{
    [UsedImplicitly]
    public static class AssetsUtility
    {
        public static string FindScenePath(string sceneName)
        {
            var guids = AssetDatabase.FindAssets("t:scene " + sceneName, null);
            return guids.Any() ? AssetDatabase.GUIDToAssetPath(guids[0]) : string.Empty;
        }
    }
}