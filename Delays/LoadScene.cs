namespace Linns.Delays
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// 加载场景类
    /// </summary>
    [AddComponentMenu("Linns/Delays/Load Scene")]
    public class LoadScene : Delay
    {
        public SceneParameterMode parameterMode = SceneParameterMode.SceneName;
        public int sceneIndex;
        public string sceneName;
        public LoadSceneMode loadSceneMode;
        public bool isAsync = true;

        protected SceneParameterMode m_parameterType = SceneParameterMode.SceneName;
        protected int m_sceneIndex;
        protected string m_sceneName;
        protected bool m_isAsync;

        public AsyncOperation asyncOperation
        {
            get;
            private set;
        }
        public float progress
        {
            get { return asyncOperation == null ? 0f : asyncOperation.progress / 0.9f; }
        }

        public override void Run()
        {
            m_parameterType = parameterMode;
            switch (parameterMode)
            {
                case SceneParameterMode.SceneIndex: m_sceneIndex = sceneIndex; break;
                case SceneParameterMode.SceneName: m_sceneName = sceneName; break;
                default: return;
            }
            Run(m_isAsync);
        }
        public void Run(int sceneIndex)
        {
            m_parameterType = SceneParameterMode.SceneIndex;
            m_sceneIndex = sceneIndex;
            Run(m_isAsync);
        }
        public void Run(string sceneName)
        {
            m_parameterType = SceneParameterMode.SceneName;
            m_sceneName = sceneName;
            Run(m_isAsync);
        }
        public void Run(bool isAsync)
        {
            m_isAsync = isAsync;
            base.Run();
        }
        protected override void RunMethod()
        {
            if (m_isAsync)
                switch (m_parameterType)
                {
                    case SceneParameterMode.SceneIndex:
                        asyncOperation = SceneManager.LoadSceneAsync(m_sceneIndex, loadSceneMode);
                        break;
                    case SceneParameterMode.SceneName:
                        asyncOperation = SceneManager.LoadSceneAsync(m_sceneName, loadSceneMode);
                        break;
                }
            else
            {
                asyncOperation = null;
                switch (m_parameterType)
                {
                    case SceneParameterMode.SceneIndex:
                        SceneManager.LoadScene(m_sceneIndex, loadSceneMode);
                        break;
                    case SceneParameterMode.SceneName:
                        SceneManager.LoadScene(m_sceneName, loadSceneMode);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 加载场景的方式
    /// </summary>
    public enum SceneParameterMode
    {
        /// <summary>
        /// 使用场景的索引
        /// </summary>
        SceneIndex,
        /// <summary>
        /// 使用场景的名字
        /// </summary>
        SceneName,
    }
}