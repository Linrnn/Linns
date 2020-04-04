namespace Linns
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using UnityEngine;
    using Marshal = System.Runtime.InteropServices.Marshal;

    public static class Convert
    {
        private static readonly Stack<object> s_stack = new Stack<object>();

        public static int TypeToSize<T>() where T : struct
        {
            return Marshal.SizeOf<T>();
        }
        public static int TypeToSize(Type type)
        {
            return Marshal.SizeOf(type);
        }
        public static int TypeToSize(ValueType valueType)
        {
            return Marshal.SizeOf(valueType);
        }

        public static string[] TextToStringArray(string str, char symbol)
        {
            return str.Split(symbol);
        }
        public static string[] TextToStringArray(string str, string symbol)
        {
            return Regex.Split(str, symbol, RegexOptions.IgnoreCase);
        }
        public static string[] TextToStringArray(TextAsset file, char symbol)
        {
            return file.text.Split(symbol);
        }
        public static string[] TextToStringArray(TextAsset file, string symbol)
        {
            return Regex.Split(file.text, symbol, RegexOptions.IgnoreCase);
        }

        public static void ArrayTo<T>(T[] input, List<T> output, bool sequential = true)
        {
            output.Clear();
            if (sequential) { foreach (T item in input) { output.Add(item); } }
            else { for (int i = input.Length - 1; i != -1; --i) { output.Add(input[i]); } }
        }
        public static void ArrayTo<T>(T[] input, Queue<T> output, bool sequential = true)
        {
            output.Clear();
            if (sequential) { foreach (T item in input) { output.Enqueue(item); } }
            else { for (int i = input.Length - 1; i != -1; --i) { output.Enqueue(input[i]); } }
        }
        public static void ArrayTo<T>(T[] input, Stack<T> output, bool sequential = true)
        {
            output.Clear();
            if (sequential) { foreach (T item in input) { output.Push(item); } }
            else { for (int i = input.Length - 1; i != -1; --i) { output.Push(input[i]); } }
        }

        public static void ReverseCopy<T>(T[] input, T[] output)
        {
            int length = input.Length;
            for (int i = 0; i != length; ++i) { input[i] = output[length - i - 1]; }
        }
        public static void ReverseCopy<T>(List<T> input, List<T> output)
        {
            output.Clear();
            for (int i = input.Count - 1; i != -1; --i) { output.Add(input[i]); }
        }
        public static void ReverseCopy<T>(Queue<T> input, Queue<T> output)
        {
            output.Clear();
            foreach (T item in input) { s_stack.Push(item); }
            foreach (T item in s_stack) { output.Enqueue(item); }
            s_stack.Clear();
        }
        public static void ReverseCopy<T>(Stack<T> input, Stack<T> output)
        {
            output.Clear();
            foreach (T item in input) { output.Push(item); }
        }

        public static void ConvertArray(Transform[] input, RectTransform[] output)
        {
            for (int i = input.Length - 1; i != -1; --i) { output[i] = input[i] as RectTransform; }
        }
        public static void ConvertArray(Vector3[] input, Vector2[] output)
        {
            for (int i = input.Length - 1; i != -1; --i) { output[i] = input[i]; }
        }
        public static void ConvertArray(Vector2[] input, Vector3[] output)
        {
            for (int i = input.Length - 1; i != -1; --i) { output[i] = input[i]; }
        }

        public static void ConvertList(List<Transform> input, List<RectTransform> output)
        {
            output.Clear();
            foreach (Transform transform in input) { output.Add(transform as RectTransform); }
        }
        public static void ConvertList(List<Vector3> input, List<Vector2> output)
        {
            output.Clear();
            foreach (Vector3 vector3 in input) { output.Add(vector3); }
        }
        public static void ConvertList(List<Vector2> input, List<Vector3> output)
        {
            output.Clear();
            foreach (Vector2 vector2 in input) { output.Add(vector2); }
        }
    }
}