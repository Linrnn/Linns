namespace Linns.ObjectPool
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public struct GameObjPoolBase : IDisposable
    {
        public bool isSingle;
        public float exisTime;
        private GameObject m_prefab;
        private List<GameObject> m_prefabs;
        private Transform m_parent;
        private List<GameObjDis> m_sleepPool;
        private List<GameObjDis> m_activePool;

        public IEnumerable<GameObjDis> sleepPool
        {
            get { return m_sleepPool; }
        }
        public IEnumerable<GameObjDis> activePool
        {
            get { return m_activePool; }
        }

        public void Set(GameObject prefab, Transform parent)
        {
            m_prefab = prefab;
            m_parent = parent;
            if (m_sleepPool != null) { m_sleepPool.Clear(); }
            else { m_sleepPool = new List<GameObjDis>(); }
            if (m_activePool != null) { m_activePool.Clear(); }
            { m_activePool = new List<GameObjDis>(); }
        }
        public void Set(List<GameObject> prefabs, Transform parent)
        {
            m_prefabs = prefabs;
            m_parent = parent;
            if (m_sleepPool != null) { m_sleepPool.Clear(); }
            else { m_sleepPool = new List<GameObjDis>(); }
            if (m_activePool != null) { m_activePool.Clear(); }
            { m_activePool = new List<GameObjDis>(); }
        }
        public GameObject PoolInstantiate()
        {
            GameObject obj;
            GameObjDis objDis;
            if (m_sleepPool.Count == 0)
            {
                obj = UnityEngine.Object.Instantiate(isSingle ? m_prefab : m_prefabs[UnityEngine.Random.Range(0, m_prefabs.Count)], m_parent);
                objDis = obj.GetComponent<GameObjDis>() ?? obj.AddComponent<GameObjDis>();
                objDis.enabled = false;
            }
            else
            {
                objDis = m_sleepPool[0];
                obj = objDis.gameObject;
            }

            objDis.goPoolBase = this;
            objDis.enabled = true;
            obj.SetActive(true);
            return obj;
        }
        public void Add(GameObjDis objDis)
        {
            m_sleepPool.Add(objDis);
            m_activePool.Remove(objDis);
        }
        public void Remove()
        {
            if (m_sleepPool.Count == 0) { return; }
            GameObjDis goDis = m_sleepPool[0];
            m_sleepPool.RemoveAt(0);
            m_activePool.Add(goDis);
        }
        public void Remove(GameObjDis objDis)
        {
            m_sleepPool.Remove(objDis);
            m_activePool.Remove(objDis);
        }
        public void Dispose()
        {
            if (m_prefabs != null) { m_prefabs.Clear(); }
            if (m_sleepPool != null) { m_sleepPool.Clear(); }
            if (m_activePool != null) { m_activePool.Clear(); }
        }
    }
}