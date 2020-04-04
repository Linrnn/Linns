namespace Linns.UICommonEvents
{
    using UnityEngine;
    using IEnumerator = System.Collections.IEnumerator;
    using PointerEventData = UnityEngine.EventSystems.PointerEventData;

    /// <summary>
    /// UI跟随结构体 注意：此脚本里的各函数只能操作UI组件
    /// </summary>
    [System.Serializable]
    public struct UIFollow
    {
        /// <summary>
        /// 鼠标是否在UI里
        /// </summary>
        private bool m_isStay;
        /// <summary>
        /// UI是否跟随鼠标
        /// </summary>
        private bool m_isFollow;
        /// <summary>
        /// UI跟随实现类
        /// </summary>
        private UseUIFollow m_useUIFollow;
        /// <summary>
        /// 用于停止协程
        /// </summary>
        private Coroutine m_coroutine;
        private Vector3[] m_targetStartPoss;

        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="rt"></param>
        /// <returns></returns>
        private static Vector3 ScreenToWorldPos(PointerEventData eventData, RectTransform rt)
        {
            return ScreenViewporWorld.ScreenToWorldPos(ScreenViewporWorld.mousePos, rt, eventData.pressEventCamera);
        }
        /// <summary>
        /// ForOnPointeUp()中的功能
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="pos"></param>
        private static void PosType(in VectorType type, Transform trans, ref Vector3 pos, Transform other)
        {
            switch (type)
            {
                case VectorType.Self:
                    pos = trans.InverseTransformPoint(pos);
                    pos.z = trans.InverseTransformPoint(trans.position).z;
                    trans.position = trans.TransformPoint(pos);
                    break;
                case VectorType.Parent:
                    if (trans.parent)
                        pos = trans.parent.InverseTransformPoint(pos);
                    pos.z = trans.localPosition.z;
                    trans.localPosition = pos;
                    break;
                case VectorType.World:
                    pos.z = trans.position.z;
                    trans.position = pos;
                    break;
                case VectorType.Other:
                    if (other) pos = other.InverseTransformPoint(pos);
                    else break;
                    pos.z = other.InverseTransformPoint(trans.position).z;
                    trans.position = other.TransformPoint(pos);
                    break;
            }
        }

        /// <summary>
        /// 必须在OnPointerEnter()调用此函数 处理UI的状态
        /// </summary>
        /// <param name="mouseButton"></param>
        public void ForOnPointerEnter(UseUIFollow useUIFollow)
        {
            if (this.m_useUIFollow == null) { this.m_useUIFollow = useUIFollow; }
            m_isStay = true;
        }

        /// <summary>
        /// 必须在OnPointerExit()调用此函数 处理UI的状态
        /// </summary>
        public void ForOnPointerExit()
        {
            if (!m_isFollow) { m_isStay = false; }
        }

        /// <summary>
        /// 可以在OnPointerUp()调用此函数（如果被调运，只能调用一次） 处理UI的状态
        /// </summary>
        /// <param name="isDragging"></param>
        private void ForOnPointerUp(bool dragging)
        {
            m_isFollow = m_isStay && !dragging ? !m_isFollow : false;
        }
        /// <summary>
        /// 可以在OnPointerUp()调用此函数（如果被调运，只能调用一次） 处理UI的状态
        /// </summary>
        /// <param name="isDragging"></param>
        private void ForOnPointerUp(PointerEventData eventData)
        {
            ForOnPointerUp(eventData.dragging);
        }

        /// <summary>
        /// 必须在OnDeselect()调用此函数 处理UI的状态
        /// </summary>
        /// <param name="isDeselect"></param>
        public void ForOnDeselect(bool isDeselect)
        {
            if (m_useUIFollow.mouseButton.mouseButtonDown && isDeselect) { m_isFollow = false; }
        }
        /// <summary>
        /// 必须在OnDeselect()调用此函数 处理UI的状态
        /// </summary>
        /// <param name="useUIFollow"></param>
        public void ForOnDeselect(UseUIFollow useUIFollow)
        {
            ForOnDeselect(useUIFollow.isDeselect);
        }

        /// <summary>
        /// 在OnPointerUp()调用此函数 实现UI的跟随
        /// </summary>
        /// <param name="useUIFollow">this</param>
        /// <param name="eventData">eventData</param>
        public void ForOnPointerUp(UseUIFollow useUIFollow, PointerEventData eventData)
        {
            if (m_coroutine != null && useUIFollow.uiTarget != UITarget.None)
            {
                useUIFollow.StopCoroutine(m_coroutine);
                m_coroutine = null;
            }
            switch (useUIFollow.uiTarget)
            {
                case UITarget.Self: m_coroutine = useUIFollow.StartCoroutine(ForOnPointerUp(eventData, useUIFollow.transform as RectTransform, useUIFollow.isCenterFollowMouse, useUIFollow.isLimitedDir, useUIFollow.direction, useUIFollow.dirType, useUIFollow.other)); break;
                case UITarget.Other: m_coroutine = useUIFollow.StartCoroutine(ForOnPointerUp(eventData, useUIFollow.transform as RectTransform, useUIFollow.movingRatio, useUIFollow.target, useUIFollow.isLimitedDir, useUIFollow.direction, useUIFollow.dirType, useUIFollow.other)); break;
                case UITarget.Others: m_coroutine = useUIFollow.StartCoroutine(ForOnPointerUp(eventData, useUIFollow.transform as RectTransform, useUIFollow.movingRatio, useUIFollow.targets, useUIFollow.isLimitedDir, useUIFollow.direction, useUIFollow.dirType, useUIFollow.other)); break;
            }
        }
        /// <summary>
        /// 在OnPointerUp()调用此协程 实现UI的跟随
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="rt">当前脚本的所挂载游戏对象的rectTransform</param>
        /// <param name="isCenterFollowsMouse">UI中心点是否跟随鼠标</param>
        /// <param name="isLimitedDir">是否限制跟随的方向</param>
        /// <param name="dir">被限制跟随的方向</param>
        /// <returns></returns>
        public IEnumerator ForOnPointerUp(PointerEventData eventData, RectTransform rt, bool isCenterFollowsMouse, bool isLimitedDir, Vector2 dir, VectorType dirType, Transform other)
        {
            if (!m_useUIFollow.mouseButton.mouseButtonUp) { yield break; }
            ForOnPointerUp(eventData.dragging);
            if (!m_isFollow) { yield break; }
            Vector3 startPos = new Vector3();
            Vector3 distance = new Vector3();

            if (!isCenterFollowsMouse)
            {
                distance = eventData.pointerPress.transform.position -
        ScreenViewporWorld.ScreenToWorldPos(eventData, eventData.pointerPress.GetComponent<RectTransform>(), eventData.pressEventCamera);
            }
            if (isLimitedDir) { startPos = rt.position; }

            while (m_isFollow)
            {
                Vector3 pos = ScreenToWorldPos(eventData, rt) - startPos;
                if (!isCenterFollowsMouse) { pos += distance; }
                if (isLimitedDir) { pos = startPos + Vector3.Project(pos, dir); }
                PosType(in dirType, rt, ref pos, other);
                yield return null;
            }
        }
        /// <summary>
        /// 在OnPointerUp()调用此协程 实现UI的跟随
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="rt">当前脚本的所挂载游戏对象的rectTransform</param>
        /// <param name="movingRatio">movingRatio</param>
        /// <param name="target">被改变游戏对象的Transform</param>
        /// <param name="isLimitedDir">是否限制跟随的方向</param>
        /// <param name="dir">被限制跟随的方向</param>
        /// <returns></returns>
        public IEnumerator ForOnPointerUp(PointerEventData eventData, RectTransform rt, float movingRatio, Transform target, bool isLimitedDir, Vector2 dir, VectorType dirType, Transform other)
        {
            if (!m_useUIFollow.mouseButton.mouseButtonUp || !target) { yield break; }
            ForOnPointerUp(eventData.dragging);
            if (!m_isFollow) { yield break; }

            Vector3 startWorldPos = ScreenToWorldPos(eventData, rt);
            Vector3 targetStartPos = target.position;

            while (m_isFollow)
            {
                Vector3 pos = (ScreenToWorldPos(eventData, rt) - startWorldPos) * movingRatio;
                if (isLimitedDir) pos = Vector3.Project(pos, dir);
                pos += targetStartPos;
                PosType(in dirType, target, ref pos, other);
                yield return null;
            }
        }
        /// <summary>
        /// 在OnPointerUp()调用此协程 实现UI的跟随
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="rt">当前脚本的所挂载游戏对象的rectTransform</param>
        /// <param name="movingRatio">movingRatio</param>
        /// <param name="targets">被改变游戏对象的Transform组</param>
        /// <param name="isLimitedDir">是否限制跟随的方向</param>
        /// <param name="dir">被限制跟随的方向</param>
        /// <returns></returns>
        public IEnumerator ForOnPointerUp(PointerEventData eventData, RectTransform rt, float movingRatio, System.Collections.Generic.List<Transform> targets, bool isLimitedDir, Vector2 dir, VectorType dirType, Transform other)
        {
            if (!m_useUIFollow.mouseButton.mouseButtonUp) { yield break; }
            ForOnPointerUp(eventData.dragging);
            if (!m_isFollow) { yield break; }
            if (dirType == VectorType.Other && !other) { yield break; }
            int length = targets.Count;
            Vector3 startWorldPos = ScreenToWorldPos(eventData, rt);
            if (m_targetStartPoss == null || m_targetStartPoss.Length != targets.Count) { m_targetStartPoss = new Vector3[length]; }
            for (int i = 0; i != length; ++i)
            {
                if (targets[i]) { m_targetStartPoss[i] = targets[i].position; }
            }

            while (m_isFollow)
            {
                Vector3 distance = (ScreenToWorldPos(eventData, rt) - startWorldPos) * movingRatio;
                if (isLimitedDir) { distance = Vector3.Project(distance, dir); }

                for (int i = 0; i != length; ++i)
                    if(targets[i])
                    {
                        Vector3 pos = m_targetStartPoss[i] + distance;
                        PosType(in dirType, targets[i], ref pos, other);
                    }
                yield return null;
            }
        }
    }
}