namespace Linns.Draw
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(VisionField2D))]
    public class VisionField2DPropertyDrawer : FoldoutPropertyDrawer<VisionField2DPropertyDrawer>
    {
        protected override void ForOnGUI(UnityEngine.Rect position, SerializedProperty property)
        {
            position.height = Data.PropertySize;

            SerializedProperty m_sector = property.FindPropertyRelative("sector");
            SerializedProperty m_visualAngle = m_sector.FindPropertyRelative("visualAngle");
            SerializedProperty m_visualRange = m_sector.FindPropertyRelative("visualRange");

            position.y += position.height; EditorGUI.PropertyField(position, m_visualAngle);
            position.y += position.height; EditorGUI.PropertyField(position, m_visualRange);
        }
        protected override int GetPropertyHeight()
        {
            return foldout ? 2 : 0;
        }
    }
}