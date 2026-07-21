using DataProvider;
using SO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(StatDataProvider))]
    public class StatDataProviderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();

            if (GUILayout.Button("Create and Assign Stat Data"))
                CreateAndAssignStatData();
        }

        private void CreateAndAssignStatData()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "Create Stat Data",
                "Stat data",
                "asset",
                "Choose where to save the stat data asset.");

            if (string.IsNullOrEmpty(path)) return;

            StatDataSo statData = CreateInstance<StatDataSo>();
            AssetDatabase.CreateAsset(statData, path);

            serializedObject.Update();
            serializedObject.FindProperty("statData").objectReferenceValue = statData;
            serializedObject.ApplyModifiedProperties();

            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(statData);
            Selection.activeObject = statData;
        }
    }
}
