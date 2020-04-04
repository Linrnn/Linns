namespace Linns.UICommonEvents
{
    using UnityEditor;

    [CustomEditor(typeof(UseUIShow))]
    public class UseUIShowEditor : EditorBase
    {
        protected SerializedProperty m_stealthMethod;
        protected SerializedProperty m_isAngleConsistent;
        protected SerializedProperty m_angle;
        protected SerializedProperty m_range;
        protected SerializedProperty m_deviation;
        protected SerializedProperty m_target;

        protected void Awake()
        {
            FindProperty(out m_stealthMethod, "stealthMethod");
            FindProperty(out m_isAngleConsistent, "isAngleConsistent");
            FindProperty(out m_angle, "angle");
            FindProperty(out m_range, "range");
            FindProperty(out m_deviation, "deviation");
            FindProperty(out m_target, "target");
        }
        protected override void ForOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_stealthMethod);
            EditorGUILayout.PropertyField(m_isAngleConsistent);
            if (!m_isAngleConsistent.boolValue) { EditorGUILayout.PropertyField(m_angle); }
            EditorGUILayout.PropertyField(m_range);
            EditorGUILayout.PropertyField(m_deviation);
            EditorGUILayout.PropertyField(m_target);
        }
    }
}