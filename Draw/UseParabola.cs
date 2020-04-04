namespace Linns.Draw
{
    using UnityEngine;

    [AddComponentMenu("Linns/Draw Meshes/Use Parabola")]
    public class UseParabola : MonoBehaviour
    {
        public Parabola parabola;
        public GameObject prefab;

        protected void Start()
        {
            parabola.Instantiate(this, transform, prefab);
        }
        protected void Update()
        {
            parabola.Update();
        }
        protected void OnDestroy()
        {
            parabola.Dispose();
        }
    }
}