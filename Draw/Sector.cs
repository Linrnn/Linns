namespace Linns.Draw
{
    using System;
    using UnityEngine;
    using static Extensions.ExtensionGameObject;

    [Serializable]
    public struct Sector2D : IDisposable
    {
        [Range(0f, 360f)]
        public float visualAngle;
        public float visualRange;
        private float m_visualAngle;
        private float m_visualRange;
        private int m_distributionFactor;
        private Transform m_target;
        private Renderer m_renderer;
        private MeshFilter m_meshFilter;
        private Mesh m_mesh;
        private Vector3[] m_vertices;
        private int[] m_triangles;

        public Vector3 this[int index]
        {
            get { return m_vertices[index]; }
            set { m_vertices[index] = value; }
        }
        public int distributionFactor
        {
            get { return m_distributionFactor; }
            set
            {
                if (m_distributionFactor == value || value < 1) { return; }
                m_visualAngle = 0f;
                m_visualRange = 0f;
                m_distributionFactor = value;
                m_vertices = new Vector3[value + 2];
                m_triangles = new int[(value << 1) + value];

                for (int i = 0, j = 0; i != value; ++i, ++j)
                {
                    m_triangles[++j] = i + 1;
                    m_triangles[++j] = i + 2;
                }

                m_mesh.vertices = m_vertices;
                m_mesh.triangles = m_triangles;
            }
        }
        public GameObject gameObject
        {
            get { return m_target.gameObject; }
        }

        public Vector3 GetVerticeTransformPoint(int index = 0)
        {
            return m_target.TransformPoint(m_vertices[index]);
        }
        public void Instantiate(int distributionFactor, GameObject target)
        {
            if (m_mesh) { goto end; }

            m_target = target.transform;
            target.GetComponent(out m_renderer);
            m_mesh = new Mesh();
            if (m_renderer is SkinnedMeshRenderer) { (m_renderer as SkinnedMeshRenderer).sharedMesh = m_mesh; }
            else { (m_meshFilter = m_target.GetComponent<MeshFilter>()).mesh = m_mesh; }
        end:
            m_mesh.name = target.name;
            this.distributionFactor = distributionFactor;
        }
        public void UpdateReset()
        {
            m_mesh.vertices = m_vertices;

            int runEnd = (m_distributionFactor >> 1) + 2;
            if ((m_distributionFactor & 1) == 0) { m_vertices[--runEnd].z = visualRange; }

            float angle = (90f + 0.5f * visualAngle) * Mathf.Deg2Rad;
            float deltaAngle = visualAngle / m_distributionFactor * Mathf.Deg2Rad;
            float sinDeltaAngle = Mathf.Sin(deltaAngle), cosDeltaAngle = Mathf.Cos(deltaAngle);

            m_vertices[1].x = visualRange * Mathf.Cos(angle);
            m_vertices[1].z = visualRange * Mathf.Sin(angle);
            m_vertices[++m_distributionFactor].x = -m_vertices[1].x;
            m_vertices[m_distributionFactor--].z = m_vertices[1].z;

            for (int i = 2, j = m_distributionFactor, k = 1; i != runEnd; ++i, --j, ++k)
            {
                m_vertices[i].x = m_vertices[k].x * cosDeltaAngle + m_vertices[k].z * sinDeltaAngle;
                m_vertices[i].z = m_vertices[k].z * cosDeltaAngle - m_vertices[k].x * sinDeltaAngle;
                m_vertices[j].x = -m_vertices[i].x;
                m_vertices[j].z = m_vertices[i].z;
            }

            m_mesh.RecalculateBounds();
        }
        public void Update()
        {
            if (m_visualAngle == visualAngle && m_visualRange == visualRange) { return; }
            else if (m_distributionFactor < 1 || !m_renderer.enabled) { return; }
            m_visualAngle = visualAngle;
            m_visualRange = visualRange;
            UpdateReset();
        }
        public void Dispose()
        {
            if (!m_mesh) { return; }
            UnityEngine.Object.Destroy(m_mesh);
            if (m_renderer is SkinnedMeshRenderer) { (m_renderer as SkinnedMeshRenderer).sharedMesh = null; }
            else { m_meshFilter.mesh = null; }
            m_target = null;
            m_mesh = null;
            m_renderer = null;
            m_meshFilter = null;
            m_vertices = null;
            m_triangles = null;
        }
    }
}