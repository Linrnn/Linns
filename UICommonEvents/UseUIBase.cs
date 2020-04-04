namespace Linns.UICommonEvents
{
    using UnityEngine;
    using ListTransform = System.Collections.Generic.List<UnityEngine.Transform>;

    public abstract class UseUIBase : MonoBehaviour
    {
        /// <summary>
        /// 被操作的对象
        /// </summary>
        [Tooltip("被操作的对象")]
        public UITarget uiTarget = UITarget.Self;
        /// <summary>
        /// 被拖拽的对象
        /// </summary>
        [Tooltip("被操作的对象")]
        public Transform target;
        /// <summary>
        /// 被拖拽的对象组
        /// </summary>
        [Tooltip("被操作的对象组")]
        public ListTransform targets = new ListTransform();

        protected void OnEnable()
        {
        }
        protected void OnDestroy()
        {
            targets.Clear();
        }
    }
}