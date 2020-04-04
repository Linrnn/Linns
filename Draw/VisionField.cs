namespace Linns.Draw
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Line2D = Mathematics.Line2D;

    [Serializable]
    public struct VisionField2D : IDisposable
    {
        public Sector2D sector;
        private List<Collider> m_colliderList;
        private bool m_isCall;

        public int distributionFactor
        {
            get { return sector.distributionFactor; }
            set { sector.distributionFactor = value; }
        }
        public GameObject gameObject
        {
            get { return sector.gameObject; }
        }
        public int collidersCount
        {
            get { return m_colliderList.Count; }
        }
        public IEnumerable<Collider> colliders
        {
            get { return m_colliderList; }
        }

        public Vector3 GetVerticesTransformPoint(int index = 0)
        {
            return sector.GetVerticeTransformPoint(index);
        }
        public Collider GetCollider(int index)
        {
            return m_colliderList[index];
        }
        public void Instantiate(int distributionFactor, GameObject target)
        {
            sector.Instantiate(distributionFactor, target);
            if (m_colliderList != null) { m_colliderList.Clear(); }
            else { m_colliderList = new List<Collider>(); }
        }
        public void UpdateReset()
        {
            sector.UpdateReset();
            m_isCall = true;
        }
        public void Update()
        {
            sector.Update();
        }
        public void Update(Line2D line)
        {
            DetecteIsCall();
            ForUpdate(line);
        }
        public void Update(IEnumerable<Line2D> lineList)
        {
            DetecteIsCall();
            foreach (Line2D line in lineList) { ForUpdate(line); }
        }
        public void Update(int layerMask)
        {
            DetecteIsCall();
            m_colliderList.Clear();
            Vector3 start = GetVerticesTransformPoint();

            for (int i = sector.distributionFactor + 1; i != 0; --i)
            {
                if (Physics.Linecast(start, GetVerticesTransformPoint(i), out RaycastHit raycastHit, layerMask))
                {
                    sector[i] = sector.gameObject.transform.InverseTransformPoint(raycastHit.point);
                    if (!m_colliderList.Contains(raycastHit.collider)) { m_colliderList.Add(raycastHit.collider); }
                }
            }
        }
        public void Dispose()
        {
            sector.Dispose();
            m_colliderList.Clear();
            m_colliderList = null;
            m_isCall = false;
        }

        private void ForUpdate(Line2D line)
        {
            InverseTransformLine2D(ref line);
            for (int i = sector.distributionFactor + 1; i != 0; --i)
            {
                Vector3 pos = sector[i];
                Line2D cd = new Line2D() { end = new Vector2(pos.x, pos.z) };
                if (!Line2D.IsIntersectionPoint(line, cd)) { continue; }
                pos = Line2D.GetIntersectionPoint(line, cd);
                pos.z = pos.y;
                pos.y = 0f;
                sector[i] = pos;
            }
        }
        private void DetecteIsCall()
        {
            if (!m_isCall) { Debug.LogError("没有在“Update”（重载）方法前调用“UpdateReset”方法。"); }
            m_isCall = false;
        }
        private void InverseTransformLine2D(ref Line2D line)
        {
            InverseTransformVector2D(ref line.start);
            InverseTransformVector2D(ref line.end);
        }
        private void InverseTransformVector2D(ref Vector2 vector)
        {
            Vector3 pos = new Vector3(vector.x, 0f, vector.y);
            pos = sector.gameObject.transform.InverseTransformPoint(pos);
            vector.Set(pos.x, pos.z);
        }
    }
}