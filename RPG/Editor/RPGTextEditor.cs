namespace Linns.RPG
{
    using UnityEditor;

    [CustomEditor(typeof(RPGText))]
    public class RPGTextEditor : SecurityCoroutineEditor
    {
        protected SerializedProperty m_nextIndex;
        protected SerializedProperty m_textSymbol;
        protected SerializedProperty m_displayMode;
        protected SerializedProperty m_textAsset;
        protected SerializedProperty m_textUI;
        protected SerializedProperty m_textEvents;
        protected SerializedProperty m_textList;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_nextIndex, "m_nextIndex");
            FindProperty(out m_textSymbol, "textSymbol");
            FindProperty(out m_displayMode, "displayMode");
            FindProperty(out m_textAsset, "m_textAsset");
            FindProperty(out m_textUI, "textUI");
            FindProperty(out m_textEvents, "textEvents");
            FindProperty(out m_textList, "textList");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_nextIndex);
            EditorGUILayout.PropertyField(m_textSymbol);
            EditorGUILayout.PropertyField(m_displayMode);
            EditorGUILayout.PropertyField(m_textAsset);
            EditorGUILayout.PropertyField(m_textUI);
            EditorGUILayout.PropertyField(m_textEvents, true);
            EditorGUILayout.PropertyField(m_textList, true);
        }
    }
}