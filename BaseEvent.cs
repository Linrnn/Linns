namespace Linns
{
    using System;
    using UnityEngine;
    using UnityEngine.Events;

    [Serializable]
    public class BaseEvent: IDisposable
    {
        public bool isEnable = true;
        [SerializeField]
        protected UnityEvent m_unityEvent = new UnityEvent();

        public bool isNull
        {
            get { return m_unityEvent == null; }
        }

        public BaseEvent()
        { 
        }
        public BaseEvent(bool isEnable, UnityEvent unityEvent)
        {
            this.isEnable = isEnable;
            m_unityEvent = unityEvent;
        }
        ~BaseEvent()
        {
            Dispose();
        }
        public virtual void Set(UnityEvent unityEvent)
        {
            m_unityEvent = unityEvent;
        }
        public virtual void Set(bool isEnable, UnityEvent unityEvent)
        {
            this.isEnable = isEnable;
            Set(unityEvent);
        }
        public virtual void Copy(out UnityEvent unityEvent)
        {
            unityEvent = m_unityEvent;
        }
        public virtual void Dispose()
        {
            m_unityEvent.RemoveAllListeners();
        }

        public virtual bool Invoke()
        {
            return Invoke(isEnable);
        }
        public virtual bool Invoke(bool value)
        {
            value = !isNull && value;
            if (value) { m_unityEvent.Invoke(); }
            return value;
        }
        public virtual void AddListener(UnityAction unityAction)
        {
            if (isNull) { m_unityEvent = new UnityEvent(); }
            m_unityEvent.AddListener(unityAction);
        }
        public virtual void AddListener(UnityAction[] unityActions)
        {
            if (isNull) { m_unityEvent = new UnityEvent(); }
            foreach (UnityAction obj in unityActions) { m_unityEvent.AddListener(obj); }
        }
        public virtual void RemoveListener(UnityAction unityAction)
        {
            if (!isNull) { m_unityEvent.RemoveListener(unityAction); }
        }
        public virtual void RemoveListener(UnityAction[] unityActions)
        {
            if (!isNull)
            {
                foreach (UnityAction obj in unityActions)
                {
                    m_unityEvent.RemoveListener(obj);
                }
            }
        }
        public virtual void RemoveListener()
        {
            if (!isNull) { m_unityEvent.RemoveAllListeners(); }
        }
    }
}