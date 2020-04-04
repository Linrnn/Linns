namespace Linns.Changes
{
    using UnityEditor;

    [CustomEditor(typeof(ChangeVector3))]
    public class ChangeVector3Editor : ChangeBaseEditor
    {
        protected SerializedProperty m_vector3Type;
        protected SerializedProperty m_target;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_vector3Type, "m_vector3Type");
            FindProperty(out m_target, "m_target");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_vector3Type);
            EditorGUILayout.PropertyField(m_target);
        }
    }
}