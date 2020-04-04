namespace Linns.ObjectPool
{
    using UnityEditor;

    [CustomEditor(typeof(GameObjPool))]
    public class GameObjPoolEditor : EditorBase
    {
        protected SerializedProperty m_isSingle;
        protected SerializedProperty m_exisTime;
        protected SerializedProperty m_prefab;
        protected SerializedProperty m_prefabs;

        protected void Awake()
        {
            FindProperty(out m_isSingle, "isSingle");
            FindProperty(out m_exisTime, "exisTime");
            FindProperty(out m_prefab, "prefab");
            FindProperty(out m_prefabs, "prefabs");
        }
        protected override void ForOnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(m_isSingle);
            EditorGUILayout.PropertyField(m_exisTime);
            EditorGUILayout.EndHorizontal();

            if (m_isSingle.boolValue) { EditorGUILayout.PropertyField(m_prefab); }
            else { EditorGUILayout.PropertyField(m_prefabs, true); }
        }
    }
}