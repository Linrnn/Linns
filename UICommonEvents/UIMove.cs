namespace Linns.UICommonEvents
{
    using UnityEngine;

    /// <summary>
    /// UI移动类 注意：此脚本里的各函数只能操作UI组件
    /// </summary>
    public static class UIMove
    {
        /// <summary>
        /// 在OnScroll()调用此函数
        /// </summary>
        /// <param name="useUIMove">this</param>
        /// <param name="eventData">eventData</param>
        public static void ForOnScroll(UseUIMove useUIMove, UnityEngine.EventSystems.PointerEventData eventData)
        {
            switch (useUIMove.uiTarget)
            {
                case UITarget.Self: ForOnScroll(useUIMove.transform, useUIMove.delta * eventData.scrollDelta.y, useUIMove.direction, useUIMove.dirType, useUIMove.other); break;
                case UITarget.Other: ForOnScroll(useUIMove.target, useUIMove.delta * eventData.scrollDelta.y, useUIMove.direction, useUIMove.dirType, useUIMove.other); break;
                case UITarget.Others: ForOnScroll(useUIMove.targets, useUIMove.delta * eventData.scrollDelta.y, useUIMove.direction, useUIMove.dirType, useUIMove.other); break;
            }
        }
        /// <summary>
        /// 在OnScroll()调用此函数
        /// </summary>
        /// <param name="target">被控制的对象</param>
        /// <param name="delta">改变量</param>
        /// <param name="dir">方向</param>
        /// <param name="dt">方向类型的枚举</param>
        public static void ForOnScroll(Transform target, float delta, Vector3 dir, VectorType dt, Transform other)
        {
            if (!target) { return; }

            switch(dt)
            {
                case VectorType.Self:
                    dir = target.TransformDirection(dir);
                    break;
                case VectorType.Parent:
                    if (!target.parent) { return; }
                    dir = target.parent.TransformDirection(dir);
                    break;
                case VectorType.World:
                    break;
                case VectorType.Other:
                    if (!other) { return; }
                    dir = other.TransformDirection(dir);
                    break;
                default: return;
            }

            target.Translate(dir * delta, Space.World);
        }
        /// <summary>
        /// 在OnScroll()调用此函数
        /// </summary>
        /// <param name="targets">被控制的对象组</param>
        /// <param name="delta">改变量</param>
        /// <param name="dir">方向</param>
        /// <param name="dt">方向类型的枚举</param>
        public static void ForOnScroll(System.Collections.Generic.List<Transform> targets, float delta, Vector3 dir, VectorType dt, Transform other)
        {
            foreach (Transform target in targets) { ForOnScroll(target, delta, dir, dt, other); }
        }
    }
}