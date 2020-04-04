namespace Linns
{
    using UnityEngine;
    using UnityEvent = UnityEngine.Events.UnityEvent;
    using SetObjectStatus = Delays.SetObjectStatus;

    [AddComponentMenu("Linns/Mono Behaviour Run")]
    public sealed class MonoBehaviourRun : MonoBehaviour
    {
        public UnityEvent awakeEvent = new UnityEvent();
        public UnityEvent onEnableEvent = new UnityEvent();
        public UnityEvent startEvent = new UnityEvent();
        public UnityEvent onDisableEvent = new UnityEvent();
        public UnityEvent onDestroyEvent = new UnityEvent();
        public UnityEvent updateEvent = new UnityEvent();
        public UnityEvent lateUpdateEvent = new UnityEvent();
        public UnityEvent fixedUpdateEvent = new UnityEvent();

        public void Print(string message)
        {
            print(message);
        }
        public void ComponentEnable(Component component)
        {
            SetObjectStatus.SetComponentEnable(component, true);
        }
        public void ComponentDisable(Component component)
        {
            SetObjectStatus.SetComponentEnable(component, false);
        }
        public void ComponentAble(Component component)
        {
            SetObjectStatus.SetComponentEnable(component);
        }
        public void GameObjectIsActive(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        public void GameObjectNoActive(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        public void GameObjectActive(GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
        public void ObjectDestroy(Object obj)
        {
            Destroy(obj);
        }

        private void Awake()
        {
            awakeEvent.Invoke();
        }
        private void OnEnable()
        {
            onEnableEvent.Invoke();
        }
        private void Start()
        {
            startEvent.Invoke();
        }
        private void FixedUpdate()
        {
            fixedUpdateEvent.Invoke();
        }
        private void Update()
        {
            updateEvent.Invoke();
        }
        private void LateUpdate()
        {
            lateUpdateEvent.Invoke();
        }
        private void OnDisable()
        {
            onDisableEvent.Invoke();
        }
        private void OnDestroy()
        {
            onDestroyEvent.Invoke();
        }
    }
}