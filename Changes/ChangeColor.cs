namespace Linns.Changes
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// 颜色变化类
    /// </summary>
    [AddComponentMenu("Linns/Changes/Change Color")]
    public class ChangeColor : ChangeBase<Color>
    {
        [SerializeField] private ColorType m_colorType = ColorType.Graphic;
        [SerializeField] private Camera m_camera;
        [SerializeField] private Light m_light;
        [SerializeField] private MeshRenderer m_meshRenderer;
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Graphic m_graphic;
        [SerializeField] private Shadow m_shadow;

        public Color color
        {
            get
            {
                switch (m_colorType)
                {
                    case ColorType.Camera: return m_camera.backgroundColor;
                    case ColorType.Light: return m_light.color;
                    case ColorType.MeshRenderer: return m_meshRenderer.material.color;
                    case ColorType.SpriteRenderer: return m_spriteRenderer.color;
                    case ColorType.Graphic: return m_graphic.color;
                    case ColorType.Shadow: return m_shadow.effectColor;
                    default: return new Color();
                }
            }
            set
            {
                switch (m_colorType)
                {
                    case ColorType.Camera: m_camera.backgroundColor = value; break;
                    case ColorType.Light: m_light.color = value; break;
                    case ColorType.MeshRenderer: m_meshRenderer.material.color = value; break;
                    case ColorType.SpriteRenderer: m_spriteRenderer.color = value; break;
                    case ColorType.Graphic: m_graphic.color = value; break;
                    case ColorType.Shadow: m_shadow.effectColor = value; break;
                }
            }
        }
        public ColorType colorType
        {
            get { return m_colorType; }
            set { m_colorType = value; }
        }
        public new Camera camera
        {
            get { return m_camera; }
            set { m_camera = value; }
        }
        public new Light light
        {
            get { return m_light; }
            set { m_light = value; }
        }
        public MeshRenderer meshRenderer
        {
            get { return m_meshRenderer; }
            set { m_meshRenderer = value; }
        }
        public SpriteRenderer spriteRenderer
        {
            get { return m_spriteRenderer; }
            set { m_spriteRenderer = value; }
        }
        public Graphic graphic
        {
            get { return m_graphic; }
            set { m_graphic = value; }
        }
        public Shadow shadow
        {
            get { return m_shadow; }
            set { m_shadow = value; }
        }
        public override GameObject gameObj
        {
            get
            {
                switch (m_colorType)
                {
                    case ColorType.Camera: return m_camera.gameObject;
                    case ColorType.Light: return m_light.gameObject;
                    case ColorType.MeshRenderer: return m_meshRenderer.gameObject;
                    case ColorType.SpriteRenderer: return m_spriteRenderer.gameObject;
                    case ColorType.Graphic: return m_graphic.gameObject;
                    case ColorType.Shadow: return m_shadow.gameObject;
                    default: return null;
                }
            }
        }

        public ChangeColor()
        {
            m_start = Color.black;
            m_end = Color.white;
        }
        public void Set(GameObject target, Color start, Color end, DecreaseType decreaseType, float speed, ColorType colorType)
        {
            m_start = start;
            m_end = end;
            m_decreaseType = decreaseType;
            m_speed = speed;
            m_colorType = colorType;
            switch (m_colorType)
            {
                case ColorType.Camera: m_camera = target.GetComponent<Camera>(); break;
                case ColorType.Light: m_light = target.GetComponent<Light>(); break;
                case ColorType.MeshRenderer: m_meshRenderer = target.GetComponent<MeshRenderer>(); break;
                case ColorType.SpriteRenderer: m_spriteRenderer = target.GetComponent<SpriteRenderer>(); break;
                case ColorType.Graphic: m_graphic = target.GetComponent<Graphic>(); break;
                case ColorType.Shadow: m_shadow = target.GetComponent<Shadow>(); break;
            }
        }
        protected override System.Collections.IEnumerator RunCoroutine()
        {
            if (speed < 0f) { yield break; }
            if (isReset) { color = m_start; }

            WaitForSeconds wait = waitForSeconds;
            while (Data.IsGreaterMin(((Vector4)(color - m_end)).magnitude))
            {
                switch (m_decreaseType)
                {
                    case DecreaseType.Linear: color = Vector4.MoveTowards(color, m_end, m_speed * Time.deltaTime); break;
                    case DecreaseType.Lerp: color = Color.Lerp(color, m_end, m_speed * Time.deltaTime); break;
                }
                InvokeRunEvent();
                yield return wait;
            }
            color = m_end;
        }
    }

    /// <summary>
    /// 颜色的类型
    /// </summary>
    public enum ColorType
    {
        /// <summary>
        /// 摄像机
        /// </summary>
        Camera,
        /// <summary>
        /// 灯光
        /// </summary>
        Light,
        /// <summary>
        /// 网格渲染器
        /// </summary>
        MeshRenderer,
        /// <summary>
        /// 精灵渲染器
        /// </summary>
        SpriteRenderer,
        /// <summary>
        /// 图像
        /// </summary>
        Graphic,
        /// <summary>
        ///  阴影
        /// </summary>
        Shadow
    }
}