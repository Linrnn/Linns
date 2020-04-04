namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI缩放实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Scale")]
    public class UseUIScale : UseUIBase, IPointerEnterHandler, IPointerExitHandler, IScrollHandler
    {
        [Tooltip("Transform的类型")]
        public TransformType transformType;
        [Tooltip("总改变量")]
        public float delta = 1f;
        [Tooltip("X，Y，Z的改变量")]
        public Vector3 deltaOfXYZ = 0.01f * Vector3.one;
        [Tooltip("宽，高的改变量")]
        public Vector2 deltaOfWidthHeight = Vector2.one;

        protected UIScale m_uiScale;

        protected new void OnDestroy()
        {
            base.OnDestroy();
            m_uiScale.Dispose();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            m_uiScale.ForOnPointerEnter(this);
        }
        public void OnScroll(PointerEventData eventData)
        {
            m_uiScale.ForOnScroll(this, eventData);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            m_uiScale.ForOnPointerExit();
        }
    }
}