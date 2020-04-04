namespace Linns.Delays
{
    using UnityEditor;

    [CustomEditor(typeof(SetObjectStatus))]
    public class SetObjectStatusEditor : DelayEditor
    {
        protected SerializedProperty m_objectType;
        protected SerializedProperty m_enables;
        protected SerializedProperty m_noEnables;
        protected SerializedProperty m_counterEnables;
        protected SerializedProperty m_actives;
        protected SerializedProperty m_disables;
        protected SerializedProperty m_counters;
        protected SerializedProperty m_destroys;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_objectType, "objectType");
            FindProperty(out m_enables, "enables");
            FindProperty(out m_noEnables, "noEnables");
            FindProperty(out m_counterEnables, "counterEnables");
            FindProperty(out m_actives, "actives");
            FindProperty(out m_disables, "disables");
            FindProperty(out m_counters, "counters");
            FindProperty(out m_destroys, "destroys");
        }
        protected override void ForOnInspectorGUI()
        {
            void Component()
            {
                EditorGUILayout.PropertyField(m_enables, true);
                EditorGUILayout.PropertyField(m_noEnables, true);
                EditorGUILayout.PropertyField(m_counterEnables, true);
            }
            void GameObject()
            {
                EditorGUILayout.PropertyField(m_actives, true);
                EditorGUILayout.PropertyField(m_disables, true);
                EditorGUILayout.PropertyField(m_counters, true);
            }
            void Object()
            {
                EditorGUILayout.PropertyField(m_destroys, true);
            }

            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_objectType);
            switch (m_objectType.enumValueIndex)
            {
                case 0: Component(); break;
                case 1: GameObject(); break;
                case 2: Object(); break;
                case 3:
                    GameObject();
                    Object();
                    break;
                case 4:
                    Component();
                    Object();
                    break;
                case 5:
                    Component();
                    GameObject();
                    break;
                case 6:
                    Component();
                    GameObject();
                    Object();
                    break;
            }
        }
    }
}