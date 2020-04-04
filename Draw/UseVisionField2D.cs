namespace Linns.Draw
{
    using UnityEngine;

    [AddComponentMenu("Linns/Draw Meshes/Use Vision Field 2D")]
    [RequireComponent(typeof(Renderer))]
    public class UseVisionField2D : ScriptBase
    {
        public int distributionFactor = 256;
        public VisionField2D visionField = new VisionField2D() { sector = new Sector2D() { visualAngle = 120f, visualRange = 10f } };
        public LayerMask layerMask;
        public bool controlMeshRenderer = true;
        protected Renderer m_renderer;

        protected void Awake()
        {
            visionField.Instantiate(distributionFactor, gameObject);
            GetComponent(out m_renderer);
        }
        protected void OnEnable()
        {
            visionField.distributionFactor = distributionFactor;
            if (controlMeshRenderer) { m_renderer.enabled = true; }
        }
        protected void Update()
        {
            visionField.UpdateReset();
            visionField.Update(layerMask);
        }
        protected void OnDisable()
        {
            if (controlMeshRenderer) { m_renderer.enabled = false; }
        }
        protected void OnDestroy()
        {
            visionField.Dispose();
        }
    }
}