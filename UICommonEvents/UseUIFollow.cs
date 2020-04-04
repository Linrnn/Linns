namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI跟随实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Follow")]
    public class UseUIFollow : UseUIBase, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IDeselectHandler
    {
        [Tooltip("方向的类型")]
        public VectorType dirType;
        [Tooltip("标准")]
        public Transform other;
        [Tooltip("鼠标按键方式")]
        public Mouse mouseButton = new Mouse(true);
        [Tooltip("UI的中心是否跟随鼠标")]
        public bool isCenterFollowMouse;
        [Tooltip("跟随的移动比例")]
        public float movingRatio = 1f;
        [Tooltip("是否限制跟随方向")]
        public bool isLimitedDir;
        [Tooltip("被限制跟随的方向")]
        public Vector2 direction;
        [Tooltip("是否自动取消")]
        public bool isDeselect = true;

        protected UIFollow m_uiFollow;

        public void OnPointerEnter(PointerEventData eventData)
        {
            m_uiFollow.ForOnPointerEnter(this);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            m_uiFollow.ForOnPointerExit();
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            m_uiFollow.ForOnPointerUp(this, eventData);
        }
        public void OnDeselect(BaseEventData eventData)
        {
            m_uiFollow.ForOnDeselect(isDeselect);
        }
    }
}