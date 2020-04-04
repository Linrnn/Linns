namespace Linns
{
    using System;
    using UnityEngine;
    using UnityEditor;

    public abstract class EnumePropertyDrawer<T> : FoldoutPropertyDrawer<EnumePropertyDrawer<T>> where T : Enum
    {
        protected static readonly string[] s_array = Enum.GetNames(typeof(T));
        protected int m_intValue;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            m_intValue = property.intValue;
            Rect rect = position;
            rect.height = Data.PropertySize;
            property.intValue = (int)(ValueType)(T)EditorGUI.EnumFlagsField(rect, Data.NullString, (T)(ValueType)property.intValue);

            if (property.intValue == 0) { EditorGUI.LabelField(rect, property.displayName); }
            else if (foldout = EditorGUI.Foldout(rect, foldout, property.displayName, true))
            {
                ++EditorGUI.indentLevel;
                ForOnGUI(position, property);
                --EditorGUI.indentLevel;
            }
        }
        protected int Count()
        {
            int count = 0;
            for (int i = s_array.Length, j = m_intValue; i != 0; --i, j >>= 1)
            {
                if ((j & 1) == 1) { ++count; }
            }
            return count;
        }
    }

    public abstract class EnumeHorizontalPropertyDrawer<T> : EnumePropertyDrawer<T> where T : Enum
    {
        protected override void ForOnGUI(Rect position, SerializedProperty property)
        {
            if (m_intValue == 0) { return; }

            position.y += Data.PropertySize;
            position.height = Data.PropertySize;
            position.width /= EqualRatio() ? Count() : Loop.EnumTotalCount<T>();

            for (int i = 0, j = property.intValue; i != s_array.Length; ++i, j >>= 1)
            {
                if ((j & 1) == 1) { EditorGUI.LabelField(position, s_array[i]); position.x += position.width; }
            }
        }
        protected override int GetPropertyHeight()
        {
            return m_intValue != 0 && foldout ? 1 : 0;
        }
        protected virtual bool EqualRatio()
        {
            return true;
        }
    }

    public abstract class EnumeVrticalPropertyDrawer<T> : EnumePropertyDrawer<T> where T : Enum
    {
        protected override void ForOnGUI(Rect position, SerializedProperty property)
        {
            position.height = Data.PropertySize;

            for (int i = 0, j = property.intValue; i != s_array.Length; ++i, j >>= 1)
            {
                if ((j & 1) == 1) { position.y += position.height; EditorGUI.LabelField(position, s_array[i]); }
            }
        }
        protected override int GetPropertyHeight()
        {
            return Count();
        }
    }
}