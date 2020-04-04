namespace Linns.Shaders
{
    using UnityEngine;

    [AddComponentMenu("Linns/Shaders/Rapid Blur Effect")]
    [RequireComponent(typeof(Camera))]
    public class RapidBlurEffect : MonoBehaviour
    {
        [SerializeField]
        protected Material m_material;

        [Range(0f, 6f)]
        [Tooltip("[降采样次数]向下采样的次数。此值越大,则采样间隔越大,需要处理的像素点越少,运行速度越快。")]
        public int downSampleNum = 2;
        [Range(0f, 20f)]
        [Tooltip("[模糊扩散度]进行高斯模糊时，相邻像素点的间隔。此值越大相邻像素间隔越远，图像越模糊。但过大的值会导致失真。")]
        public float blurSpreadSize = 3f;
        [Range(0f, 8f)]
        [Tooltip("[迭代次数]此值越大,则模糊操作的迭代次数越多，模糊效果越好，但消耗越大。")]
        public int blurIterations = 3;

        protected void Start()
        {
            if (!SystemInfo.supportsImageEffects) { enabled = false; }
        }
        protected void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
        {
            if (!m_material)
            {
                Graphics.Blit(sourceTexture, destTexture);
                return;
            }

            float widthMod = 1f / (1 << downSampleNum);
            m_material.SetFloat("_DownSampleValue", blurSpreadSize * widthMod);
            sourceTexture.filterMode = FilterMode.Bilinear;
            int renderWidth = sourceTexture.width >> downSampleNum;
            int renderHeight = sourceTexture.height >> downSampleNum;

            RenderTexture renderBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
            renderBuffer.filterMode = FilterMode.Bilinear;
            Graphics.Blit(sourceTexture, renderBuffer, m_material, 0);

            for (int i = 0; i != blurIterations; ++i)
            {
                float iterationOffs = i;
                m_material.SetFloat("_DownSampleValue", blurSpreadSize * widthMod + iterationOffs);

                RenderTexture tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
                Graphics.Blit(renderBuffer, tempBuffer, m_material, 1);
                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;

                tempBuffer = RenderTexture.GetTemporary(renderWidth, renderHeight, 0, sourceTexture.format);
                Graphics.Blit(renderBuffer, tempBuffer, m_material, 2);

                RenderTexture.ReleaseTemporary(renderBuffer);
                renderBuffer = tempBuffer;
            }
            Graphics.Blit(renderBuffer, destTexture);
            RenderTexture.ReleaseTemporary(renderBuffer);
        }
    }
}