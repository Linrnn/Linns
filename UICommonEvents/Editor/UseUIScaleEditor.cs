namespace Linns.UICommonEvents
{
    using UnityEditor;

    [CustomEditor(typeof(UseUIScale))]
    public class UseUIScaleEditor : UseUIBaseEditor
    {
        protected SerializedProperty m_transformType;
        protected SerializedProperty m_delta;
        protected SerializedProperty m_deltaOfXYZ;
        protected SerializedProperty m_deltaOfWidthHeight;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_transformType, "transformType");
            FindProperty(out m_delta, "delta");
            FindProperty(out m_deltaOfXYZ, "deltaOfXYZ");
            FindProperty(out m_deltaOfWidthHeight, "deltaOfWidthHeight");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            if (m_uiTarget.enumValueIndex > 0)
            {
                EditorGUILayout.PropertyField(m_transformType);
                EditorGUILayout.PropertyField(m_delta);
                switch (m_transformType.enumValueIndex)
                {
                    case 0: EditorGUILayout.PropertyField(m_deltaOfXYZ); break;
                    case 1: EditorGUILayout.PropertyField(m_deltaOfWidthHeight); break;
                }
                UISwitch(m_uiTarget, m_target, m_targets);
            }
        }
    }
}