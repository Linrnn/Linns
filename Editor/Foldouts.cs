namespace Linns
{
    using UnityEngine;
    using UnityEditor;
    using ListDataBase = System.Collections.Generic.List<DataBase>;

    public class Foldouts : ScriptableObject
    {
        protected const string c_name = "FoldoutContainer";
        protected static readonly string s_path = $"Assets/Editor/{c_name}.asset";
        protected static Foldouts s_instance;

        [SerializeField]
        protected ListDataBase m_foldoutList = new ListDataBase();

        public static void Set<T>(bool value)
        {
            if (Contains<T>())
            {
                int index = Index<T>();
                DataBase data = Instance().m_foldoutList[index];
                if (data.foldoutValue != value)
                {
                    data.foldoutValue = value;
                    Instance().m_foldoutList[index] = data;
                }
            }
            else { Add<T>(); }
        }
        public static bool Get<T>()
        {
            if (!Contains<T>()) { Add<T>(); }
            return Instance().m_foldoutList[Index<T>()].foldoutValue;
        }
        protected static Foldouts Instance()
        {
            if (!s_instance)
            {
                if (!System.IO.Directory.Exists(EditorPath())) { System.IO.Directory.CreateDirectory(EditorPath()); }
                s_instance = AssetDatabase.LoadAssetAtPath<Foldouts>(s_path);
                if (!s_instance) { AssetDatabase.CreateAsset(s_instance = CreateInstance<Foldouts>(), s_path); }
            }
            return s_instance;
        }
        protected static string EditorPath()
        {
            return $"{Application.dataPath}/Editor";
        }
        protected static void Add<T>()
        {
            Instance().m_foldoutList.Add(new DataBase() { typeName = GetStr<T>() });
        }
        protected static bool Contains<T>()
        {
            foreach (DataBase data in Instance().m_foldoutList)
            {
                if (data.typeName == GetStr<T>()) { return true; }
            }
            return false;
        }
        protected static int Index<T>()
        {
            int i = 0;
            foreach (DataBase data in Instance().m_foldoutList)
            {
                if (data.typeName == GetStr<T>()) { return i; }
                ++i;
            }
            return -1;
        }
        protected static string GetStr<T>()
        {
            return typeof(T).ToString();
        }

        [ContextMenu("Reset")]
        protected void Reset()
        {
            m_foldoutList.Clear();
        }
    }

    [System.Serializable]
    public struct DataBase
    {
        public string typeName;
        public bool foldoutValue;
    }

    [CustomEditor(typeof(Foldouts))]
    public class FoldoutsEditor : EditorBase
    {
        protected SerializedProperty m_foldoutList;

        protected void Awake()
        {
            FindProperty(out m_foldoutList, "m_foldoutList");
        }
        protected override void ForOnInspectorGUI()
        {
            LinnsLayout.Container(m_foldoutList);
        }
    }

    [CustomPropertyDrawer(typeof(DataBase))]
    public class DataBasePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.width -= (EditorGUI.indentLevel + 1) * Data.PropertySize;
            EditorGUI.LabelField(position, string.Empty, property.FindPropertyRelative("typeName").stringValue);
            position.x += position.width;
            property = property.FindPropertyRelative("foldoutValue");
            property.boolValue = EditorGUI.ToggleLeft(position, Data.NullString, property.boolValue);
        }
    }
}