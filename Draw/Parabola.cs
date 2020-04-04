namespace Linns.Draw
{
    using System;
    using UnityEngine;
    using System.Collections.Generic;

    [Serializable]
    public struct Parabola : IDisposable
    {
        public Mathematics.QuadraticFunction function;
        public float range;
        public bool isEquallyDistributed;
        public bool isMove;
        public float moveSpeed;
        private MonoBehaviour m_script;
        private Transform m_parent;
        private GameObject m_prefab;
        private List<GameObject> m_goList;
        private List<float> m_xList;
        private Coroutine m_coroutine;
        private float m_len;

        public int gameObjectCount
        {
            get { return (function.distributionFactor + 1) >> 1; }
        }
        private bool isInstantiate
        {
            get { return gameObjectCount > m_goList.Count; }
        }

        public void Instantiate(MonoBehaviour script, Transform parent, GameObject prefab)
        {
            function.range.y = function.range.x + range;
            m_script = script;
            m_parent = parent;
            m_prefab = prefab;
            if (m_goList != null) { m_goList.Clear(); }
            else { m_goList = new List<GameObject>(); }
            if (m_xList != null) { m_xList.Clear(); }
            else { m_xList = new List<float>(); }
            Instantiate();
        }
        public void Set(Vector3 pos, Vector2 v)
        {
            float g = -Physics.gravity.y;
            if (v.y == 0f) { function.oPos = pos; }
            else
            {
                if (v.y < 0f) { v = -v; }
                float t = v.y / g, gt2 = g * t * t;
                function.oPos.Set
                    (
                    pos.x + gt2 * v.x / v.y,
                    pos.y + v.y * t - 0.5f * gt2,
                    pos.z
                    );
            }
            function.a = -0.5f * g / (v.x * v.x);
            function.range.Set(pos.x, pos.x + range);
        }
        public void Update()
        {
            Instantiate();
            SetList();
            SetTransform();
        }
        public void Dispose()
        {
            function.Dispose();
            if (m_goList != null) { m_goList.Clear(); }
            if (m_xList != null) { m_xList.Clear(); }
            if (m_coroutine != null) { m_script.StopCoroutine(m_coroutine); }
            m_script = null;
            m_parent = null;
            m_prefab = null;
            m_goList = null;
            m_xList = null;
            m_coroutine = null;
        }

        private void Instantiate()
        {
            if (m_coroutine != null) { m_script.StopCoroutine(m_coroutine); }
            if (isInstantiate) { m_coroutine = m_script.StartCoroutine(InstantiateMultiple()); }
        }
        private System.Collections.IEnumerator InstantiateMultiple(int everyCount = 10)
        {
            int deltaCount = gameObjectCount - m_goList.Count;
            for (int i = deltaCount / everyCount; i != 0; --i)
            {
                for (int j = everyCount; j != 0; --j) { InstantiateSingle(); }
                yield return null;
            }
            for (int i = deltaCount % everyCount; i != 0; --i) { InstantiateSingle(); }
        }
        private void InstantiateSingle()
        {
            GameObject obj = UnityEngine.Object.Instantiate(m_prefab, m_parent);
            obj.AddComponent<DropLocation>();
            obj.SetActive(false);
            m_goList.Add(obj);
            obj.name = $"{m_prefab.name} ({m_goList.Count})";
        }
        private void ResetList()
        {
            m_xList.Clear();
            function.range.y = function.range.x + range;
            if (isEquallyDistributed) { m_len = function.GetAverageLength(m_xList); }
            else { foreach (float x in function.xList) { m_xList.Add(x); } }
        }
        private void SetList()
        {
            if (m_xList.Count != function.distributionFactor + 1 || range != function.range.y - function.range.x) { ResetList(); }
            if (!isMove) { return; }
            float speed = moveSpeed * Time.deltaTime;
            for (int i = function.distributionFactor; i != -1; --i)
            {
                m_xList[i] += speed;
                if (m_xList[i] > function.range.y) { m_xList[i] -= range; }
                else if (m_xList[i] < function.range.x) { m_xList[i] += range; }
            }
        }
        private void SetTransform()
        {
            if (isInstantiate) { return; }
            for (int i = gameObjectCount - 1; i != -1; --i)
            {
                GameObject gameObject = m_goList[i];
                Transform transform = gameObject.transform;
                gameObject.SetActive(false);

                Vector3 left = function.GetPos(m_xList[i << 1]);
                Vector3 right = function.GetPos(m_xList[(i << 1) + 1]);
                if (left.x > right.x) { continue; }

                transform.localPosition = (left + right) / 2f;
                Vector2 dir = right - left;
                transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan(dir.y / dir.x) * Mathf.Rad2Deg);
                gameObject.SetActive(true);
            }
            for (int i = gameObjectCount; i != m_goList.Count; ++i) { m_goList[i].SetActive(false); }
        }
    }
}