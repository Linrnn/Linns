namespace Linns.Changes
{
    using UnityEngine;

    public abstract class ChangeBase<T> : SecurityCoroutine
    {
        [Space]
        [SerializeField] protected T m_start;
        [SerializeField] protected T m_end;
        [SerializeField] protected DecreaseType m_decreaseType;
        [SerializeField] protected float m_speed = 1f;

        public virtual T start
        {
            get { return m_start; }
            set { m_start = value; }
        }
        public virtual T end
        {
            get { return m_end; }
            set { m_end = value; }
        }
        public virtual DecreaseType decreaseType
        {
            get { return m_decreaseType; }
            set { m_decreaseType = value; }
        }
        public float speed
        {
            get { return m_speed; }
            set { m_speed = value < 0f ? 0f : value; }
        }
        public abstract GameObject gameObj
        {
            get;
        }
    }

    /// <summary>
    /// 减速的类型
    /// </summary>
    public enum DecreaseType
    {
        /// <summary>
        /// 线性
        /// </summary>
        Linear,
        /// <summary>
        /// 插值
        /// </summary>
        Lerp
    }
}