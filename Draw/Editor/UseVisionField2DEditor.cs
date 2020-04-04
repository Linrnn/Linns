namespace Linns.Draw
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(UseVisionField2D))]
    public class UseVisionField2DEditor : EditorBase
    {
        protected SerializedProperty m_distributionFactor;
        protected SerializedProperty m_visionField;
        protected SerializedProperty m_layerMask;
        protected SerializedProperty m_controlMeshRenderer;

        protected void Awake()
        {
            FindProperty(out m_distributionFactor, "distributionFactor");
            FindProperty(out m_visionField, "visionField");
            FindProperty(out m_layerMask, "layerMask");
            FindProperty(out m_controlMeshRenderer, "controlMeshRenderer");
        }
        protected override void ForOnInspectorGUI()
        {
            EditorGUILayout.PropertyField(m_distributionFactor);
            EditorGUILayout.PropertyField(m_visionField, true);
            EditorGUILayout.PropertyField(m_layerMask);
            EditorGUILayout.PropertyField(m_controlMeshRenderer);
        }
    }
}