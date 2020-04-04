namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI拖拽实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Darg")]
    public class UseUIDarg : UseUIBase, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Tooltip("方向的类型")]
        public VectorType dirType;
        [Tooltip("标准")]
        public Transform other;
        [Tooltip("减速的方式")]
        public DecelerationType decelerationType;
        [Tooltip("每次减少的百分比")]
        public float percentage = 0.1f;
        [Tooltip("每次减少的间隔时间")]
        public float deltaTime = 0.01f;
        [Tooltip("最大速度的百分比")]
        public float start = 1f;
        [Tooltip("最小速度的百分比")]
        public float end = 0.1f;
        [Tooltip("按下是否停止")]
        public bool isStop = true;

        [Tooltip("鼠标按键方式")]
        public Mouse mouseButton = new Mouse(true);
        [Tooltip("UI的中心是否跟随鼠标")]
        public bool isCenterFollowMouse;
        [Tooltip("拖拽的移动比例")]
        public float movingRatio = 1f;
        [Tooltip("是否限制拖拽方向")]
        public bool isLimitedDir;
        [Tooltip("被限制拖拽的方向")]
        public Vector2 direction;

        protected UIDrag m_uiDarg;

        protected new void OnDestroy()
        {
            base.OnDestroy();
            m_uiDarg.Dispose();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            m_uiDarg.ForOnPointerDown(this);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            m_uiDarg.ForOnBeginDrag(this, eventData);
        }
        public void OnDrag(PointerEventData eventData)
        {
            m_uiDarg.ForOnDrag(this, eventData);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            m_uiDarg.ForOnEndDrag(this);
        }
    }
}