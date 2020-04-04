namespace Linns.Extensions
{
    using UnityEngine;

    public static class ExtensionCollision2D
    {
        public static Transform Find(this Collision2D input, string name)
        {
            return input.transform.Find(name);
        }
        public static Transform Find(this Collision2D input, out Transform output, string name)
        {
            return output = input.transform.Find(name);
        }
        public static GameObject Find(this Collision2D input, out GameObject output, string name)
        {
            return output = input.transform.Find(name).gameObject;
        }

        public static T AddComponent<T>(this Collision2D input) where T : Component
        {
            return input.gameObject.AddComponent<T>();
        }
        public static T AddComponent<T>(this Collision2D input, out T output) where T : Component
        {
            return output = input.gameObject.AddComponent<T>();
        }

        public static T GetComponentInParent<T>(this Collision2D input)
        {
            return input.gameObject.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this Collision2D input)
        {
            return input.gameObject.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this Collision2D input)
        {
            return input.gameObject.GetComponentInChildren<T>();
        }
        public static T GetComponentInParent<T>(this Collision2D input, out T output)
        {
            return output = input.gameObject.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this Collision2D input, out T output)
        {
            return output = input.gameObject.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this Collision2D input, out T output)
        {
            return output = input.gameObject.GetComponentInChildren<T>();
        }

        public static T[] GetComponentsInParent<T>(this Collision2D input)
        {
            return input.gameObject.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this Collision2D input)
        {
            return input.gameObject.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this Collision2D input)
        {
            return input.gameObject.GetComponentsInChildren<T>();
        }
        public static T[] GetComponentsInParent<T>(this Collision2D input, out T[] output)
        {
            return output = input.gameObject.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this Collision2D input, out T[] output)
        {
            return output = input.gameObject.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this Collision2D input, out T[] output)
        {
            return output = input.gameObject.GetComponentsInChildren<T>();
        }
    }
}