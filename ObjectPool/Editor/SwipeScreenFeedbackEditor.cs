namespace Linns.ObjectPool
{
    using UnityEditor;

    [CustomEditor(typeof(SwipeScreenFeedback))]
    public class SwipeScreenFeedbackEditor : SecurityCoroutineEditor
    {
        protected SerializedProperty m_isDown;
        protected SerializedProperty m_isUp;
        protected SerializedProperty m_isBeginDrag;
        protected SerializedProperty m_isDrag;
        protected SerializedProperty m_position;
        protected SerializedProperty m_scale;
        protected SerializedProperty m_camera;
        protected SerializedProperty m_rectTransform;
        protected SerializedProperty m_exisTime;
        protected SerializedProperty m_prefab;

        protected new void Awake()
        {
            base.Awake();
            FindProperty(out m_isDown, "isDown");
            FindProperty(out m_isUp, "isUp");
            FindProperty(out m_isBeginDrag, "isBeginDrag");
            FindProperty(out m_isDrag, "isDrag");
            FindProperty(out m_position, "position");
            FindProperty(out m_scale, "scale");
            FindProperty(out m_camera, "camera");
            FindProperty(out m_rectTransform, "rectTransform");
            FindProperty(out m_exisTime, "exisTime");
            FindProperty(out m_prefab, "prefab");
        }
        protected override void ForOnInspectorGUI()
        {
            base.ForOnInspectorGUI();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            m_isDown.boolValue = EditorGUILayout.ToggleLeft("Down", m_isDown.boolValue);
            m_isUp.boolValue = EditorGUILayout.ToggleLeft("Up", m_isUp.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            m_isBeginDrag.boolValue = EditorGUILayout.ToggleLeft("Begin Drag", m_isBeginDrag.boolValue);
            m_isDrag.boolValue = EditorGUILayout.ToggleLeft("Drag", m_isDrag.boolValue);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(m_position);
            EditorGUILayout.PropertyField(m_scale);
            EditorGUILayout.PropertyField(m_camera);
            EditorGUILayout.PropertyField(m_rectTransform);
            EditorGUILayout.PropertyField(m_exisTime);
            EditorGUILayout.PropertyField(m_prefab);
        }
    }
}