using DataProvider;
using Module;
using UI;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PlayerStatUI))]
    public class PlayerStatUIEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Provider Creation", EditorStyles.boldLabel);

            if (GUILayout.Button("Create Stat UI"))
                CreateProvider<StatUIProvider>("StatUIProvider");
        }

        private void CreateProvider<T>(string providerName) where T : Component
        {
            PlayerStatUI dataLister = (PlayerStatUI)target;
            GameObject providerObject = new GameObject(providerName);

            Undo.RegisterCreatedObjectUndo(providerObject, $"Create {providerName}");
            providerObject.transform.SetParent(dataLister.transform, false);
            providerObject.AddComponent<T>();

            Selection.activeGameObject = providerObject;
        }
    }
}