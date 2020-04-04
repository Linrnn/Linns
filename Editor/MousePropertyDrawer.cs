namespace Linns
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(Mouse))]
    public class MousePropertyDrawer : PropertyDrawerBase
    {
        protected static readonly string[] propertyPaths = new string[] { "left", "right", "middle" };

        protected override void ForOnGUI(UnityEngine.Rect position, SerializedProperty property)
        {
            LinnsLayout.DrawToggles(position, propertyPaths, property, 3);
        }
    }
}