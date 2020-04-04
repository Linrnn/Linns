namespace Linns.Draw
{
    using UnityEngine;

    [AddComponentMenu("Linns/Draw Meshes/Drop Location")]
    [RequireComponent(typeof(Renderer))]
    public class DropLocation : ScriptBase
    {
        protected Renderer m_renderer;

        protected void Start()
        {
            GetComponent(out m_renderer);
        }
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<DropLocation>()) { m_renderer.enabled = false; }
        }
        protected void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<DropLocation>()) { m_renderer.enabled = true; }          
        }
    }
}