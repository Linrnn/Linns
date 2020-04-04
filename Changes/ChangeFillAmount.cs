namespace Linns.Changes
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Image填充类
    /// </summary>
    [AddComponentMenu("Linns/Changes/Change Fill Amount")]
    public class ChangeFillAmount : ChangeBase<float>
    {
        [SerializeField] private Image m_target;

        public Image target
        {
            get { return m_target; }
            set { m_target = value; }
        }
        public override float start
        {
            set { m_start = Mathf.Clamp(value, 0f, 1f); }
        }
        public override float end
        {
            set { m_end = Mathf.Clamp(value, 0f, 1f); }
        }
        public override GameObject gameObj
        {
            get { return m_target.gameObject; }
        }

        public ChangeFillAmount()
        {
            m_end = 1f;
        }
        public void Set(Image target, Image.FillMethod fillMethod, int origin, float start, float end, DecreaseType decreaseType, float speed, bool fillClockwise = true)
        {
            m_target = target;
            m_target.type = Image.Type.Filled;
            m_target.fillMethod = fillMethod;

            if (origin < 0) { origin = 0; }
            if (fillMethod < Image.FillMethod.Radial90 && origin > 1) { origin = 1; }
            else if (fillMethod > Image.FillMethod.Vertical && origin > 3) { origin = 3; }
            m_target.fillOrigin = origin;

            m_start = start;
            m_end = end;
            m_decreaseType = decreaseType;
            m_speed = speed;
            m_target.fillClockwise = fillClockwise;
        }
        protected override System.Collections.IEnumerator RunCoroutine()
        {
            if (speed < 0f) { yield break; }
            if (isReset) { m_target.fillAmount = m_start; }

            WaitForSeconds wait = waitForSeconds;
            while (Data.IsAbsGreaterMin(m_target.fillAmount - m_end))
            {
                switch (m_decreaseType)
                {
                    case DecreaseType.Linear: m_target.fillAmount = Mathf.MoveTowards(m_target.fillAmount, m_end, m_speed * Time.deltaTime); break;
                    case DecreaseType.Lerp: m_target.fillAmount = Mathf.Lerp(m_target.fillAmount, m_end, m_speed * Time.deltaTime); break;
                }
                InvokeRunEvent();
                yield return wait;
            }
            m_target.fillAmount = m_end;
        }
    }
}