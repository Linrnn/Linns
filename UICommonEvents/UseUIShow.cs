namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI显示实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Show")]
    public class UseUIShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Tooltip("隐形的方式")]
        public StealthMethod stealthMethod = StealthMethod.Active;
        [Tooltip("角度是否一致")]
        public bool isAngleConsistent = true;
        [Tooltip("角度")]
        public Vector3 angle;
        [Tooltip("鼠标在UI中的位置关系\n鼠标在UI左下角坐标:(0,0)\n右上角坐标:(1,1)")]
        public Vector2 range;
        [Tooltip("偏差值")]
        public Vector3 deviation;
        [Tooltip("被显示的对象")]
        public RectTransform target;

        protected UIShow m_uiShow;

        protected void OnEnable()
        {
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_uiShow.ForOnPointerEnter(this, eventData);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            m_uiShow.ForOnPointerExit(this);
        }
    }
}