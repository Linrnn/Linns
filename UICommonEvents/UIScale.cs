namespace Linns.UICommonEvents
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// UI缩放结构体 注意：此脚本里的各函数只能操作UI组件
    /// </summary>
    [Serializable]
    public struct UIScale : IDisposable
    {
        private RectTransform target;
        private List<RectTransform> targets;

        /// <summary>
        /// 在OnScroll()调用此函数  实现UI的缩放
        /// </summary>
        /// <param name="target">被改变游戏对象的rectTransform</param>
        /// <param name="delta">总改变量</param>
        /// <param name="scrollDeltaY">滚轮的改变量</param>
        /// <param name="deltaScale">宽高的改变量</param>
        public static void ForOnScroll(RectTransform target, float delta, Vector2 deltaScale)
        {
            if (target) { target.sizeDelta += delta * deltaScale; }
        }
        /// <summary>
        /// 在OnScroll()调用此函数  实现UI的缩放
        /// </summary>
        /// <param name="targets">被改变游戏对象的rectTransform数组</param>
        /// <param name="delta">总改变量</param>
        /// <param name="scrollDeltaY">滚轮的改变量</param>
        /// <param name="deltaScale">宽高的改变量</param>
        public static void ForOnScroll(List<RectTransform> targets, float delta, Vector2 deltaScale)
        {
            Vector2 v2 = delta * deltaScale;

            foreach (RectTransform target in targets)
            {
                if (target) { target.sizeDelta += v2; }
            }
        }
        /// <summary>
        /// 在OnScroll()调用此函数  实现UI的缩放
        /// </summary>
        /// <param name="target">被改变游戏对象的transform</param>
        /// <param name="delta">总改变量</param>
        /// <param name="scrollDeltaY">滚轮的改变量</param>
        /// <param name="deltaScale">xyz的改变量</param>
        public static void ForOnScroll(Transform target, float delta, Vector3 deltaScale)
        {
            if (target) { target.localScale += delta * deltaScale; }
        }
        /// <summary>
        /// 在OnScroll()调用此函数  实现UI的缩放
        /// </summary>
        /// <param name="targets">被改变游戏对象的transform数组</param>
        /// <param name="delta">总改变量</param>
        /// <param name="scrollDeltaY">滚轮的改变量</param>
        /// <param name="deltaScale">xyz的改变量</param>
        public static void ForOnScroll(List<Transform> targets, float delta, Vector3 deltaScale)
        {
            Vector3 v3 = delta * deltaScale;

            foreach (Transform target in targets)
            {
                if (target) { target.localScale += v3; }
            }
        }

        /// <summary>
        /// 必须在OnPointerEnter()调用此函数  为UI的缩放做数据准备
        /// </summary>
        /// <param name="useUIScale">this</param>
        public void ForOnPointerEnter(UseUIScale useUIScale)
        {
            switch (useUIScale.uiTarget)
            {
                case UITarget.Self: target = useUIScale.GetComponent<RectTransform>(); break;
                case UITarget.Other: target = useUIScale.target.GetComponent<RectTransform>(); break;
                case UITarget.Others: Linns.Convert.ConvertList(useUIScale.targets, targets); break;
            }
        }
        /// <summary>
        /// 可以在OnPointerExit()调用此函数
        /// </summary>
        public void ForOnPointerExit()
        {
            target = null;
            targets = null;
        }
        /// <summary>
        /// 在OnScroll()调用此函数  实现UI的缩放
        /// </summary>
        /// <param name="useUIScale">this</param>
        /// <param name="eventData">eventData</param>
        public void ForOnScroll(UseUIScale useUIScale, UnityEngine.EventSystems.PointerEventData eventData)
        {
            switch (useUIScale.uiTarget)
            {
                case UITarget.Self:
                    switch (useUIScale.transformType)
                    {
                        case TransformType.Transform:
                            ForOnScroll(useUIScale.transform, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfXYZ);
                            break;
                        case TransformType.RectTransform:
                            ForOnScroll(target, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfWidthHeight);
                            break;
                    }
                    break;
                case UITarget.Other:
                    switch (useUIScale.transformType)
                    {
                        case TransformType.Transform:
                            ForOnScroll(useUIScale.target, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfXYZ);
                            break;
                        case TransformType.RectTransform:
                            ForOnScroll(target, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfWidthHeight);
                            break;
                    }
                    break;
                case UITarget.Others:
                    switch (useUIScale.transformType)
                    {
                        case TransformType.Transform:
                            ForOnScroll(useUIScale.targets, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfXYZ);
                            break;
                        case TransformType.RectTransform:
                            ForOnScroll(targets, useUIScale.delta * eventData.scrollDelta.y, useUIScale.deltaOfWidthHeight);
                            break;
                    }
                    break;
            }
        }
        public void Dispose()
        {
            target = null;
            if (targets != null) { targets.Clear(); }
        }
    }
}