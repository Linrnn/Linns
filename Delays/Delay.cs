namespace Linns.Delays
{
    using UnityEngine;

    /// <summary>
    /// 延迟类
    /// </summary>
    [AddComponentMenu("Linns/Delays/Delay")]
    public class Delay : SecurityCoroutine
    {
        /// <summary>
        /// 是否延迟
        /// </summary>
        [Space, Tooltip("是否延迟")] public bool isDelay;
        /// <summary>
        /// 不自动运行的延迟时间
        /// </summary>
        [SerializeField, Tooltip("不自动运行的延迟时间")] protected float m_time;
        /// <summary>
        /// 自动运行的延迟时间
        /// </summary>
        [SerializeField, Tooltip("自动运行的延迟时间")] protected float m_timeOnAutomatic;

        public float time
        {
            get { return isAutomatic ? m_timeOnAutomatic : m_time; }
            set
            {
                if (isAutomatic) { m_timeOnAutomatic = value; }
                else { m_time = value; }     
            }
        }
        protected override System.Collections.IEnumerator RunCoroutine()
        {
            yield return isDelay ? new WaitForSeconds(isAutomatic ? m_timeOnAutomatic : m_time) : null;
            RunMethod();
        }
        protected virtual void RunMethod()
        {
            InvokeRunEvent();
        }
    } 
}