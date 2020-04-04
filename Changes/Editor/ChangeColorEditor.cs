namespace Linns.Changes
{
    using UnityEditor;

    [CustomEditor(typeof(ChangeColor))]
    public class ChangeColorEditor : ChangeBaseEditor
    {
        protected SerializedProperty m_colorType;
        protected SerializedProperty m_camera;
        protected SerializedProperty m_light;
        protected SerializedProperty m_meshRenderer;
        protected SerializedProperty m_spriteRenderer;
        protected SerializedProperty m_graphic;
        protected SerializedProperty m_shadow;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_colorType, "m_colorType");
            FindProperty(out m_camera, "m_camera");
            FindProperty(out m_light, "m_light");
            FindProperty(out m_meshRenderer, "m_meshRenderer");
            FindProperty(out m_spriteRenderer, "m_spriteRenderer");
            FindProperty(out m_graphic, "m_graphic");
            FindProperty(out m_shadow, "m_shadow");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_colorType);
            switch (m_colorType.enumValueIndex)
            {
                case 0: EditorGUILayout.PropertyField(m_camera); break;
                case 1: EditorGUILayout.PropertyField(m_light); break;
                case 2: EditorGUILayout.PropertyField(m_meshRenderer); break;
                case 3: EditorGUILayout.PropertyField(m_spriteRenderer); break;
                case 4: EditorGUILayout.PropertyField(m_graphic); break;
                case 5: EditorGUILayout.PropertyField(m_shadow); break;
            }
        }
    }
}