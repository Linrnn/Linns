namespace Linns.UICommonEvents
{
    using UnityEditor;

    [CustomEditor(typeof(UseUIFollow))]
    public class UseUIFollowEditor : UseUIBaseEditor
    {
        protected SerializedProperty m_dirType;
        protected SerializedProperty m_other;
        protected SerializedProperty m_mouseButton;
        protected SerializedProperty m_isCenterFollowMouse;
        protected SerializedProperty m_movingRatio;
        protected SerializedProperty m_isLimitedDir;
        protected SerializedProperty m_direction;
        protected SerializedProperty m_isDeselect;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_dirType, "dirType");
            FindProperty(out m_other, "other");
            FindProperty(out m_mouseButton, "mouseButton");
            FindProperty(out m_isCenterFollowMouse, "isCenterFollowMouse");
            FindProperty(out m_movingRatio, "movingRatio");
            FindProperty(out m_isLimitedDir, "isLimitedDir");
            FindProperty(out m_direction, "direction");
            FindProperty(out m_isDeselect, "isDeselect");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            if (m_uiTarget.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(m_dirType);
                if (m_dirType.enumValueIndex == 3) { EditorGUILayout.PropertyField(m_other); }
                EditorGUILayout.PropertyField(m_mouseButton, true);
                if (m_uiTarget.enumValueIndex > 1) { EditorGUILayout.PropertyField(m_movingRatio); }
                EditorGUILayout.PropertyField(m_isLimitedDir);
                if (m_isLimitedDir.boolValue) { EditorGUILayout.PropertyField(m_direction); }
                UISwitch(m_uiTarget, m_isCenterFollowMouse, m_target, m_targets);
                EditorGUILayout.PropertyField(m_isDeselect);
            }
        }
    }
}