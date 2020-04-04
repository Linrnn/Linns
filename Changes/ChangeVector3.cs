namespace Linns.Changes
{
    using UnityEngine;

    /// <summary>
    /// 三维向量变化类
    /// </summary>
    [AddComponentMenu("Linns/Changes/Change Vector3")]
    public class ChangeVector3 : ChangeBase<Vector3>
    {
        [SerializeField] private Vector3Type m_vector3Type = Vector3Type.LocalPosition;
        [SerializeField] private Transform m_target;

        public Vector3 vector3
        {
            get
            {
                switch (m_vector3Type)
                {
                    case Vector3Type.LocalPosition: return m_target.localPosition;
                    case Vector3Type.LocalEulerAngles: return m_target.localEulerAngles;
                    case Vector3Type.LocalScale: return m_target.localScale;
                    default: return new Vector3();
                }
            }
            set
            {
                switch (m_vector3Type)
                {
                    case Vector3Type.LocalPosition: m_target.localPosition = value; break;
                    case Vector3Type.LocalEulerAngles: m_target.localEulerAngles = value; break;
                    case Vector3Type.LocalScale: m_target.localScale = value; break;
                }
            }
        }
        public Vector3Type vector3Type
        {
            get { return vector3Type; }
            set { vector3Type = value; }
        }
        public Transform target
        {
            get { return m_target; }
            set { m_target = value; }
        }
        public override GameObject gameObj
        {
            get { return m_target.gameObject; }
        }

        public ChangeVector3()
        {
            m_end = Vector3.one;
        }
        public void Set(Transform target, Vector3 start, Vector3 end, DecreaseType decreaseType, float speed, Vector3Type vector3Type)
        {
            m_target = target;
            m_start = start;
            m_end = end;
            m_decreaseType = decreaseType;
            m_speed = speed;
            m_vector3Type = vector3Type;
        }
        protected override System.Collections.IEnumerator RunCoroutine()
        {
            if (speed < 0f) { yield break; }
            if (isReset) { vector3 = m_start; }

            Vector3 v3 = vector3;
            WaitForSeconds wait = waitForSeconds;
            while (Data.IsGreaterMin((v3 - m_end).magnitude))
            {
                switch (m_decreaseType)
                {
                    case DecreaseType.Linear: v3 = Vector3.MoveTowards(v3, m_end, m_speed * Time.deltaTime); break;
                    case DecreaseType.Lerp: v3 = Vector3.Lerp(v3, m_end, m_speed * Time.deltaTime); break;
                }
                vector3 = v3;
                InvokeRunEvent();
                yield return wait;
            }
            vector3 = m_end;
        }
    }

    /// <summary>
    /// 三维向量的类型
    /// </summary>
    public enum Vector3Type
    {
        /// <summary>
        /// 自身相当于父对象的坐标
        /// </summary>
        LocalPosition,
        /// <summary>
        /// 自身相当于父对象的欧拉角
        /// </summary>
        LocalEulerAngles,
        /// <summary>
        /// 自身相当于父对象的缩放
        /// </summary>
        LocalScale
    }
}