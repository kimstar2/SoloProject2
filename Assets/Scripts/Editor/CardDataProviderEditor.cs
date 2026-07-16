using DataProvider;
using SO;
using UI.Card;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(CardDataProvider))]
    public class CardDataProviderEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("Create Card Data"))
            {
                CreateCardData();
            }
        }

        private void CreateCardData()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "Create Card Data",
                "Card data",
                "asset",
                "카드 데이터를 저장할 위치를 선택하세요.");

            if (string.IsNullOrEmpty(path))
                return;

            CardDataSo cardData =
                CreateInstance<CardDataSo>();

            AssetDatabase.CreateAsset(cardData, path);
            AssetDatabase.SaveAssets();

            serializedObject.Update();

            SerializedProperty listProperty =
                serializedObject.FindProperty("_cardDataSos");

            int newIndex = listProperty.arraySize;
            listProperty.InsertArrayElementAtIndex(newIndex);

            listProperty
                .GetArrayElementAtIndex(newIndex)
                .objectReferenceValue = cardData;

            serializedObject.ApplyModifiedProperties();

            EditorGUIUtility.PingObject(cardData);
            Selection.activeObject = cardData;
        }
    }
}