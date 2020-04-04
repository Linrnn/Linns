namespace Linns.ObjectPool
{
    using System.Collections.Generic;
    using UnityEngine;

    [AddComponentMenu("Linns/Object Pool/Game Object Pool")]
    public class GameObjPool : MonoBehaviour
    {
        public bool isSingle = true;
        public float exisTime = 1f;
        public GameObject prefab;
        public List<GameObject> prefabs = new List<GameObject>();
        protected GameObjPoolBase m_poolBase;

        public IEnumerable<GameObjDis> sleepPool
        {
            get { return m_poolBase.sleepPool; }
        }
        public IEnumerable<GameObjDis> activePool
        {
            get { return m_poolBase.activePool; }
        }

        protected void Start()
        {
            m_poolBase.Set(prefab, transform);
            m_poolBase.Set(prefabs, transform);
        }
        protected void Update()
        {
            m_poolBase.isSingle = isSingle;
            m_poolBase.exisTime = exisTime;
        }
        protected void OnDestroy()
        {
            prefabs.Clear();
            m_poolBase.Dispose();
        }
        public GameObject Instantiate()
        {
            return m_poolBase.PoolInstantiate();
        }
    }
}