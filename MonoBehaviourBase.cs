namespace Linns
{
    using UnityEngine;

    public abstract class ScriptBase : MonoBehaviour
    {
        public static T GetInstance<T>() where T : SingleMonoBase<T>
        {
            return SingleMonoBase<T>.Instance;
        }
        public static T GetInstance<T>(out T output) where T : SingleMonoBase<T>
        {
            return output = SingleMonoBase<T>.Instance;
        }

        public Transform Find(string name)
        {
            return transform.Find(name);
        }
        public Transform Find(out Transform output, string name)
        {
            return output = transform.Find(name);
        }
        public GameObject Find(out GameObject output, string name)
        {
            return output = transform.Find(name).gameObject;
        }

        public T AddComponent<T>(out T output) where T : Component
        {
            return output = gameObject.AddComponent<T>();
        }

        public T GetComponentInParent<T>(out T output)
        {
            return output = GetComponentInParent<T>();
        }
        public T GetComponent<T>(out T output)
        {
            return output = GetComponent<T>();
        }
        public T GetComponentInChildren<T>(out T output)
        {
            return output = GetComponentInChildren<T>();
        }

        public T[] GetComponentsInParent<T>(out T[] output)
        {
            return output = GetComponentsInParent<T>();
        }
        public T[] GetComponents<T>(out T[] output)
        {
            return output = GetComponents<T>();
        }
        public T[] GetComponentsInChildren<T>(out T[] output)
        {
            return output = GetComponentsInChildren<T>();
        }
    }

    public abstract class SingleMonoBase<T> : MonoBehaviour where T : SingleMonoBase<T>
    {
        public static T Instance
        {
            get;
            protected set;
        }

        protected void Awake()
        {
            Instance = this as T;
        }
    }
}