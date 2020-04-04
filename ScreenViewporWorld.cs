namespace Linns
{
    using UnityEngine;
    using PointerEventData = UnityEngine.EventSystems.PointerEventData;

    /// <summary>
    /// 屏幕坐标，视窗坐标，世界坐标类
    /// </summary>
    public static class ScreenViewporWorld
    {
        /// <summary>
        /// 返回屏幕坐标
        /// </summary>
        public static Vector2 mousePos
        {
            get { return GetMousePos(); }
        }
        /// <summary>
        /// 返回世界坐标
        /// </summary>
        /// <returns></returns>
        public static Vector3 worldPos
        {
            get { return ScreenToWorldPos(GetMousePos(), Camera.main); }
        }

        /// <summary>
        /// 返回屏幕坐标
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static Vector2 GetMousePos(int index = 0)
        {
            if (Application.platform == RuntimePlatform.Android ||
                Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return Input.GetTouch(index).position;
            }
            else if (Mouse.isWindows) { return Input.mousePosition; }
            else { return Vector2.zero; }
        }
        /// <summary>
        /// 返回屏幕坐标
        /// </summary>
        /// <param name="eventData">PointerEventData类型的事件参数</param>
        /// <returns></returns>
        public static Vector2 GetMousePos(PointerEventData eventData)
        {
            return eventData.position;
        }

        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(int index = 0)
        {
            return ScreenToWorldPos(GetMousePos(index), Camera.main);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="otherCamera">传递一个摄像机</param>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Camera otherCamera, int index = 0)
        {
            return ScreenToWorldPos(GetMousePos(index), otherCamera);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="eventData">PointerEventData类型的事件参数</param>
        /// <param name="otherCamera">传递一个摄像机</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(PointerEventData eventData, Camera otherCamera)
        {
            return ScreenToWorldPos(eventData.position, otherCamera);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <returns>世界坐标</returns>
        public static Vector3 ScreenToWorldPos(Vector2 pos)
        {
            return ScreenToWorldPos(pos, Camera.main);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <param name="otherCamera">传递一个摄像机</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Vector2 pos, Camera otherCamera)
        {
            return otherCamera.ScreenToWorldPoint(pos);
        }

        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="rt">传递一个RectTransform</param>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(RectTransform rt, int index = 0)
        {
            return ScreenToWorldPos(GetMousePos(index), rt, Camera.main);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="rt">传递一个RectTransform</param>
        /// <param name="eventCamera">传递一个摄像机</param>
        /// <param name="index">索引值</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(RectTransform rt, Camera eventCamera, int index = 0)
        {
            return ScreenToWorldPos(GetMousePos(index), rt, eventCamera);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="eventData">PointerEventData类型的事件参数</param>
        /// <param name="rt">传递一个RectTransform</param>
        /// <param name="eventCamera">传递一个摄像机</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(PointerEventData eventData, RectTransform rt, Camera eventCamera)
        {
            return ScreenToWorldPos(eventData.position, rt, eventCamera);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <param name="rt">传递一个RectTransform</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Vector2 pos, RectTransform rt)
        {
            return ScreenToWorldPos(pos, rt, Camera.main);
        }
        /// <summary>
        /// 将屏幕坐标转换成世界坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <param name="rt">传递一个RectTransform</param>
        /// <param name="eventCamera">传递一个摄像机</param>
        /// <returns></returns>
        public static Vector3 ScreenToWorldPos(Vector2 pos, RectTransform rt, Camera eventCamera)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, pos, eventCamera, out Vector3 worldPos);
            return worldPos;
        }

        /// <summary>
        /// 将世界坐标转换成屏幕坐标
        /// </summary>
        /// <param name="pos">世界坐标</param>
        /// <returns></returns>
        public static Vector2 WorldToScreenPos(Vector3 pos)
        {
            return Camera.main.WorldToScreenPoint(pos);
        }
        /// <summary>
        /// 将世界坐标转换成屏幕坐标
        /// </summary>
        /// <param name="pos">世界坐标</param>
        /// <param name="otherCamera"></param>
        /// <returns></returns>
        public static Vector2 WorldToScreenPos(Vector3 pos, Camera otherCamera)
        {
            return otherCamera.WorldToScreenPoint(pos);
        }
    }
}