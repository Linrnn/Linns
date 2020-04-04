namespace Linns.UICommonEvents
{
    using UnityEngine;
    using IEnumerator = System.Collections.IEnumerator;
    using ListTransform = System.Collections.Generic.List<UnityEngine.Transform>;
    using PointerEventData = UnityEngine.EventSystems.PointerEventData;

    /// <summary>
    /// UI拖拽结构体 注意：此脚本里的各函数只能操作UI组件
    /// </summary>
    [System.Serializable]
    public struct UIDrag : System.IDisposable
    {
        /// <summary>
        /// ForOnBeginDrag()函数是否被运行
        /// </summary>
        private bool m_isRunForOnBeginDrag;
        /// <summary>
        /// 开始被操作时鼠标的屏幕世界坐标
        /// </summary>
        private Vector3 m_onBeginPos;
        /// <summary>
        /// 开始被操作时的对象的世界坐标
        /// </summary>
        private Vector3 m_onBeginWorldPos;
        /// <summary>
        /// 开始被操作时的对象的世界坐标-开始被操作鼠标的屏幕世界坐标
        /// </summary>
        private Vector3 m_onBeginAndOnWorldDistance;
        /// <summary>
        /// 初始位置
        /// </summary>
        private Vector3 m_startPos;
        /// <summary>
        /// 初始位置组
        /// </summary>
        private Vector3[] m_startPoss;
        /// <summary>
        /// UI拖拽实现类
        /// </summary>
        private UseUIDarg m_useUIDarg;
        /// <summary>
        /// 用于协程
        /// </summary>
        private Coroutine m_coroutine;
        private DirType m_dirType;
        private Operation m_operation;

        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
		private static Vector3 ScreenToWorldPos(PointerEventData eventData)
        {
            return ScreenViewporWorld.ScreenToWorldPos(eventData, eventData.pointerDrag.transform as RectTransform, eventData.pressEventCamera);
        }
        /// <summary>
        /// （鼠标选中物体的世界坐标-鼠标的世界坐标）* 移动比例
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="pos"></param>
        /// <param name="movingRatio"></param>
        /// <returns></returns>
		private static Vector3 Distance(PointerEventData eventData, in Vector3 pos, in float movingRatio)
        {
            return (ScreenToWorldPos(eventData) - pos) * movingRatio;
        }
        /// <summary>
        /// ForOnDarg()中的功能
        /// </summary>
        /// <param name="type"></param>
        /// <param name="trans"></param>
        /// <param name="pos"></param>
        /// <param name="other"></param>
        /// <param name="startPos"></param>
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
                    if (trans.parent) pos = trans.parent.InverseTransformPoint(pos);
                    else break;
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
        /// 为OnEndDrag()拖拽对象组做一些数据上的准备
        /// </summary>
        private void ToOnEndDrag()
        {
            m_isRunForOnBeginDrag = false;
            m_onBeginPos = new Vector3();
            m_onBeginWorldPos = new Vector3();
            m_onBeginAndOnWorldDistance = new Vector3();
            m_startPos = new Vector3();
            m_startPoss = null;
        }

        /// <summary>
        /// 必须在OnPointerDown()调用此函数（只须调用一次）
        /// </summary>
        /// <param name="useUIDarg">this</param>
        public void ForOnPointerDown(UseUIDarg useUIDarg)
        {
            if (this.m_useUIDarg == null) { this.m_useUIDarg = useUIDarg; }
            if (m_coroutine != null && useUIDarg.mouseButton.mouseButtonStay && useUIDarg.isStop)
            {
                useUIDarg.StopCoroutine(m_coroutine);
            }
        }

        /// <summary>
        /// 必须在OnBeginDrag()调用此函数（只须调用一次）   为ForOnDrag()拖拽对象组做一些数据上的准备
        /// </summary>
        /// <param name="useUIDarg">this</param>
        /// <param name="eventData">eventData</param>
        public void ForOnBeginDrag(UseUIDarg useUIDarg, PointerEventData eventData)
        {
            if (m_coroutine != null && useUIDarg.mouseButton.mouseButtonStay)
            {
                useUIDarg.StopCoroutine(m_coroutine);
            }
            switch (useUIDarg.uiTarget)
            {
                case UITarget.Self: ForOnBeginDrag(eventData, useUIDarg.other); break;
                case UITarget.Other: ForOnBeginDrag(eventData, useUIDarg.target, useUIDarg.other); break;
                case UITarget.Others: ForOnBeginDrag(eventData, useUIDarg.targets, useUIDarg.other); break;
            }
        }
        /// <summary>
        /// 必须在OnBeginDrag()调用此函数（只须调用一次）   为ForOnDrag()拖拽自身做一些数据上的准备
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="mouseButton">鼠标按键类</param>
        public void ForOnBeginDrag(PointerEventData eventData, Transform other)
        {
            if (!m_useUIDarg.mouseButton.mouseButtonStay) { return; }
            m_isRunForOnBeginDrag = true;
            m_onBeginPos = ScreenToWorldPos(eventData);
            m_onBeginWorldPos = eventData.pointerDrag.transform.position;
            m_onBeginAndOnWorldDistance = m_onBeginWorldPos - m_onBeginPos;
        }
        /// <summary>
        /// 必须在OnBeginDrag()调用此函数（只须调用一次）   为ForOnDrag()拖拽对象做一些数据上的准备
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="target">被对拽的对象</param>
        /// <param name="mouseButton">鼠标按键类</param>
		public void ForOnBeginDrag(PointerEventData eventData, Transform target, Transform other)
        {
            if (!target) { return; }
            ForOnBeginDrag(eventData, other);
            m_startPos = target.position;
        }
        /// <summary>
        /// 必须在OnBeginDrag()调用此函数（只须调用一次）   为ForOnDrag()拖拽对象组做一些数据上的准备
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="targets">被对拽的对象组</param>
        /// <param name="mouseButton">鼠标按键类</param>
		public void ForOnBeginDrag(PointerEventData eventData, ListTransform targets, Transform other)
        {
            ForOnBeginDrag(eventData, other);
            int count = targets.Count;
            m_startPoss = new Vector3[count];
            for (int i = 0; i < count; ++i)
            {
                if (targets[i]) { m_startPoss[i] = targets[i].position; }
            }
        }

        /// <summary>
        /// 在OnDrag()调用此函数  实现对象的拖拽
        /// </summary>
        /// <param name="useUIDarg">this</param>
        /// <param name="eventData">eventData</param>
        public void ForOnDrag(UseUIDarg useUIDarg, PointerEventData eventData)
        {
            switch (useUIDarg.uiTarget)
            {
                case UITarget.Self: ForOnDrag(eventData, useUIDarg.dirType, useUIDarg.isCenterFollowMouse, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.other); break;
                case UITarget.Other: ForOnDrag(eventData, useUIDarg.dirType, useUIDarg.movingRatio, useUIDarg.target, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.other); break;
                case UITarget.Others: ForOnDrag(eventData, useUIDarg.dirType, useUIDarg.movingRatio, useUIDarg.targets, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.other); break;
            }
        }
        /// <summary>
        /// 在OnDrag()调用此函数  实现自身的拖拽
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="isCenterFollowsMouse">UI中心点是否跟随鼠标</param>
        /// <param name="isLimitedDir">是否限制拖拽方向</param>
        /// <param name="dir">被限制拖拽的方向</param>
        public void ForOnDrag(PointerEventData eventData, VectorType vectorType, bool isCenterFollowsMouse, bool isLimitedDir, Vector2 dir, Transform other)
        {
            if (!m_isRunForOnBeginDrag) { return; }
            if (!m_useUIDarg.mouseButton.mouseButtonStay) { return; }
            Vector3 pos = ScreenToWorldPos(eventData);
            if (!isCenterFollowsMouse) { pos += m_onBeginAndOnWorldDistance; }
            if (isLimitedDir) { pos = m_onBeginWorldPos + Vector3.Project(pos - m_onBeginWorldPos, dir); }
            PosType(in vectorType, eventData.pointerDrag.transform, ref pos, other);
        }
        /// <summary>
        /// 在OnDrag()调用此函数  实现对象的拖拽
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="movingRatio">拖拽的移动比例</param>
        /// <param name="target">被对拽的对象</param>
        /// <param name="startPos">被对拽的对象开始的世界坐标</param>
        /// <param name="isLimitedDir">是否限制拖拽的方向</param>
        /// <param name="dir">被限制拖拽的方向</param>
        public void ForOnDrag(PointerEventData eventData, VectorType vectorType, float movingRatio, Transform target, bool isLimitedDir, Vector2 dir, Transform other)
        {
            if (!m_isRunForOnBeginDrag) { return; }
            if (!m_useUIDarg.mouseButton.mouseButtonStay) { return; }
            Vector3 pos = Distance(eventData, in m_onBeginPos, in movingRatio);
            if (isLimitedDir) { pos += Vector3.Project(pos, dir) - pos; }
            pos += m_startPos;
            PosType(in vectorType, target, ref pos, other);
        }
        /// <summary>
        /// 在OnDrag()调用此函数  实现对象的拖拽
        /// </summary>
        /// <param name="eventData">PointerEventData的类型的事件参数</param>
        /// <param name="movingRatio">拖拽的移动比例</param>
        /// <param name="targets">被对拽的对象组</param>
        /// <param name="startPoss">被对拽的对象组开始的世界坐标</param>
        /// <param name="isLimitedDir">是否限制拖拽的方向</param>
        /// <param name="dir">被限制拖拽的方向</param>
        public void ForOnDrag(PointerEventData eventData, VectorType vectorType, float movingRatio, ListTransform targets, bool isLimitedDir, Vector2 dir, Transform other)
        {
            if (!m_isRunForOnBeginDrag) { return; }
            if (!m_useUIDarg.mouseButton.mouseButtonStay) { return; }
            Vector3 distance = Distance(eventData, in m_onBeginPos, in movingRatio);
            if (isLimitedDir) { distance += Vector3.Project(distance, dir) - distance; }
            for (int i = 0; i != targets.Count; ++i)
            {
                if (targets[i])
                {
                    Vector3 pos = m_startPoss[i] + distance;
                    PosType(in vectorType, targets[i], ref pos, other);
                }
            }
        }

        /// <summary>
        /// 在OnEndDrag()调用此函数  实现对象的滑动
        /// </summary>
        /// <param name="useUIDarg">this</param>
        public void ForOnEndDrag(UseUIDarg useUIDarg)
        {
            switch (useUIDarg.uiTarget)
            {
                case UITarget.Self: m_coroutine = useUIDarg.StartCoroutine(ForOnEndDrag(useUIDarg.transform, useUIDarg.dirType, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.decelerationType,useUIDarg.other, useUIDarg.percentage, useUIDarg.deltaTime, useUIDarg.start, useUIDarg.end)); break;
                case UITarget.Other: m_coroutine = useUIDarg.StartCoroutine(ForOnEndDrag(useUIDarg.target, useUIDarg.dirType, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.decelerationType, useUIDarg.other, useUIDarg.percentage, useUIDarg.deltaTime, useUIDarg.start, useUIDarg.end)); break;
                case UITarget.Others: m_coroutine = useUIDarg.StartCoroutine(ForOnEndDrag(useUIDarg.targets, useUIDarg.dirType, useUIDarg.isLimitedDir, useUIDarg.direction, useUIDarg.decelerationType, useUIDarg.other, useUIDarg.percentage, useUIDarg.deltaTime, useUIDarg.start, useUIDarg.end)); break;
            }
        }
        /// <summary>
        /// 在OnEndDrag()调用此协程  实现对象的滑动
        /// </summary>
        /// <param name="target">滑动的对象</param>
        /// <param name="vectorType">方向的类型</param>
        /// <param name="isLimitedDir">是否限制方向</param>
        /// <param name="dir">限制的方向</param>
        /// <param name="decelerationType">减速的类型</param>
        /// <param name="percentage">减少的百分比</param>
        /// <param name="time">间隔时间</param>
        /// <param name="end">最小速度</param>
        /// <returns></returns>
        public IEnumerator ForOnEndDrag(Transform target, VectorType vectorType, bool isLimitedDir, Vector2 dir, DecelerationType decelerationType, Transform other, float percentage = 0.1f, float time = 0.01f, float start = 1f, float end = 0.1f)
        {
            if (!target || !m_useUIDarg.mouseButton.mouseButtonUp) yield break;
            ToOnEndDrag();
            WaitForSeconds wait = new WaitForSeconds(time);

            Vector3 mouseDir = Mouse.direction;
            Vector3 m_dir;
            if (isLimitedDir)
            {
                float dot = Vector3.Dot(mouseDir, dir);
                if (dot > 0f) { m_dir = dir.normalized * mouseDir.magnitude; }
                else if (dot < 0f) { m_dir = -dir.normalized * mouseDir.magnitude; }
                else yield break;
            }
            else m_dir = mouseDir;

            switch (vectorType)
            {
                case VectorType.Self:
                    m_dirType += Self;
                    break;
                case VectorType.Parent:
                    if (target.parent) { m_dirType += Parent; }
                    else { yield break; }
                    break;
                case VectorType.World:
                    m_dirType += World;
                    break;
                case VectorType.Other:
                    if (other) { m_dirType += Other; }
                    else { yield break; }
                    break;
                default: yield break;
            }

            switch (decelerationType)
            {
                case DecelerationType.Linear: m_operation += Linear; break;
                case DecelerationType.Lerp: m_operation += Lerp; break;
                default: yield break;
            }

            while (start > end)
            {
                m_dirType(target, in m_dir, in start, other);
                m_operation(ref start, in percentage);
                yield return wait;
            }
            wait = null;
            Clear();
        }
        /// <summary>
        /// 在OnEndDrag()调用此协程  实现对象的滑动
        /// </summary>
        /// <param name="targets">滑动的对象组</param>
        /// <param name="vectorType">方向的类型</param>
        /// <param name="isLimitedDir">是否限制方向</param>
        /// <param name="dir">限制的方向</param>
        /// <param name="decelerationType">减速的类型</param>
        /// <param name="percentage">减少的百分比</param>
        /// <param name="time">间隔时间</param>
        /// <param name="end">最小速度</param>
        /// <returns></returns>
        public IEnumerator ForOnEndDrag(ListTransform targets, VectorType vectorType, bool isLimitedDir, Vector2 dir, DecelerationType decelerationType, Transform other, float percentage = 0.1f, float time = 0.01f, float start = 1f, float end = 0.1f)
        {
            if (!m_useUIDarg.mouseButton.mouseButtonUp) { yield break; }
            if (targets == null || !m_useUIDarg.mouseButton.mouseButtonUp) { yield break; }
            ToOnEndDrag();
            WaitForSeconds wait = new WaitForSeconds(time);

            switch (vectorType)
            {
                case VectorType.Self: m_dirType += Self; break;
                case VectorType.Parent: m_dirType += Parent; break;
                case VectorType.World: m_dirType += World; break;
                case VectorType.Other: m_dirType += Other; break;
                default: yield break;
            }

            switch (decelerationType)
            {
                case DecelerationType.Linear: m_operation += Linear; break;
                case DecelerationType.Lerp: m_operation += Lerp; break;
                default: yield break;
            }

            Vector3 mouseDir = Mouse.direction;
            Vector3 m_dir;
            if (isLimitedDir)
            {
                float dot = Vector3.Dot(mouseDir, dir);
                if (dot > 0f) { m_dir = dir.normalized * mouseDir.magnitude; }
                else if (dot < 0f) { m_dir = -dir.normalized * mouseDir.magnitude; }
                else yield break;
            }
            else m_dir = mouseDir;

            while (start > end)
            {
                foreach (Transform target in targets) { m_dirType(target, in m_dir, in start, other); }
                m_operation(ref start, in percentage);
                yield return wait;
            }
            wait = null;
            Clear();
        }

        private delegate void DirType(Transform target, in Vector3 dir, in float speed, Transform other);
        private static void Self(Transform target, in Vector3 dir, in float speed, Transform other)
        {
            if (target) { target.Translate(dir * speed); }
        }
        private static void Parent(Transform target, in Vector3 dir, in float speed, Transform other)
        {
            if (target && target.parent) { target.Translate(target.parent.TransformDirection(dir) * speed, Space.World); }
        }
        private static void World(Transform target, in Vector3 dir, in float speed, Transform other)
        {
            if (target) { target.Translate(dir * speed, Space.World); }
        }
        private static void Other(Transform target, in Vector3 dir, in float speed, Transform other)
        {
            if (target && other) { target.Translate(other.TransformDirection(dir) * speed, Space.World); }
        }

        private delegate void Operation(ref float left, in float right);
        private static void Linear(ref float left, in float right)
        {
            left -= right;
        }
        private static void Lerp(ref float left, in float right)
        {
            left *= 1f - right;
        }

        private void Clear()
        {
            m_dirType -= Self;
            m_dirType -= Parent;
            m_dirType -= World;
            m_dirType -= Other;
            m_operation -= Linear;
            m_operation -= Lerp;
            m_dirType = null;
            m_operation = null;
        }
        public void Dispose()
        {
            m_startPoss = null;
            m_useUIDarg = null;
            m_coroutine = null;
            Clear();
        }
    }
}