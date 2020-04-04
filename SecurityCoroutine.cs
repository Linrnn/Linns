namespace Linns
{
    using UnityEngine;
    using Action = System.Action;
    using IEnumerator = System.Collections.IEnumerator;

    /// <summary>
    /// 安全协程类
    /// </summary>
    public abstract class SecurityCoroutine : MonoBehaviour
    {
        /// <summary>
        /// 重置的类型
        /// </summary>
        [Tooltip("重置的类型")] public ResetType resetType;
        /// <summary>
        /// 是否启用
        /// </summary>
        [Tooltip("是否启用")] public bool isEnable = true;
        /// <summary>
        /// 是否自动运行
        /// </summary>
        [Tooltip("是否自动运行")] public bool isAutomatic = true;
        /// <summary>
        /// 是否在不可用或非激活状态时重置
        /// </summary>
        [Tooltip("是否在不可用或非激活状态时重置")] public bool isDisableReset;
        /// <summary>
        /// 是否开始新的协程
        /// </summary>
        [Tooltip("是否开始新的协程")] public bool isReset = true;
        /// <summary>
        /// 间隔的时间
        /// </summary>
        [Tooltip("间隔的时间")] public float intervalTime;

        /// <summary>
        /// 协程运行前调用
        /// </summary>
        public event Action BeforeEvent = Data.NullMethod;
        /// <summary>
        /// 协程运行时调用
        /// </summary>
        public event Action RunEvent = Data.NullMethod;
        /// <summary>
        /// 协程结束后调用
        /// </summary>
        public event Action AfterEvent = Data.NullMethod;

        /// <summary>
        /// 协程的函数名
        /// </summary>
        public string coroutineName = "RunCoroutine";
        /// <summary>
        /// 协程的迭代器
        /// </summary>
        public IEnumerator enumerator;
        /// <summary>
        /// 协程类
        /// </summary>
        public Coroutine coroutine;

        /// <summary>
        /// 是否存在协程在运行
        /// </summary>
        public bool isExistCoroutine
        {
            get;
            protected set;
        }
        /// <summary>
        /// 返回间隔时间
        /// </summary>
        public virtual WaitForSeconds waitForSeconds
        {
            get { return intervalTime > 0f ? new WaitForSeconds(intervalTime) : null; }
        }

        public static IEnumerator DelayingSeveralFrames(int num)
        {
            if (num < 1) { yield break; }
            for (int i = 0; i != num; ++i) { yield return null; }
        }

        public SecurityCoroutine()
        {
            enumerator = RunCoroutine();
        }
        protected void OnEnable()
        {
            if (isAutomatic) { Run(); }
        }
        protected void OnDisable()
        {
            if (isDisableReset) { StopAllCoroutines(); }
        }
        protected void OnDestroy()
        {
            StopAllCoroutines();
            BeforeEvent -= Data.NullMethod;
            RunEvent -= Data.NullMethod;
            AfterEvent -= Data.NullMethod;
        }

        public virtual void Run()
        {
            Run(RunCoroutine());
        }
        public virtual void Run(IEnumerator enumerator)
        {
            if (!isEnable) { return; }
            switch (resetType)
            {
                case ResetType.Reset:
                    StopAllCoroutines();
                    StartCoroutine(NextCoroutine(enumerator));
                    break;
                case ResetType.NotReset:
                    StartCoroutine(NextCoroutine(enumerator));
                    break;
                case ResetType.CancelPrevious:
                    if (!isExistCoroutine) { StartCoroutine(NextCoroutine(enumerator)); }
                    break;
            }
        }
        protected virtual IEnumerator NextCoroutine(IEnumerator enumerator)
        {
            BeforeEvent();
            isExistCoroutine = true;
            yield return StartCoroutine(enumerator);
            isExistCoroutine = false;
            AfterEvent();
        }
        protected abstract IEnumerator RunCoroutine();

        public new virtual void StopCoroutine(string coroutineName)
        {
            base.StopCoroutine(coroutineName);
            isExistCoroutine = false;
        }
        public new virtual void StopCoroutine(IEnumerator enumerator)
        {
            base.StopCoroutine(enumerator);
            isExistCoroutine = false;
        }
        public new virtual void StopCoroutine(Coroutine coroutine)
        {
            base.StopCoroutine(coroutine);
            isExistCoroutine = false;
        }
        public new virtual void StopAllCoroutines()
        {
            base.StopAllCoroutines();
            enumerator = null;
            coroutine = null;
            isExistCoroutine = false;
        }

        public void InvokeBeforeEvent()
        {
            BeforeEvent();
        }
        public void InvokeRunEvent()
        {
            RunEvent();
        }
        public void InvokeAfterEvent()
        {
            AfterEvent();
        }
    }

    /// <summary>
    /// 重置协程的类型
    /// </summary>
    public enum ResetType
    {
        /// <summary>
        /// 重置延迟  取消所有协程 重新开始新的协程
        /// </summary>
        Reset,
        /// <summary>
        /// 不重置协程
        /// </summary>
        NotReset,
        /// <summary>
        /// 不重置协程 且 取消除第一个延迟之外的所有延迟
        /// </summary>
        CancelPrevious
    }
}