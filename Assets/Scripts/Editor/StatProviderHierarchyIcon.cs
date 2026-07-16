using Enum;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public static class StatProviderHierarchyIcon
    {
        private const string IconRoot = "Assets/Sprite/2D Pixel RPG Icon Pack/Icons/";
        private const float IconSize = 20f;

        private static readonly Texture2D HealthIcon = LoadIcon("Postion/postion_red_001.png");
        private static readonly Texture2D DefenseIcon = LoadIcon("Shield/shield_001.png");
        private static readonly Texture2D DamageIcon = LoadIcon("Sword/sword_001.png");
        private static readonly Texture2D ManaIcon = LoadIcon("Postion/postion_blue_001.png");

        static StatProviderHierarchyIcon()
        {
            EditorApplication.hierarchyWindowItemOnGUI += DrawIcon;
        }

        private static void DrawIcon(int instanceId, Rect selectionRect)
        {
            if (EditorUtility.EntityIdToObject(instanceId) is not GameObject gameObject)
                return;

            DataProvider.StatDataProvider provider = gameObject.GetComponent<DataProvider.StatDataProvider>();
            if (provider == null || provider.StatData == null)
                return;

            GUIContent icon = GetIcon(provider.StatData.statType);
            if (icon.image == null)
                return;

            float nameWidth = EditorStyles.label.CalcSize(new GUIContent(gameObject.name)).x;
            Rect iconRect = new Rect(
                selectionRect.x + nameWidth + 43f,
                selectionRect.y - 1f,
                IconSize,
                IconSize);

            GUI.Label(iconRect, icon);
        }

        private static Texture2D LoadIcon(string fileName)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(IconRoot + fileName);
        }

        private static GUIContent GetIcon(StatType statType)
        {
            return statType switch
            {
                StatType.Health => new GUIContent(HealthIcon, "Health"),
                StatType.Defense => new GUIContent(DefenseIcon, "Defense"),
                StatType.Damage => new GUIContent(DamageIcon, "Damage"),
                StatType.Mana => new GUIContent(ManaIcon, "Mana"),
                _ => GUIContent.none
            };
        }
    }
}
