namespace Linns.Draw
{
    using UnityEditor;

    [CustomPropertyDrawer(typeof(Parabola))]
    public class ParabolaPropertyDrawer : FoldoutPropertyDrawer<ParabolaPropertyDrawer>
    {
        protected bool m_move;

        protected override void ForOnGUI(UnityEngine.Rect position, SerializedProperty property)
        {
            position.height = Data.PropertySize;

            SerializedProperty m_function = property.FindPropertyRelative("function");
            SerializedProperty m_range = property.FindPropertyRelative("range");
            SerializedProperty m_isEquallyDistributed = property.FindPropertyRelative("isEquallyDistributed");
            SerializedProperty m_isMove = property.FindPropertyRelative("isMove");
            SerializedProperty m_moveSpeed = property.FindPropertyRelative("moveSpeed");

            SerializedProperty m_a = m_function.FindPropertyRelative("a");
            SerializedProperty m_oPos = m_function.FindPropertyRelative("oPos");
            SerializedProperty m_fun_range = m_function.FindPropertyRelative("range");
            SerializedProperty m_distributionFactor = m_function.FindPropertyRelative("distributionFactor");

            position.y += position.height; EditorGUI.PropertyField(position, m_a);
            position.y += position.height; EditorGUI.PropertyField(position, m_oPos);
            position.y += position.height; EditorGUI.PropertyField(position, m_fun_range);
            position.y += position.height; EditorGUI.PropertyField(position, m_distributionFactor);

            position.y += position.height; EditorGUI.PropertyField(position, m_range);
            position.y += position.height; position.width *= 0.5f; EditorGUI.PropertyField(position, m_isMove);
            position.x += position.width; EditorGUI.PropertyField(position, m_isEquallyDistributed); 
            if (m_move = m_isMove.boolValue)
            {
                position.x -= position.width; position.width *= 2f; position.y += position.height;
                EditorGUI.PropertyField(position, m_moveSpeed);
            }
        }
        protected override int GetPropertyHeight()
        {
            return m_move ? 7 : 6;
        }
    }
}