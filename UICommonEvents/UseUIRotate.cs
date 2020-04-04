namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI旋转实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Rotate")]
    public class UseUIRotate : UseUIBase, IDragHandler, IEndDragHandler
    {
        [Tooltip("旋转的类型")]
        public RotateType rotateType;
        [Tooltip("围绕的点")]
        public Vector3 point;
        [Tooltip("围绕的对象")]
        public Transform pos;
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
        [Tooltip("旋转的旋转比例")]
        public Vector2 movingRatio = new Vector2(1f, 1f);
        [Tooltip("是否限制旋转方向")]
        public bool isLimitedDir;
        [Tooltip("被限制旋转的方向")]
        public Vector2 direction = new Vector2(1f, 1f);

        protected UIRotate m_uiRotate;

        protected new void OnDestroy()
        {
            base.OnDestroy();
            m_uiRotate.Dispose();
        }
        public void OnDrag(PointerEventData eventData)
        {
            m_uiRotate.ForOnDrag(this);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            m_uiRotate.ForOnEndDrag(this);
        }
    }
}