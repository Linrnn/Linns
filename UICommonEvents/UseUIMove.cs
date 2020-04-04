namespace Linns.UICommonEvents
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// UI移动实现类
    /// </summary>
    [AddComponentMenu("Linns/UI Common Events/Use UI Move")]
    public class UseUIMove : UseUIBase, IScrollHandler
    {
        [Tooltip("方向的类型")]
        public VectorType dirType;
        [Tooltip("标准")]
        public Transform other;
        [Tooltip("移动的方向")]
        public Vector3 direction;
        [Tooltip("总改变量")]
        public float delta = 1f;

        public void OnScroll(PointerEventData eventData)
        {
            UIMove.ForOnScroll(this, eventData);
        }
    }
}