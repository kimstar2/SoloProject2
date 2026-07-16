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
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Stat Data"))
            {
                CreateStatData();
            }
        }

        private void CreateStatData()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "Create Stat Data",
                "Stat data",
                "asset",
                "스탯 데이터를 저장할 위치를 선택하세요.");

            if (string.IsNullOrEmpty(path))
                return;

            StatDataSo statData =
                CreateInstance<StatDataSo>();

            AssetDatabase.CreateAsset(statData, path);
            AssetDatabase.SaveAssets();

            serializedObject.Update();

            SerializedProperty listProperty =
                serializedObject.FindProperty("_statDataSos");

            int newIndex = listProperty.arraySize;
            listProperty.InsertArrayElementAtIndex(newIndex);

            listProperty
                .GetArrayElementAtIndex(newIndex)
                .objectReferenceValue = statData;

            serializedObject.ApplyModifiedProperties();

            EditorGUIUtility.PingObject(statData);
            Selection.activeObject = statData;
        }
    }
}