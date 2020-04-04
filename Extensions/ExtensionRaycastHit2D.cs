namespace Linns.Extensions
{
    using UnityEngine;

    public static class ExtensionRaycastHit2D
    {
        public static Transform Find(this RaycastHit2D input, string name)
        {
            return input.transform.Find(name);
        }
        public static Transform Find(this RaycastHit2D input, out Transform output, string name)
        {
            return output = input.transform.Find(name);
        }
        public static GameObject Find(this RaycastHit2D input, out GameObject output, string name)
        {
            return output = input.transform.Find(name).gameObject;
        }

        public static T AddComponent<T>(this RaycastHit2D input) where T : Component
        {
            return input.transform.gameObject.AddComponent<T>();
        }
        public static T AddComponent<T>(this RaycastHit2D input, out T output) where T : Component
        {
            return output = input.transform.gameObject.AddComponent<T>();
        }

        public static T GetComponentInParent<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponentInChildren<T>();
        }
        public static T GetComponentInParent<T>(this RaycastHit2D input, out T output)
        {
            return output = input.transform.GetComponentInParent<T>();
        }
        public static T GetComponent<T>(this RaycastHit2D input, out T output)
        {
            return output = input.transform.GetComponent<T>();
        }
        public static T GetComponentInChildren<T>(this RaycastHit2D input, out T output)
        {
            return output = input.transform.GetComponentInChildren<T>();
        }

        public static T[] GetComponentsInParent<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this RaycastHit2D input)
        {
            return input.transform.GetComponentsInChildren<T>();
        }
        public static T[] GetComponentsInParent<T>(this RaycastHit2D input, out T[] output)
        {
            return output = input.transform.GetComponentsInParent<T>();
        }
        public static T[] GetComponents<T>(this RaycastHit2D input, out T[] output)
        {
            return output = input.transform.GetComponents<T>();
        }
        public static T[] GetComponentsInChildren<T>(this RaycastHit2D input, out T[] output)
        {
            return output = input.transform.GetComponentsInChildren<T>();
        }
    }
}