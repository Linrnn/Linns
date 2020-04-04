namespace Linns.UICommonEvents
{
    using UnityEditor;

    [CustomEditor(typeof(UseUIMove))]
    public class UseUIMoveEditor : UseUIBaseEditor
    {
        protected SerializedProperty m_dirType;
        protected SerializedProperty m_other;
        protected SerializedProperty m_direction;
        protected SerializedProperty m_delta;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_dirType, "dirType");
            FindProperty(out m_other, "other");
            FindProperty(out m_direction, "direction");
            FindProperty(out m_delta, "delta");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            if (m_uiTarget.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(m_dirType);
                if (m_dirType.enumValueIndex == 3) { EditorGUILayout.PropertyField(m_other); }
                EditorGUILayout.PropertyField(m_delta);
                EditorGUILayout.PropertyField(m_direction);
                UISwitch(m_uiTarget, m_target, m_targets);
            }
        }
    }
}