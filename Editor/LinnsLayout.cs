namespace Linns
{
    using UnityEditor;
    using Rect = UnityEngine.Rect;

    public static class LinnsLayout
    {
        public static void Container(SerializedProperty container, bool showSize = true)
        {
            int size = container.arraySize;
            if (size == 0) { return; }

            if (showSize) { EditorGUILayout.LabelField($"The size of this container is {size}."); }
            for (int i = 0; i != size; ++i) { EditorGUILayout.PropertyField(container.GetArrayElementAtIndex(i), true); }
        }

        public static void Toggle(Rect rect, string propertyPath, SerializedProperty property)
        {
            property = property.FindPropertyRelative(propertyPath);
            float x1 = property.displayName.Length * Data.PropertyWidth + rect.x;
            float x2 = rect.x + rect.width - rect.height;
            rect.width = x1 > x2 ? x2 : x1;
            EditorGUI.LabelField(rect, property.displayName);
            rect.x = rect.width;
            rect.width = rect.height;
            property.boolValue = EditorGUI.Toggle(rect, string.Empty, property.boolValue);
        }

        public static void DrawToggles(Rect position, string[] propertyPaths, SerializedProperty property, int nEqualParts)
        {
            position.width /= nEqualParts;
            foreach (string path in propertyPaths)
            {
                Toggle(position, path, property);
                position.x += position.width;
            }
        }
    }
}