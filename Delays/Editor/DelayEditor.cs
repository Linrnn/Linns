namespace Linns.Delays
{
    using UnityEditor;

    [CustomEditor(typeof(Delay))]
    public class DelayEditor : SecurityCoroutineEditor
    {
        protected SerializedProperty m_isDelay;
        protected SerializedProperty m_time;
        protected SerializedProperty m_timeOnAutomatic;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_isDelay, "isDelay");
            FindProperty(out m_time, "m_time");
            FindProperty(out m_timeOnAutomatic, "m_timeOnAutomatic");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_isDelay);
            if (m_isDelay.boolValue) { EditorGUILayout.PropertyField(m_isAutomatic.boolValue ? m_timeOnAutomatic : m_time); }
        }
    }
}