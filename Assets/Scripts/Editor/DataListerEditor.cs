using DataProvider;
using Module;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(StatDataLister))]
    public class DataListerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Provider Creation", EditorStyles.boldLabel);

            if (GUILayout.Button("Create CardProvider"))
                CreateProvider<CardDataProvider>("CardDataProvider");

            if (GUILayout.Button("Create StatProvider"))
                CreateProvider<StatDataProvider>("StatDataProvider");
        }

        private void CreateProvider<T>(string providerName) where T : Component
        {
            StatDataLister statDataLister = (StatDataLister)target;
            GameObject providerObject = new GameObject(providerName);

            Undo.RegisterCreatedObjectUndo(providerObject, $"Create {providerName}");
            providerObject.transform.SetParent(statDataLister.transform, false);
            providerObject.AddComponent<T>();

            Selection.activeGameObject = providerObject;
        }
    }
}
