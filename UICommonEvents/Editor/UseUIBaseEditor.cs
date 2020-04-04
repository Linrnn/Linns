namespace Linns.UICommonEvents
{
    using UnityEditor;

    public abstract class UseUIBaseEditor : EditorBase
    {
        protected SerializedProperty m_uiTarget;
        protected SerializedProperty m_target;
        protected SerializedProperty m_targets;

        protected static void UISwitch(SerializedProperty uiTarget, SerializedProperty isCenterFollowMouse, SerializedProperty target, SerializedProperty targets)
        {
            switch (uiTarget.enumValueIndex)
            {
                case 1: EditorGUILayout.PropertyField(isCenterFollowMouse); break;
                case 2: EditorGUILayout.PropertyField(target); break;
                case 3: EditorGUILayout.PropertyField(targets, true); break;
            }
        }
        protected static void UISwitch(SerializedProperty uiTarget, SerializedProperty target, SerializedProperty targets)
        {
            switch (uiTarget.enumValueIndex)
            {
                case 2: EditorGUILayout.PropertyField(target); break;
                case 3: EditorGUILayout.PropertyField(targets, true); break;
            }
        }

        protected void Awake()
        {
            FindProperty(out m_uiTarget, "uiTarget");
            FindProperty(out m_target, "target");
            FindProperty(out m_targets, "targets");
        }
        protected override void ForOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_uiTarget);
        }
    }
}