namespace Linns.Delays
{
    using UnityEditor;

    [CustomEditor(typeof(LoadScene))]
    public class LoadSceneEditor : DelayEditor
    {
        protected SerializedProperty m_parameterMode;
        protected SerializedProperty m_sceneIndex;
        protected SerializedProperty m_sceneName;
        protected SerializedProperty m_loadSceneMode;
        protected SerializedProperty m_isAsync;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_parameterMode, "parameterMode");
            FindProperty(out m_sceneIndex, "sceneIndex");
            FindProperty(out m_sceneName, "sceneName");
            FindProperty(out m_loadSceneMode, "loadSceneMode");
            FindProperty(out m_isAsync, "isAsync");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.PropertyField(m_parameterMode);
            switch (m_parameterMode.enumValueIndex)
            {
                case 0: EditorGUILayout.PropertyField(m_sceneIndex); break;
                case 1: EditorGUILayout.PropertyField(m_sceneName); break;
            }
            EditorGUILayout.PropertyField(m_loadSceneMode);
            EditorGUILayout.PropertyField(m_isAsync);
        }
    }
}