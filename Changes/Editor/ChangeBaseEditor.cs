namespace Linns.Changes
{
    using UnityEditor;

    public abstract class ChangeBaseEditor : SecurityCoroutineEditor
    {
        protected SerializedProperty m_start;
        protected SerializedProperty m_end;
        protected SerializedProperty m_decreaseType;
        protected SerializedProperty m_speed;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_start, "m_start");
            FindProperty(out m_end, "m_end");
            FindProperty(out m_decreaseType, "m_decreaseType");
            FindProperty(out m_speed, "m_speed");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_start);
            EditorGUILayout.PropertyField(m_end);
            EditorGUILayout.PropertyField(m_decreaseType);
            EditorGUILayout.PropertyField(m_speed);
        }
    }
}