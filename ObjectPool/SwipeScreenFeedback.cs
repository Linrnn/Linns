namespace Linns.ObjectPool
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using IEnumerator = System.Collections.IEnumerator;

    [AddComponentMenu("Linns/Object Pool/Swipe Screen Feedback")]
    public class SwipeScreenFeedback : SecurityCoroutine, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        public bool isDown;
        public bool isUp;
        public bool isBeginDrag;
        public bool isDrag;
        public Vector3 position;
        public Vector3 scale = Vector3.one;
        public new Camera camera;
        public RectTransform rectTransform;
        public float exisTime = 1f;
        public GameObject prefab;
        protected GameObjPoolBase m_gameObjPoolBase;
        protected WaitForSeconds m_waitForSeconds;

        public SwipeScreenFeedback()
        {
            resetType = ResetType.CancelPrevious;
        }

        protected void Start()
        {
            GameObjPool gameObjPool = gameObject.GetComponent<GameObjPool>() ?? gameObject.AddComponent<GameObjPool>();
            m_gameObjPoolBase.Set(prefab, transform);
        }
        protected new void OnDestroy()
        {
            base.OnDestroy();
            m_gameObjPoolBase.Dispose();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            m_gameObjPoolBase.exisTime = exisTime;
            if (!isDown) { return; }
            ShowFeedback(eventData);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isUp) { return; }
            StopAllCoroutines();
            ShowFeedback(eventData);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isBeginDrag) { return; }
            m_waitForSeconds = waitForSeconds;
            ShowFeedback(eventData);
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!isDrag) { return; }
            Run(RunCoroutine(eventData));
        }

        public void ShowFeedback(PointerEventData eventData)
        {
            Transform gameObj = m_gameObjPoolBase.PoolInstantiate().transform;
            gameObj.position = ScreenViewporWorld.ScreenToWorldPos(eventData, rectTransform, camera) + position;
            gameObj.localScale = scale;
            InvokeRunEvent();
        }
        protected override IEnumerator RunCoroutine()
        {
            yield break;
        }
        protected IEnumerator RunCoroutine(PointerEventData eventData)
        {
            ShowFeedback(eventData);
            yield return m_waitForSeconds;
        }
    }
}