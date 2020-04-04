namespace Linns.Extensions
{
    using UnityEngine;

    public static class ExtensionGameObject
    {
        public static Transform Find(this GameObject input, string name)
        {
            return input.transform.Find(name);
        }
        public static Transform Find(this GameObject input, out Transform output, string name)
        {
            return output = input.transform.Find(name);
        }
        public static GameObject Find(this GameObject input, out GameObject output, string name)
        {
            return output = input.transform.Find(name).gameObject;
        }

        public static T AddComponent<T>(this GameObject input, out T output) where T : Component
        {
            return output = input.AddComponent<T>();
        }

        public static T GetComponentInParent<T>(this GameObject input, out T output)
        {
            return output = input.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this GameObject input, out T output)
        {
            return output = input.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this GameObject input, out T output)
        {
            return output = input.GetComponentInChildren<T>();
        }
        public static T[] GetComponentsInParent<T>(this GameObject input, out T[] output)
        {
            return output = input.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this GameObject input, out T[] output)
        {
            return output = input.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this GameObject input, out T[] output)
        {
            return output = input.GetComponentsInChildren<T>();
        }
    }
}