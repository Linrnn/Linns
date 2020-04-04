namespace Linns.Shaders
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using ListVector3 = System.Collections.Generic.List<UnityEngine.Vector3>;

    [AddComponentMenu("Linns/Shaders/Outline")]
    [RequireComponent(typeof(Renderer))]
    public class Outline : ScriptBase
    {
        protected static readonly HashSet<Mesh> s_registeredMeshes = new HashSet<Mesh>();

        [SerializeField] protected OutlineNumber m_numberMode = OutlineNumber.Static;
        [SerializeField] protected OutlineMode m_outlineMode;
        [SerializeField] protected Color m_outlineColor = Color.white;
        [SerializeField] protected float m_outlineWidth = 20f;
        [SerializeField] protected Material m_outlineFillMaterial;
        [SerializeField] protected Material m_outlineMaskMaterial;

        protected readonly List<Mesh> m_bakeKeys = new List<Mesh>();
        protected readonly List<ListVector3> m_bakeValues = new List<ListVector3>();
        protected bool m_needsUpdate;
        protected Renderer m_renderer;
        protected Renderer[] m_renderers;

        public OutlineMode outlineMode
        {
            get { return m_outlineMode; }
            set
            {
                m_outlineMode = value;
                m_needsUpdate = true;
            }
        }
        public Color outlineColor
        {
            get { return m_outlineColor; }
            set
            {
                m_outlineColor = value;
                m_needsUpdate = true;
            }
        }
        public float outlineWidth
        {
            get { return m_outlineWidth; }
            set
            {
                m_outlineWidth = value;
                m_needsUpdate = true;
            }
        }

        protected void Awake()
        {
            LoadSmoothNormals();
            m_needsUpdate = true;
            switch (m_numberMode)
            {
                case OutlineNumber.Single: GetComponent(out m_renderer); break;
                case OutlineNumber.Static: GetComponentsInChildren(out m_renderers); break;
            }
        }
        protected void OnEnable()
        {
            Renderer[] renderers = null;
            switch (m_numberMode)
            {
                case OutlineNumber.Single: MaterialAdd(m_renderer); break;
                case OutlineNumber.Static: renderers = m_renderers; break;
                case OutlineNumber.Dynamic: GetComponentsInChildren(out renderers); break;
            }
            if (renderers != null)
            {
                foreach (Renderer renderer in renderers) { MaterialAdd(renderer); }
                Loop.Empty(renderers);
            }
        }
        protected void Update()
        {
            if (!m_needsUpdate) { return; }
            m_needsUpdate = false;
            UpdateMaterialProperties();
        }
        protected void OnDisable()
        {
            Renderer[] renderers = null;
            switch (m_numberMode)
            {
                case OutlineNumber.Single: MaterialRemove(m_renderer); break;
                case OutlineNumber.Static: renderers = m_renderers; break;
                case OutlineNumber.Dynamic: GetComponentsInChildren(out renderers); break;
            }
            if (renderers != null)
            {
                foreach (Renderer renderer in renderers) { MaterialRemove(renderer); }
            }
        }
        protected void OnDestroy()
        {
            m_bakeKeys.Clear();
            m_bakeValues.Clear();
            Loop.Empty(m_renderers);
        }
        protected void OnValidate()
        {
            m_needsUpdate = true;
            if (m_bakeKeys.Count != 0 || m_bakeKeys.Count != m_bakeValues.Count)
            {
                m_bakeKeys.Clear();
                m_bakeValues.Clear();
            }
            if (m_bakeKeys.Count == 0) { Bake(); }
        }
        protected void Bake()
        {
            HashSet<Mesh> bakedMeshes = new HashSet<Mesh>();
            foreach (MeshFilter meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                if (!bakedMeshes.Add(meshFilter.sharedMesh)) { continue; }
                List<Vector3> smoothNormals = SmoothNormals(meshFilter.sharedMesh);
                m_bakeKeys.Add(meshFilter.sharedMesh);
                m_bakeValues.Add(smoothNormals);
            }
            bakedMeshes.Clear();
        }
        protected void LoadSmoothNormals()
        {
            foreach (MeshFilter meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                if (!s_registeredMeshes.Add(meshFilter.sharedMesh)) { continue; }
                int index = m_bakeKeys.IndexOf(meshFilter.sharedMesh);
                List<Vector3> smoothNormals = (index >= 0) ? m_bakeValues[index] : SmoothNormals(meshFilter.sharedMesh);
                meshFilter.sharedMesh.SetUVs(3, smoothNormals);
            }

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (s_registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
                {
                    skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
                }
            }
        }
        protected List<Vector3> SmoothNormals(Mesh mesh)
        {
            IEnumerable<IGrouping<Vector3, KeyValuePair<Vector3, int>>> groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index)).GroupBy(pair => pair.Key);
            List<Vector3> smoothNormals = new List<Vector3>(mesh.normals);
            foreach (var group in groups)
            {
                if (group.Count() == 1) { continue; }
                Vector3 smoothNormal = new Vector3();

                foreach (KeyValuePair<Vector3, int> pair in group)
                {
                    smoothNormal += mesh.normals[pair.Value];
                }

                smoothNormal.Normalize();

                foreach (KeyValuePair<Vector3, int> pair in group)
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }
            return smoothNormals;
        }
        protected void UpdateMaterialProperties()
        {
            m_outlineFillMaterial.SetColor("_OutlineColor", m_outlineColor);

            switch (m_outlineMode)
            {
                case OutlineMode.OutlineAll:
                    m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat("_OutlineWidth", m_outlineWidth);
                    break;

                case OutlineMode.OutlineVisible:
                    m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat("_OutlineWidth", m_outlineWidth);
                    break;

                case OutlineMode.OutlineHidden:
                    m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    m_outlineFillMaterial.SetFloat("_OutlineWidth", m_outlineWidth);
                    break;

                case OutlineMode.OutlineAndSilhouette:
                    m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Always);
                    m_outlineFillMaterial.SetFloat("_OutlineWidth", m_outlineWidth);
                    break;

                case OutlineMode.SilhouetteOnly:
                    m_outlineMaskMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    m_outlineFillMaterial.SetFloat("_ZTest", (float)UnityEngine.Rendering.CompareFunction.Greater);
                    m_outlineFillMaterial.SetFloat("_OutlineWidth", 0);
                    break;
            }
        }
        protected void MaterialAdd(Renderer renderer)
        {
            List<Material> materials = renderer.sharedMaterials.ToList();
            materials.Add(m_outlineMaskMaterial);
            materials.Add(m_outlineFillMaterial);
            renderer.materials = materials.ToArray();
            materials.Clear();
        }
        protected void MaterialRemove(Renderer renderer)
        {
            List<Material> materials = renderer.sharedMaterials.ToList();
            materials.Remove(m_outlineMaskMaterial);
            materials.Remove(m_outlineFillMaterial);
            renderer.materials = materials.ToArray();
            materials.Clear();
        }
    }

    public enum OutlineNumber
    {
        Single,
        Static,
        Dynamic
    }

    public enum OutlineMode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }
}