namespace Linns
{
    using UnityEngine;
    using UnityEditor;

    public abstract class EditorBase : Editor
    {
        protected static string[] s_scripts;
        protected MonoScript m_script;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (s_scripts == null) { s_scripts = AssetDatabase.FindAssets("t:Script"); }
            if (!m_script)
            {
                foreach (string s in s_scripts)
                {
                    m_script = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(MonoScript)) as MonoScript;
                    if (m_script.name == target.GetType().Name) { break; }
                }
            }
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", m_script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
            ForOnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
        protected abstract void ForOnInspectorGUI();
        protected void FindProperty(out SerializedProperty output, string propertyPath)
        {
            output = serializedObject.FindProperty(propertyPath);
        }
    }

    public abstract class PropertyDrawerBase : PropertyDrawer
    {     
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, null, property);
            string s = label.text;
            if (!ShowLabel()) { label.text = null; }
            Rect rect = EditorGUI.PrefixLabel(position, label);
            if (DrawLabel()) { position = rect; }
            label.text = s;
            ForOnGUI(position, property);
            EditorGUI.EndProperty();
        }
        protected abstract void ForOnGUI(Rect position, SerializedProperty property);
        protected virtual bool DrawLabel()
        {
            return true;
        }
        protected virtual bool ShowLabel()
        {
            return true;
        }
    }

    public abstract class FoldoutPropertyDrawer<T> : PropertyDrawer where T : FoldoutPropertyDrawer<T>
    {
        public static bool foldout
        {
            get { return Foldouts.Get<T>(); }
            set { Foldouts.Set<T>(value); }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string s = label.text;
            if (!ShowLabel()) { label.text = null; }
            Rect rect = position; rect.height = Data.PropertySize;
            if (foldout = EditorGUI.Foldout(rect, foldout, label, true))
            {
                ++EditorGUI.indentLevel;
                ForOnGUI(position, property);
                --EditorGUI.indentLevel;
            }
            label.text = s;
        }
        protected abstract void ForOnGUI(Rect position, SerializedProperty property);
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return Data.PropertySize * ((foldout ? GetPropertyHeight() : 0) + BaseHeight());
        }
        protected abstract int GetPropertyHeight();
        protected virtual int BaseHeight()
        {
            return 1;
        }
        protected virtual bool ShowLabel()
        {
            return true;
        }
    }
}