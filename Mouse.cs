namespace Linns
{
    using UnityEngine;

    /// <summary>
    /// 鼠标按键结构体
    /// </summary>
    [System.Serializable]
    public struct Mouse
    {
        /// <summary>
        /// 鼠标左键
        /// </summary>
        [Tooltip("鼠标左键")] public bool left;
        /// <summary>
        /// 鼠标右键
        /// </summary>
        [Tooltip("鼠标右键")] public bool right;
        /// <summary>
        /// 鼠标中键
        /// </summary>
        [Tooltip("鼠标中键")] public bool middle;

        public Mouse(bool value)
        {
            left = value;
            right = value;
            middle = value;
        }
        public Mouse(bool left, bool right, bool middle)
        {
            this.left = left;
            this.right = right;
            this.middle = middle;
        }

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return left;
                    case 1: return right;
                    case 2: return middle;
                    default: Debug.LogError("越界！"); return false;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: left = value; break;
                    case 1: right = value; break;
                    case 2: middle = value; break;
                    default: Debug.LogError("越界！"); break;
                }
            }
        }
        /// <summary>
        /// 鼠标按键方式-按下
        /// </summary>
        /// <returns></returns>
        public bool mouseButtonDown
        {
            get
            {
                if (isWindows)
                {
                    if (Input.GetMouseButtonDown(0)) { return left; }
                    else if (Input.GetMouseButtonDown(1)) { return right; }
                    else if (Input.GetMouseButtonDown(2)) { return middle; }
                    else { return false; }
                }
                else { return true; }
            }
        }
        /// <summary>
        /// 鼠标按键方式-按着
        /// </summary>
        /// <returns></returns>
        public bool mouseButtonStay
        {
            get
            {
                if (isWindows)
                {
                    if (Input.GetMouseButton(0)) { return left; }
                    else if (Input.GetMouseButton(1)) { return right; }
                    else if (Input.GetMouseButton(2)) { return middle; }
                    else { return false; }
                }
                else { return true; }
            }
        }
        /// <summary>
        /// 鼠标按键方式-抬起
        /// </summary>
        /// <returns></returns>
        public bool mouseButtonUp
        {
            get
            {
                if (isWindows)
                {
                    if (Input.GetMouseButtonUp(0)) { return left; }
                    else if (Input.GetMouseButtonUp(1)) { return right; }
                    else if (Input.GetMouseButtonUp(2)) { return middle; }
                    else { return false; }
                }
                else { return true; }
            }
        }

        /// <summary>
        /// 判断是否在Windows上运行
        /// </summary>
        public static bool isWindows
        {
            get
            {
                return Application.platform == RuntimePlatform.WindowsPlayer
                    || Application.platform == RuntimePlatform.WindowsEditor;
            }
        }
        /// <summary>
        ///鼠标移动的方向
        /// </summary>
        public static Vector2 direction
        {
            get { return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); }
        }

        public override string ToString()
        {
            return $"Left:{left}   Right:{right}   Middle:{middle}";
        }
    }
}