namespace Linns.RPG
{
    using UnityEditor;

    [CustomEditor(typeof(RPGContent))]
    public class RPGContentEditor : RPGTextEditor
    {
        protected SerializedProperty m_nameIntervalTime;
        protected SerializedProperty m_nameSymbol;
        protected SerializedProperty m_nameDisplayMode;
        protected SerializedProperty m_nameUI;
        protected SerializedProperty m_nameList;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_nameIntervalTime, "nameIntervalTime");
            FindProperty(out m_nameSymbol, "nameSymbol");
            FindProperty(out m_nameDisplayMode, "nameDisplayMode");
            FindProperty(out m_nameUI, "nameUI");
            FindProperty(out m_nameList, "nameList");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            bool value = m_displayMode.enumValueIndex == 1;
            EditorGUILayout.PropertyField(m_nextIndex);
            if (value) EditorGUILayout.PropertyField(m_nameIntervalTime);
            EditorGUILayout.PropertyField(m_nameSymbol);
            EditorGUILayout.PropertyField(m_textSymbol);
            EditorGUILayout.PropertyField(m_textAsset);
            EditorGUILayout.PropertyField(m_displayMode);
            if (value) EditorGUILayout.PropertyField(m_nameDisplayMode);
            EditorGUILayout.PropertyField(m_nameUI);
            EditorGUILayout.PropertyField(m_textUI);
            EditorGUILayout.PropertyField(m_textEvents, true);
            EditorGUILayout.PropertyField(m_nameList, true);
            EditorGUILayout.PropertyField(m_textList, true);
        }
    }
}