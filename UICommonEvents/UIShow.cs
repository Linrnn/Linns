namespace Linns.UICommonEvents
{
    using UnityEngine;
    using PointerEventData = UnityEngine.EventSystems.PointerEventData;
    using MaskableGraphic = UnityEngine.UI.MaskableGraphic;

    /// <summary>
    /// UI显示结构体 注意：此脚本里的各函数只能操作UI组件
    /// </summary>
    [System.Serializable]
    public struct UIShow
    {
        /// <summary>
        /// 协程
        /// </summary>
        private Coroutine coroutine;

        private static float GetWorldWidth(RectTransform rt)
        {
            return rt.lossyScale.x * rt.rect.width;
        }
        private static float GetWorldHeight(RectTransform rt)
        {
            return rt.lossyScale.y * rt.rect.height;
        }
        private static Vector2 GetWorldWidthAndHeight(RectTransform rt)
        {
            return new Vector2(GetWorldWidth(rt), GetWorldHeight(rt));
        }

        /// <summary>
        ///  ForOnPointerEnter()中的功能
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="pos"></param>
        private static void WorldToLocalPos(Transform trans, ref Vector3 pos)
        {
            if (trans.parent) { pos = trans.parent.InverseTransformPoint(pos); }

            pos.z = trans.localPosition.z;
            trans.localPosition = pos;
        }
        /// <summary>
        /// Stealth Method Switch
        /// </summary>
        /// <param name="rt"></param>
        /// <param name="sm"></param>
        /// <param name="b"></param>
        private static void StealthMethodSwitch(RectTransform rt, in StealthMethod sm, bool b)
        {
            switch (sm)
            {
                case StealthMethod.Stay:
                    rt.gameObject.SetActive(true);
                    foreach (MaskableGraphic ui in rt.GetComponentsInChildren<MaskableGraphic>())
                        ui.enabled = true;
                    break;
                case StealthMethod.Enable:
                    rt.gameObject.SetActive(true);
                    foreach (MaskableGraphic ui in rt.GetComponentsInChildren<MaskableGraphic>())
                        ui.enabled = b;
                    break;
                case StealthMethod.Active:
                    rt.gameObject.SetActive(b);
                    foreach (MaskableGraphic ui in rt.GetComponentsInChildren<MaskableGraphic>())
                        ui.enabled = b;
                    break;
            }
        }

        /// <summary>
        /// 在OnPointerEnter()调用此函数  实现UI的显示
        /// </summary>
        /// <param name="useUIShow"></param>
        /// <param name="eventData">事件数据</param>
        /// <returns></returns>
        public void ForOnPointerEnter(UseUIShow useUIShow, PointerEventData eventData)
        {
            if (coroutine != null) { useUIShow.StopCoroutine(coroutine); }

            coroutine = useUIShow.StartCoroutine(ForOnPointerEnter
                 (eventData, useUIShow.target, useUIShow.isAngleConsistent, useUIShow.angle, useUIShow.range, useUIShow.deviation, useUIShow.stealthMethod));
        }
        /// <summary>
        /// 在OnPointerEnter()调用此协程  实现UI的显示
        /// </summary>
        /// <param name="eventData">事件数据</param>
        /// <param name="rt">被改变游戏对象的rectTransform</param>
        /// <param name="isAngleConsistent">角度是否一致</param>
        /// <param name="range">UI显示的范围</param>
        /// <param name="dev">偏差值</param>
        /// <param name="sm">隐身的方式</param>
        /// <returns></returns>
        public System.Collections.IEnumerator ForOnPointerEnter(PointerEventData eventData, RectTransform rt, bool isAngleConsistent, Vector3 angle, Vector2 range, Vector3 dev, StealthMethod sm)
        {
            if (!rt) { yield break; }

            StealthMethodSwitch(rt, in sm, true);

            for (; ; )
            {
                Vector3 pos = ScreenViewporWorld.ScreenToWorldPos(
                    ScreenViewporWorld.mousePos,
                    eventData.pointerEnter.GetComponent<RectTransform>(),
                    eventData.enterEventCamera);
                pos += Vector3.Scale(GetWorldWidthAndHeight(rt), Vector_2_3_4.Add(-range, 0.5f)) + dev;
                WorldToLocalPos(rt, ref pos);

                if (isAngleConsistent) { rt.eulerAngles = eventData.pointerEnter.transform.eulerAngles; }
                else { rt.localEulerAngles = angle; }

                yield return null;
            }
        }

        /// <summary>
        /// 必须在OnPointerExit()调用此函数（只须要调用一次）    处理UI的状态
        /// </summary>
        /// <param name="useUIShow">this</param>
        public void ForOnPointerExit(UseUIShow useUIShow)
        {
            if (coroutine != null) { useUIShow.StopCoroutine(coroutine); }
            StealthMethodSwitch(useUIShow.target, in useUIShow.stealthMethod, false);
        }
    }
}