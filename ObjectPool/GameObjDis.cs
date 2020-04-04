namespace Linns.ObjectPool
{
    using UnityEngine;

    [DisallowMultipleComponent]
    public class GameObjDis : MonoBehaviour
    {
        public GameObjPoolBase goPoolBase;

        protected void OnEnable()
        {
            if (IsReturn()) { return; }
            goPoolBase.Remove();
            Invoke("Disable", goPoolBase.exisTime);
        }
        protected void OnDisable()
        {
            if (IsReturn()) { return; }
            goPoolBase.Add(this);
            CancelInvoke();
        }
        protected void OnDestroy()
        {
            if (IsReturn()) { return; }
            goPoolBase.Remove(this);
        }
        protected void Disable()
        {
            gameObject.SetActive(false);
        }
        protected bool IsReturn()
        {
            return goPoolBase.sleepPool == null || goPoolBase.activePool == null;
        }
    }
}