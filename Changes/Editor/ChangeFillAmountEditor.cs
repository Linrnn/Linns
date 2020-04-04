namespace Linns.Changes
{
    using UnityEditor;

    [CustomEditor(typeof(ChangeFillAmount))]
    public class ChangeFillAmountEditor : ChangeBaseEditor
    {
        protected SerializedProperty m_target;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_target, "m_target");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_target);
        }
    }
}