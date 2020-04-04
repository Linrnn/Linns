namespace Linns.UICommonEvents
{
    using UnityEditor;

    [CustomEditor(typeof(UseUIDarg))]
    public class UseUIDargEditor : UseUIBaseEditor
    {
        protected SerializedProperty m_dirType;
        protected SerializedProperty m_other;
        protected SerializedProperty m_decelerationType;
        protected SerializedProperty m_percentage;
        protected SerializedProperty m_deltaTime;
        protected SerializedProperty m_start;
        protected SerializedProperty m_end;
        protected SerializedProperty m_isStop;
        protected SerializedProperty m_mouseButton;
        protected SerializedProperty m_isCenterFollowMouse;
        protected SerializedProperty m_movingRatio;
        protected SerializedProperty m_isLimitedDir;
        protected SerializedProperty m_direction;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_dirType, "dirType");
            FindProperty(out m_other, "other");
            FindProperty(out m_decelerationType, "decelerationType");
            FindProperty(out m_percentage, "percentage");
            FindProperty(out m_deltaTime, "deltaTime");
            FindProperty(out m_start, "start");
            FindProperty(out m_end, "end");
            FindProperty(out m_isStop, "isStop");
            FindProperty(out m_mouseButton, "mouseButton");
            FindProperty(out m_isCenterFollowMouse, "isCenterFollowMouse");
            FindProperty(out m_movingRatio, "movingRatio");
            FindProperty(out m_isLimitedDir, "isLimitedDir");
            FindProperty(out m_direction, "direction");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            if (m_uiTarget.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(m_dirType);
                if (m_dirType.enumValueIndex == 3) { EditorGUILayout.PropertyField(m_other); }
                EditorGUILayout.PropertyField(m_decelerationType);
                if (m_decelerationType.enumValueIndex > 0)
                {
                    EditorGUILayout.PropertyField(m_percentage);
                    EditorGUILayout.PropertyField(m_deltaTime);
                    EditorGUILayout.PropertyField(m_start);
                    EditorGUILayout.PropertyField(m_end);
                    EditorGUILayout.PropertyField(m_isStop);
                }
                EditorGUILayout.PropertyField(m_mouseButton, true);
                if (m_uiTarget.enumValueIndex > 1) { EditorGUILayout.PropertyField(m_movingRatio); }
                EditorGUILayout.PropertyField(m_isLimitedDir);
                if (m_isLimitedDir.boolValue) { EditorGUILayout.PropertyField(m_direction); }
                UISwitch(m_uiTarget, m_isCenterFollowMouse, m_target, m_targets);
            }
        }
    }
}