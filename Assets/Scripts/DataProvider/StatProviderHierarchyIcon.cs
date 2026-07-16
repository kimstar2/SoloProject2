using System;
using UnityEditor;
using UnityEngine;

namespace DataProvider
{
    [InitializeOnLoad]
    public static class StatProviderHierarchyIcon
    {
        static readonly Texture Icon =
            EditorGUIUtility.IconContent("d_Favorite").image;

        [Obsolete("Obsolete")]
        static StatProviderHierarchyIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawIcon;
        }

        [Obsolete("Obsolete")]
        static void DrawIcon(int instanceId, Rect rect)
        {
            if (EditorUtility.InstanceIDToObject(instanceId) is not GameObject gameObject)
                return;

            if (gameObject.GetComponent<StatDataProvider>() == null)
                return;

            var iconRect = new Rect(rect.xMax - 18f, rect.y + 1f, 16f, 16f);
            GUI.DrawTexture(iconRect, Icon);
        }
    }
}