using DataProvider;
using Player;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PlayerStatDataLister))]
    public class DataListerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Provider Creation", EditorStyles.boldLabel);

            if (GUILayout.Button("Create Stat Provider"))
                CreateProvider<StatDataProvider>("Stat Provider");
        }

        private void CreateProvider<T>(string providerName) where T : Component
        {
            PlayerStatDataLister playerStatDataLister = (PlayerStatDataLister)target;
            GameObject providerObject = new GameObject(providerName);

            Undo.RegisterCreatedObjectUndo(providerObject, $"Create {providerName}");
            providerObject.transform.SetParent(playerStatDataLister.transform, false);
            providerObject.AddComponent<T>();

            Selection.activeGameObject = providerObject;
        }
    }
}
