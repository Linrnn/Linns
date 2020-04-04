namespace Linns.Extensions
{
    using UnityEngine;

    public static class ExtensionComponent
    {
        public static Transform Find(this Component input, string name)
        {
            return input.transform.Find(name);
        }
        public static Transform Find(this Component input, out Transform output, string name)
        {
            return output = input.transform.Find(name);
        }
        public static GameObject Find(this Component input, out GameObject output, string name)
        {
            return output = input.transform.Find(name).gameObject;
        }

        public static T AddComponent<T>(this Component input, out T output) where T : Component
        {
            return output = input.gameObject.AddComponent<T>();
        }

        public static T GetComponentInParent<T>(this Component input, out T output)
        {
            return output = input.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this Component input, out T output)
        {
            return output = input.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this Component input, out T output)
        {
            return output = input.GetComponentInChildren<T>();
        }
        public static T[] GetComponentsInParent<T>(this Component input, out T[] output)
        {
            return output = input.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this Component input, out T[] output)
        {
            return output = input.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this Component input, out T[] output)
        {
            return output = input.GetComponentsInChildren<T>();
        }
    }
}