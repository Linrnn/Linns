namespace Linns
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using StringBuilder = System.Text.StringBuilder;
    using Random = UnityEngine.Random;

    public static class Loop
    {
        private static readonly StringBuilder s_stringBuilder = new StringBuilder();

        public static KeyCode keyCodeDown
        {
            get
            {
                foreach (KeyCode keyCode in Internal<KeyCode>.enumArray)
                {
                    if (Input.GetKeyDown(keyCode)) { return keyCode; }
                }
                return KeyCode.None;
            }
        }
        public static KeyCode keyCodeStay
        {
            get
            {
                foreach (KeyCode keyCode in Internal<KeyCode>.enumArray)
                {
                    if (Input.GetKey(keyCode)) { return keyCode; }
                }
                return KeyCode.None;
            }
        }
        public static KeyCode keyCodeUp
        {
            get
            {
                foreach (KeyCode keyCode in Internal<KeyCode>.enumArray)
                {
                    if (Input.GetKeyUp(keyCode)) { return keyCode; }
                }
                return KeyCode.None;
            }
        }

        public static T GetRandomValue<T>(T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
        public static T GetRandomValue<T>(List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static void ForEachEnum<T>(Action<T> Each) where T : Enum
        {
            foreach (T item in Internal<T>.enumArray) { Each(item); }
        }
        public static int EnumTotalCount<T>() where T : Enum
        {
            return Internal<T>.enumArray.Length;
        }
        public static int EnumSum<T>() where T : Enum
        {
            int i = 0;
            foreach (T item in Internal<T>.enumArray) { i |= item.GetHashCode(); }
            return i;
        }

        public static void Empty<T>(T[] array) where T : class
        {
            for (int i = array.Length - 1; i != -1; --i) { array[i] = null; }
        }
        public static void Empty<T>(ref T[] array) where T : class
        {
            Empty(array);
            array = null;
        }
        public static void Empty<T>(List<T> list) where T : class
        {
            for (int i = list.Count - 1; i != -1; --i) { list[i] = null; }
        }

        public static void DisposeContainer<T>(IEnumerable<T> container) where T : IDisposable
        {
            foreach (T item in container) { item.Dispose(); }
        }
        public static void DebugLogContainer<T>(IEnumerable<T> container)
        {
            foreach (T item in container)
            {
                s_stringBuilder.Append(item);
                s_stringBuilder.Append('\n');
            }
            Debug.Log(s_stringBuilder);
            s_stringBuilder.Clear();
        }

        private static class Internal<T> where T : Enum
        {
            public static readonly Array enumArray = Enum.GetValues(typeof(T));
        }
    }
}