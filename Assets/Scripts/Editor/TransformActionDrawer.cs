using Struct;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(TransformAction))]
    public class TransformActionDrawer : PropertyDrawer
    {
        private const float Spacing = 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect line = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(line, label, EditorStyles.boldLabel);
            NextLine(ref line);

            EditorGUI.indentLevel++;

            DrawProperty(ref line, property, "rotation", "Rotation");
            DrawAxisToggles(ref line, property, "Add Rot", "addRotationX", "addRotationY", "addRotationZ");
            DrawProperty(ref line, property, "rotDur", "Rot Dur");
            DrawProperty(ref line, property, "rotEase", "Rot Ease");

            DrawProperty(ref line, property, "scale", "Scale");
            DrawAxisToggles(ref line, property, "Add Scale", "addScaleX", "addScaleY", "addScaleZ");
            DrawProperty(ref line, property, "scaleDur", "Scale Dur");
            DrawProperty(ref line, property, "scaleEase", "Scale Ease");

            EditorGUI.indentLevel--;
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 9f * LineHeight;
        }

        private static void DrawProperty(
            ref Rect line,
            SerializedProperty root,
            string propertyName,
            string label)
        {
            EditorGUI.PropertyField(line, root.FindPropertyRelative(propertyName), new GUIContent(label));
            NextLine(ref line);
        }

        private static void DrawAxisToggles(
            ref Rect line,
            SerializedProperty root,
            string label,
            string xName,
            string yName,
            string zName)
        {
            Rect content = EditorGUI.PrefixLabel(line, new GUIContent(label));
            float width = content.width / 3f;

            DrawToggle(new Rect(content.x, content.y, width, content.height), root.FindPropertyRelative(xName), "X");
            DrawToggle(new Rect(content.x + width, content.y, width, content.height), root.FindPropertyRelative(yName), "Y");
            DrawToggle(new Rect(content.x + width * 2f, content.y, width, content.height), root.FindPropertyRelative(zName), "Z");

            NextLine(ref line);
        }

        private static void DrawToggle(Rect rect, SerializedProperty property, string axis)
        {
            const float controlWidth = 42f;
            Rect toggleRect = new Rect(
                rect.x + (rect.width - controlWidth) * 0.5f,
                rect.y,
                controlWidth,
                rect.height);

            property.boolValue = EditorGUI.ToggleLeft(toggleRect, axis, property.boolValue);
        }

        private static void NextLine(ref Rect line)
        {
            line.y += LineHeight;
        }

        private static float LineHeight => EditorGUIUtility.singleLineHeight + Spacing;
    }
}
