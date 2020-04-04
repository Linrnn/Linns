namespace Linns
{
    using System;
    using UnityEngine;
    using UnityEvent = UnityEngine.Events.UnityEvent;

    /// <summary>
    /// 多次点击类
    /// </summary>
    [AddComponentMenu("Linns/Multiple Click")]
    public class MultipleClick : MonoBehaviour
    {
        public MultipleClickBase multipleClickBase;

        protected MultipleClick()
        {
            multipleClickBase.isEnable = true;
            multipleClickBase.m_times = 2;
            multipleClickBase.m_time = 1f;
            multipleClickBase.baseEvent = new UnityEvent();
        }
        protected void OnDestroy()
        {
            multipleClickBase.Dispose();
        }
        public void Run()
        {
            multipleClickBase.Run();
        }
    }

    [Serializable]
    public struct MultipleClickBase : IDisposable
    {
        [Tooltip("是否启用")] public bool isEnable;
        [Tooltip("点击的次数")] public int m_times;
        [Tooltip("点击的总时间")] public float m_time;
        [Tooltip("会发生的事件")] public UnityEvent baseEvent;

        public DateTime oldTime
        {
            get;
            private set;
        }
        public int times
        {
            get;
            private set;
        }
        public float time
        {
            get;
            private set;
        }
        public float intervalTime
        {
            get { return (float)(DateTime.Now - oldTime).TotalSeconds; }
        }

        public void Run()
        {
            if (!isEnable) { return; }

            AddDate();
            if (time < m_time)
            {
                if (times < m_times) { return; }
                else if (times == m_times) { baseEvent.Invoke(); }
            }
            ResetDate();
        }
        public void AddDate()
        {
            ++times;
            time += intervalTime;
            oldTime = DateTime.Now;
        }
        public void ResetDate()
        {
            times = 1;
            time = 0f;
        }
        public void Dispose()
        {
            if (baseEvent != null) { baseEvent.RemoveAllListeners(); }
        }
    }
}