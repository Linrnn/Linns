namespace Linns
{
    using UnityEditor;

    public abstract class SecurityCoroutineEditor : EditorBase
    {
        protected SerializedProperty m_resetType;
        protected SerializedProperty m_isEnable;
        protected SerializedProperty m_isAutomatic;
        protected SerializedProperty m_isDisableReset;
        protected SerializedProperty m_isReset;
        protected SerializedProperty m_intervalTime;

        protected void Awake()
        {
            FindProperty(out m_resetType, "resetType");
            FindProperty(out m_isEnable, "isEnable");
            FindProperty(out m_isAutomatic, "isAutomatic");
            FindProperty(out m_isDisableReset, "isDisableReset");
            FindProperty(out m_isReset, "isReset");
            FindProperty(out m_intervalTime, "intervalTime");
        }
        protected override void ForOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_resetType);

            EditorGUILayout.BeginHorizontal();
            m_isEnable.boolValue = EditorGUILayout.ToggleLeft(m_isEnable.displayName, m_isEnable.boolValue);
            m_isAutomatic.boolValue = EditorGUILayout.ToggleLeft(m_isAutomatic.displayName, m_isAutomatic.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            m_isDisableReset.boolValue = EditorGUILayout.ToggleLeft(m_isDisableReset.displayName, m_isDisableReset.boolValue);
            m_isReset.boolValue = EditorGUILayout.ToggleLeft(m_isReset.displayName, m_isReset.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(m_intervalTime);
        }
    }
}